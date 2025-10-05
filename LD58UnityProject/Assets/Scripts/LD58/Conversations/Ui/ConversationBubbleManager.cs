using UnityEngine;

namespace LD58.Conversations.Ui
{
    public class ConversationBubbleManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ConversationManager _conversationManager;
        [SerializeField] private ConversationBubbleUi _leftBubble;
        [SerializeField] private ConversationBubbleUi _rightBubble;

        private ConversationBubbleUi _currentConversationPlayerBubble;
        private ConversationBubbleUi _currentConversationNpcBubble;

        private Transform _playerAnchor;
        private Transform _npcAnchor;

        public void Start()
        {
            _conversationManager.OnConversationStarted.AddListener( HandleConversationStarted );
            _conversationManager.OnPlayerSaidSomething.AddListener( HandlePlayerSaidSomething );
            _conversationManager.OnNpcSaidSomething.AddListener( HandleNpcSaidSomething );
        }

        private void HandlePlayerSaidSomething( string text ) => ShowBubble( _currentConversationPlayerBubble, text );
        private void HandleNpcSaidSomething( string text ) => ShowBubble( _currentConversationNpcBubble, text );
        private static void ShowBubble( ConversationBubbleUi bubble, string text ) => bubble.Show( text, 2 );

        private void HandleConversationStarted( Conversation conversation )
        {
            _currentConversationPlayerBubble = conversation.Player.transform.position.x < conversation.Npc.transform.position.x ? _leftBubble : _rightBubble;
            _currentConversationNpcBubble = _currentConversationPlayerBubble == _leftBubble ? _rightBubble : _leftBubble;

            _playerAnchor = conversation.Player.ConversationBubbleWorldAnchor;
            _npcAnchor = conversation.Npc.Character.ConversationBubbleWorldAnchor;
        }

        private void Update()
        {
            if( _currentConversationPlayerBubble && _playerAnchor ) _currentConversationPlayerBubble.transform.position = _camera.WorldToScreenPoint( _playerAnchor.position );
            if( _currentConversationNpcBubble && _npcAnchor ) _currentConversationNpcBubble.transform.position = _camera.WorldToScreenPoint( _npcAnchor.position );
        }
    }
}