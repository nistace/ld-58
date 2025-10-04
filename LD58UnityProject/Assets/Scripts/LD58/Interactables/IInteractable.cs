using UnityEngine;

namespace LD58.Interactables
{
    public interface IInteractable
    {
        GameObject gameObject { get; }
        Vector3 Position { get; }
        string InteractionDisplayText { get; }
        Vector3 TooltipWorldAnchor { get; }

        void Interact();
    }
}