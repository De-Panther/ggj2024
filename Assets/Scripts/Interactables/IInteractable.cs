using Interactables.Gameplay;
using Intractables;

namespace Interactables
{
    public interface IInteractable
    {
        public void OnTouchEnter(InteractionType interactionType, Direction direction);
        public void OnTouchExit();
    }
}