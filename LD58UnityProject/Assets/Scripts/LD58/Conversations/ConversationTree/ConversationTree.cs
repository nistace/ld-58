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
            var shareInformationSubNodes = new List<ConversationNode>();

            foreach( var requestableInformation in config.RequestableInformations )
            {
                if( !requestableInformation.OnlyIfUnknown || !record.IsKnown( requestableInformation.Information ) )
                {
                    shareInformationSubNodes.Add( new AskInformationConversationNode( requestableInformation.Information,
                            requestableInformation.OptionText,
                            requestableInformation.DiscussionText
                        )
                    );
                }
            }

            if( shareInformationSubNodes.Count > 0 )
            {
                _roots.Add( new ConversationNode( config.ShareInformationOptionName, shareInformationSubNodes.ToArray() ) );
            }
        }
    }
}