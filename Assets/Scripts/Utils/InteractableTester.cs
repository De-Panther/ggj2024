using Interactables;
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
        
        private InputAction _mousePositionAction;
        private InputAction _keyAction;
        private Vector2 _mousePosition;
        private bool _keyIsPressed = false;
        private IInteractable _currentlyInteracting;
        
        #endregion
        
        
        #region --- Constants ---

        private const string MOUSE_POSITION_ACTION = "<Mouse>/position";
        private const string KEY_ACTION = "<Keyboard>/e";
        
        #endregion
        
        
        #region --- Unity Methods ---

        private void OnEnable()
        {
            _mousePositionAction = new InputAction(binding: MOUSE_POSITION_ACTION);
            _mousePositionAction.Enable();
            _mousePositionAction.performed += UpdateMousePosition;
            _keyAction = new InputAction(binding: KEY_ACTION);
            _keyAction.Enable();
            _keyAction.started += _ => _keyIsPressed = true; 
            _keyAction.canceled += _ => _keyIsPressed = false; 
        }
        
        private void OnDisable()
        {
            _mousePositionAction.performed -= UpdateMousePosition;
            _mousePositionAction.Disable();
            _keyAction.started -= _ => _keyIsPressed = true;
            _keyAction.canceled -= _ => _keyIsPressed = false;
            _keyAction.Disable();
        }   

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            Ray ray = CameraUtils.MainCamera.ScreenPointToRay(_mousePosition);
            RaycastHit hit;
            IInteractable newInteracting = null;

            if (Physics.Raycast(ray, out hit))
            {
                Gizmos.color = _gizmoColor;
                Vector3 worldPosition = hit.point;
                Gizmos.DrawSphere(worldPosition, _gizmoSize);
        
                newInteracting = hit.transform.GetComponent<IInteractable>();
            }

            if (_currentlyInteracting == newInteracting) return;
            
            _currentlyInteracting?.OnTouchExit();
            
            if (_keyIsPressed)
            {
                newInteracting?.OnTouchEnter();
            } 

            _currentlyInteracting = newInteracting;
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
            _mousePosition = context.ReadValue<Vector2>();
        }
        
        #endregion
    }
}