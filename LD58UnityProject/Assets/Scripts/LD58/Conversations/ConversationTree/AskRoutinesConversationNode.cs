using System;

namespace LD58.Conversations
{
    [Serializable]
    public class AskRoutinesConversationNode : ConversationNode
    {
        public AskRoutinesConversationNode( string optionText, string conversationText ) : base( optionText, conversationText ) { }
    }
}