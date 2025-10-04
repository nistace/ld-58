using LD58.Characters;
using LD58.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Conversations
{
    public class ConversationStarterInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NpcCharacter _conversationTarget;
        [SerializeField] private Transform _tooltipWorldAnchor;

        public Vector3 Position => transform.position;
        public string InteractionDisplayText => "Talk";
        public Vector3 TooltipWorldAnchor => _tooltipWorldAnchor.position;
        public NpcCharacter ConversationTarget => _conversationTarget;

        public static UnityEvent<ConversationStarterInteractable> OnConversationRequested { get; } = new();

        public void Interact() => OnConversationRequested.Invoke( this );
    }
}