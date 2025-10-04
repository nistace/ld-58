using LD58.Characters;
using LD58.Characters.PlayerCharacters;
using LD58.Records;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Conversations
{
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] private NpcManager _npcManager;
        [SerializeField] private NpcRecordManager _recordManager;
        [SerializeField] private PlayerCharacter _playerCharacter;

        public bool IsConversationOnGoing => _playerCharacter.IsTalking;
        public UnityEvent<Conversation> OnConversationStarted { get; } = new();
        public UnityEvent<Conversation> OnConversationEnded { get; } = new();
        public Conversation CurrentConversation { get; private set; }

        private void Start()
        {
            ConversationStarterInteractable.OnConversationRequested.AddListener( HandleConversationRequested );
        }

        private void OnDestroy()
        {
            ConversationStarterInteractable.OnConversationRequested.RemoveListener( HandleConversationRequested );
        }

        private void HandleConversationRequested( ConversationStarterInteractable source )
        {
            if( IsConversationOnGoing )
            {
                Debug.LogWarning( "Is already in conversation" );

                return;
            }

            CurrentConversation = new Conversation( source.ConversationTarget, _recordManager.GetOrCreateRecord( source.ConversationTarget ) );

            source.ConversationTarget.IsTalking = true;
            _playerCharacter.IsTalking = true;
        }
    }
}