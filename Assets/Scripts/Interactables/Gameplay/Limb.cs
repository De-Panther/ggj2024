using Intractables;
using UnityEngine;
using Utils;

namespace Interactables.Gameplay
{
    public class Limb : MonoBehaviour, IInteractable
    {
        #region --- Inspector ---

        [SerializeField] private Transform _joint;
        [SerializeField] private float _maxRotationAngle = 45f; // maximum rotation angle in degrees
        
        #endregion
        
        
        #region --- Private Fields ---
        
        private bool _isBeingTouched = false;
        private bool _modifierEnabled = false;
        
        #endregion
        
        
        #region --- Constatns ---
        
        private const float ROTATION_SPEED = 1.5f;
        
        #endregion
        
        
        #region --- Unity Methods ---
        
        private void Update()
        {
            if (!_isBeingTouched) return;

            if (!_modifierEnabled)
            {
                RotateBodyAroundHand();
                return;
            }

            RotateAroundLimb();
        }
        
        #endregion
        

        #region --- Public Methods ---
        
        public void OnTouchEnter(InteractionType interactionType)
        {
            LoggerService.LogWarning($"OnTouchEnter: {gameObject.name}");
            _isBeingTouched = true;
            _modifierEnabled = (interactionType == InteractionType.Electrify);
        }

        public void OnTouchExit()
        {
            LoggerService.LogWarning($"OnTouchExit: {gameObject.name}");
            _isBeingTouched = false;
        }
        
        #endregion
        
        
        #region --- Private Methods ---
        
        private void RotateBodyAroundHand()
        {
            var rootParent = transform;
            while (rootParent.parent != null)
            {
                rootParent = rootParent.parent;
            }

            var hand = FindHandForCurrentLimb(transform.gameObject, "Wiper");

            if (hand == null)
            {
                LoggerService.LogError("A child with the tag 'Wiper' wasn't found, for " + transform.name);
                return;
            }

            var position = hand.transform.position;
            var initialHandPosition = position;

            rootParent.RotateAround(position, Vector3.forward, Time.deltaTime * ROTATION_SPEED * 10f);
            var movement = position - initialHandPosition;
            rootParent.position -= movement;  
        }

        private void RotateAroundLimb()
        {
            var position = _joint.position;
            var z = Vector3.Distance(CameraUtils.MainCamera.transform.position, position);
            var touchPositionWorld = CameraUtils.MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            var touchDirection = touchPositionWorld - position;

            var angleModifier = _modifierEnabled ? -1 : 1;
            var angle = Vector3.SignedAngle(_joint.up, touchDirection, -Vector3.forward) * angleModifier; 

            _joint.Rotate(Vector3.forward, angle * Time.deltaTime * ROTATION_SPEED, Space.World);
        }
        
        private GameObject FindHandForCurrentLimb(GameObject limb, string tag)
        {
            if (limb.gameObject.CompareTag(tag))
            {
                return limb.gameObject;
            }

            foreach (Transform child in limb.transform)
            {
                var result = FindHandForCurrentLimb(child.gameObject, tag);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        
        #endregion
    }
}