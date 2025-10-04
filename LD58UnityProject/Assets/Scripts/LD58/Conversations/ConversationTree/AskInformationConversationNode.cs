using LD58.Records;
using System;
using UnityEngine;

namespace LD58.Conversations
{
    [Serializable]
    public class AskInformationConversationNode : ConversationNode
    {
        [SerializeField] private NpcRecord.EInformation _askedInformation;

        public AskInformationConversationNode( NpcRecord.EInformation information, string buttonText, string conversationText ) : base( buttonText, conversationText )
        {
            _askedInformation = information;
        }
    }
}