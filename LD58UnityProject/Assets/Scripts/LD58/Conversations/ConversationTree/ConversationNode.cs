using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD58.Conversations
{
    [Serializable]
    public class ConversationNode
    {
        [SerializeField] private string _optionText;
        [SerializeField] private string _conversationText;
        [SerializeField] private ConversationNode[] _subNodes;

        public string OptionText => _optionText;
        public string ConversationText => _conversationText;
        public bool LeafNode => _subNodes is { Length: 0 };
        public IReadOnlyList<ConversationNode> SubNodes => _subNodes;

        private ConversationNode( string optionText, string conversationText, ConversationNode[] subNodes )
        {
            _optionText = optionText;
            _conversationText = conversationText;
            _subNodes = subNodes;
        }

        public ConversationNode( string optionText, ConversationNode[] subNodes ) : this( optionText, string.Empty, subNodes ) { }

        public ConversationNode( string optionText, string conversationText ) : this( optionText, conversationText, Array.Empty<ConversationNode>() ) { }
    }
}