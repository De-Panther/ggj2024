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

            interactable?.OnTouchEnter(interactionType);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();

            interactable?.OnTouchExit();
        }

        #endregion
    }
}