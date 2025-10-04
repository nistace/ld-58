using LD58.Characters;
using LD58.Records;
using System;
using UnityEngine;

namespace LD58.Conversations
{
    [Serializable]
    public class Conversation
    {
        [SerializeField] private NpcCharacter _npc;
        [SerializeField] private NpcRecord _record;
        [SerializeField] private ConversationTreeBuilderConfig _config;

        public NpcCharacter Npc => _npc;

        public NpcRecord Record => _record;

        public ConversationTree CurrentConversationTree { get; private set; }
        public bool IsOver { get; set; }

        public Conversation( NpcCharacter npc, NpcRecord record, ConversationTreeBuilderConfig config )
        {
            _npc = npc;
            _record = record;
            _config = config;
        }

        public ConversationTree GenerateNextConversationTree() => CurrentConversationTree = new ConversationTree( _record, _config );
    }
}