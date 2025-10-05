using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace LD58.Conversations.Ui
{
    public class ConversationUi : MonoBehaviour
    {
        [SerializeField] private ConversationManager _conversationManager;
        [SerializeField] private TMP_Text _pathText;
        [SerializeField] private GameObject _pathContainer;
        [SerializeField] private GameObject _conversationButtonsContainer;
        [SerializeField] private ConversationButtonUi[] _conversationButtons;
        [SerializeField] private bool _showPath;

        private readonly List<ConversationNode> _pathNodes = new();

        private void Start()
        {
            _pathContainer.SetActive( _showPath && _conversationManager.IsConversationOnGoing );
            _conversationButtonsContainer.SetActive( false );
            RefreshPathText();

            _conversationManager.OnConversationTreeGenerated.AddListener( HandleConversationTreeGenerated );
            _conversationManager.OnConversationStarted.AddListener( HandleConversationStarted );
            _conversationManager.OnConversationEnded.AddListener( HandleConversationEnded );
            _conversationManager.OnConversationOptionSelected.AddListener( HandleConversationOptionSelected );
            ConversationButtonUi.OnConversationButtonClicked.AddListener( HandleButtonClicked );
        }

        private void HandleConversationOptionSelected()
        {
            _pathContainer.SetActive( false );
            _conversationButtonsContainer.SetActive( false );
        }

        private void HandleConversationEnded( Conversation _ )
        {
            _pathContainer.SetActive( false );
            _conversationButtonsContainer.SetActive( false );
        }

        private void HandleConversationStarted( Conversation conversation )
        {
            _pathNodes.Clear();
            RefreshPathText();
            _pathContainer.gameObject.SetActive( _showPath );
        }

        private void RefreshPathText() => _pathText.text = string.Join( " > ", _pathNodes.Select( t => t.OptionText ) );

        private void HandleButtonClicked( ConversationNode node )
        {
            _pathNodes.Add( node );

            RefreshPathText();

            if( node.LeafNode )
            {
                _conversationManager.SelectLeafOptionInCurrentConversationTree( node );

                return;
            }

            RefreshButtonsWithSubNodes();
        }

        private void RefreshButtonsWithSubNodes()
        {
            var nodes = _conversationManager.CurrentConversation.CurrentConversationTree.Roots;

            if( _pathNodes.Count > 0 )
            {
                nodes = _pathNodes.Last().SubNodes;
            }

            for( var i = 0; i < nodes.Count; ++i )
            {
                _conversationButtons[ i ].SetUp( nodes[ i ] );
                _conversationButtons[ i ].gameObject.SetActive( true );
            }

            for( var i = nodes.Count; i < _conversationButtons.Length; ++i )
            {
                _conversationButtons[ i ].gameObject.SetActive( false );
            }

            _conversationButtonsContainer.SetActive( true );
        }

        private void HandleConversationTreeGenerated( Conversation _ )
        {
            _pathNodes.Clear();

            RefreshPathText();
            RefreshButtonsWithSubNodes();
        }
    }
}