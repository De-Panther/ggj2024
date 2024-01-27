using Intractables;
using Progress;
using UnityEngine;
using Utils;

namespace Interactables.Gameplay
{
    public class Limb : MonoBehaviour, IInteractable
    {
        #region --- Inspector ---

        [SerializeField] private Transform _joint;
        
        #endregion
        
        
        #region --- Private Fields ---
        
        private bool _isBeingTouched = false;
        private bool _modifierEnabled = false;
        private Direction _direction;
        
        #endregion
        
        
        #region --- Constatns ---
        
        private const float ROTATION_SPEED = 1.5f;
        private const string WIPER_TAG = "Wiper";
        private const string ELECTRICITY_SOUND = "electric";
        private const string LAUGH_SOUND = "laugh";
        
        #endregion
        
        
        #region --- Properties ---

        private string LaughSound
        {
            get { return LAUGH_SOUND + Random.Range(1, 4); }
        }
        
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
        
        public void OnTouchEnter(InteractionType interactionType, Direction direction)
        {
            LoggerService.LogWarning($"OnTouchEnter: {gameObject.name}");
            _isBeingTouched = true;
            _modifierEnabled = (interactionType == InteractionType.Electrify);
            _direction = direction;
            PlaySfx(_modifierEnabled);
        }

        public void OnTouchExit()
        {
            LoggerService.LogWarning($"OnTouchExit: {gameObject.name}");
            _isBeingTouched = false;
            StopPlaySfx();
        }
        
        #endregion
        
        
        #region --- Private Methods ---
        
        private void PlaySfx(bool isElectrified)
        {
            var soundName = isElectrified ? ELECTRICITY_SOUND : LaughSound;
            GameController.Instance.PlaySfx(soundName);
        }
        
        private void StopPlaySfx()
        {
            GameController.Instance.StopPlayingSfx();
        }
        
        private void RotateBodyAroundHand()
        {
            var rootParent = transform;
            while (rootParent.parent != null)
            {
                rootParent = rootParent.parent;
            }

            var hand = FindHandForCurrentLimb(transform.gameObject, WIPER_TAG);

            if (hand == null) { return; }

            var position = hand.transform.position;
            var initialHandPosition = position;
            var direction = _direction == Direction.West ? -1 : 1;
            LoggerService.LogInfo("Rotate Body Direction is: " + _direction);

            rootParent.RotateAround(position, Vector3.forward, Time.deltaTime * ROTATION_SPEED * 10f * direction);
            var movement = position - initialHandPosition;
            rootParent.position -= movement;  
        }

        private void RotateAroundLimb()
        {
            var position = _joint.position;
            var z = Vector3.Distance(CameraUtils.MainCamera.transform.position, position);
            var touchPositionWorld = CameraUtils.MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            var touchDirection = touchPositionWorld - position;

            var angleModifier = _direction == Direction.North ? -1 : 1;
            
            LoggerService.LogInfo("Rotate Limb Direction is: " + _direction);
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