using LD58.Records;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD58.Conversations
{
    [Serializable]
    public class ConversationTree
    {
        [SerializeField] private List<ConversationNode> _roots = new();

        public IReadOnlyList<ConversationNode> Roots => _roots;

        public ConversationTree( NpcRecord record, ConversationTreeBuilderConfig config )
        {
            _roots.Add( new NameConversationNode( "Ask name", config.RandomAskNameLine ) );
            _roots.Add( new TaxConversationNode( "Calculate tax", config.RandomTaxConversationLine ) );
            _roots.Add( new EndConversationNode( "End conversation", config.RandomEndConversationLine ) );
        }
    }
}