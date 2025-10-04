using Cysharp.Threading.Tasks;
using LD58.Characters;
using LD58.Characters.PlayerCharacters;
using LD58.Records;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Conversations
{
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] private NpcManager _npcManager;
        [SerializeField] private NpcRecordManager _recordManager;
        [SerializeField] private PlayerCharacter _playerCharacter;
        [SerializeField] private ConversationTreeBuilderConfig _conversationTreeBuilderConfig;

        public bool IsConversationOnGoing => _playerCharacter.IsTalking;
        public UnityEvent<Conversation> OnConversationStarted { get; } = new();
        public UnityEvent<Conversation> OnConversationEnded { get; } = new();
        public Conversation CurrentConversation { get; private set; }
        private ConversationNode SelectedNodeInCurrentConversation { get; set; }
        private CancellationTokenSource ConversationCancellationToken { get; set; }

        public UnityEvent<Conversation> OnConversationTreeGenerated { get; } = new();
        public UnityEvent<Conversation, ConversationNode> OnConversationTreeOptionSelected { get; } = new();

        private void Start()
        {
            ConversationStarterInteractable.OnConversationRequested.AddListener( HandleConversationRequested );
        }

        private void OnDestroy()
        {
            ConversationCancellationToken?.Cancel();
            ConversationCancellationToken?.Dispose();
            ConversationCancellationToken = null;
            ConversationStarterInteractable.OnConversationRequested.RemoveListener( HandleConversationRequested );
        }

        private void HandleConversationRequested( ConversationStarterInteractable source )
        {
            if( IsConversationOnGoing )
            {
                Debug.LogWarning( "Is already in conversation" );

                return;
            }

            ConversationCancellationToken?.Cancel();
            ConversationCancellationToken?.Dispose();
            ConversationCancellationToken = new CancellationTokenSource();

            ConverseAsync( source.ConversationTarget, ConversationCancellationToken.Token ).Forget();
        }

        private async UniTask ConverseAsync( NpcCharacter npcCharacter, CancellationToken cancellationToken )
        {
            try
            {
                CurrentConversation = new Conversation( npcCharacter, _recordManager.GetOrCreateRecord( npcCharacter ), _conversationTreeBuilderConfig );

                npcCharacter.IsTalking = true;
                _playerCharacter.IsTalking = true;

                OnConversationStarted.Invoke( CurrentConversation );

                while( !CurrentConversation.IsOver )
                {
                    CurrentConversation.GenerateNextConversationTree();
                    SelectedNodeInCurrentConversation = null;

                    OnConversationTreeGenerated.Invoke( CurrentConversation );

                    await UniTask.WaitWhile( () => SelectedNodeInCurrentConversation == null, cancellationToken: cancellationToken );

                    OnConversationTreeOptionSelected.Invoke( CurrentConversation, SelectedNodeInCurrentConversation );

                    _recordManager.GetOrCreateRecord( npcCharacter ).Learn( NpcRecord.EInformation.Name, "Kevin" );

                    CurrentConversation.IsOver = true;
                }
            }
            finally
            {
                npcCharacter.IsTalking = false;
                _playerCharacter.IsTalking = false;

                OnConversationEnded.Invoke( CurrentConversation );

                CurrentConversation = null;
            }
        }

        public void SelectLeafOptionInCurrentConversationTree( ConversationNode leafOption ) => SelectedNodeInCurrentConversation = leafOption;
    }
}