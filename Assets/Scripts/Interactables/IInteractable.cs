using Intractables;

namespace Interactables
{
    public interface IInteractable
    {
        public void OnTouchEnter(InteractionType interactionType);
        public void OnTouchExit();
    }
}