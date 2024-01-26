using Intractables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class InteractableTester : MonoBehaviour
    {
        #region --- Inspector ---
        
        [SerializeField] private float _gizmoSize = 0.1f;
        [SerializeField] private Color _gizmoColor = Color.yellow;
        [SerializeField] private InteractionType _interactionType;
        
        #endregion
        
        
        #region --- Private Fields ---
        
        private Camera _camera;
        private int _zWorldLocationBasedOnCamera;
        private InputAction mousePositionAction;
        private Vector2 mousePosition;
        
        #endregion
        
        
        #region --- Constants ---

        private const string MOUSE_POSITION_ACTION = "<Mouse>/position";
        
        #endregion
        
        
        #region --- Unity Methods ---

        private void OnEnable()
        {
            mousePositionAction = new InputAction(binding: MOUSE_POSITION_ACTION);
            mousePositionAction.Enable();
            mousePositionAction.performed += UpdateMousePosition;
        }
        
        private void OnDisable()
        {
            mousePositionAction.performed -= UpdateMousePosition;
            mousePositionAction.Disable();
        }   

        private void Start()
        {
            _camera = Camera.main;

            if (_camera != null)
            {
                _zWorldLocationBasedOnCamera = (int)_camera.transform.position.z * -1;
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }
    
            Gizmos.color = _gizmoColor;    
            var screenPosition = new Vector3(mousePosition.x, mousePosition.y, _zWorldLocationBasedOnCamera);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            Gizmos.DrawSphere(worldPosition, _gizmoSize);
        }
        
        #endregion
        
        
        #region --- private Methods ---
        
        private void ChangeInteractionType(InteractionType newInteractionType)
        {
            _interactionType = newInteractionType;
            
            switch (_interactionType)
            {
                case InteractionType.Electrify:
                    _gizmoColor = Color.green;
                    break;
                case InteractionType.Tickle:
                    _gizmoColor = Color.blue;
                    break;
                default:
                    _gizmoColor = Color.yellow;
                    break;
            }
        }
        
        private void UpdateMousePosition(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }
        
        #endregion
    }
}