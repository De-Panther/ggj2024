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

            var position = _joint.position;
            var z = Vector3.Distance(CameraUtils.MainCamera.transform.position, position);
            var touchPositionWorld = CameraUtils.MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            var touchDirection = touchPositionWorld - position;

            var angleModifier = _modifierEnabled ? -1 : 1;
            var angle = Vector3.SignedAngle(_joint.up, touchDirection, -Vector3.forward) * angleModifier; 

            _joint.Rotate(Vector3.forward, angle * Time.deltaTime * ROTATION_SPEED, Space.World);
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
    }
}