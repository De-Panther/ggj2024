using Intractables;
using UnityEngine;

namespace Interactables.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class InteractionActor : MonoBehaviour
    {
        #region --- Inspector ---
        
        [SerializeField] private InteractionType interactionType;
        
        #endregion

        
        #region --- Unity Methods ---

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();
            Direction direction = Direction.North;

            if (interactionType == InteractionType.Electrify)
            {
                var actorPosition = transform.position;
                var interactablePosition = other.transform.position;

                direction = actorPosition.y > interactablePosition.y ? Direction.North : Direction.South;
            }
            else
            {
                direction = other.gameObject.name.ToLower().Contains("arm") ? Direction.East : Direction.West;
            }
            
            interactable?.OnTouchEnter(interactionType, direction);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();

            interactable?.OnTouchExit();
        }

        #endregion
    }
}