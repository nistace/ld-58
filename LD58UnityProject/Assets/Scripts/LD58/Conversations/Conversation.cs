using LD58.Characters;
using LD58.Characters.PlayerCharacters;
using LD58.Records;
using System;
using UnityEngine;

namespace LD58.Conversations
{
    [Serializable]
    public class Conversation
    {
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private NpcCharacter _npc;
        [SerializeField] private NpcRecord _record;
        [SerializeField] private ConversationTreeBuilderConfig _config;

        public NpcCharacter Npc => _npc;
        public PlayerCharacter Player => _player;
        public NpcRecord Record => _record;

        public ConversationTree CurrentConversationTree { get; private set; }
        public bool IsOver { get; set; }

        public Conversation( NpcCharacter npc, NpcRecord record, ConversationTreeBuilderConfig config, PlayerCharacter player )
        {
            _npc = npc;
            _record = record;
            _config = config;
            _player = player;
        }

        public ConversationTree GenerateNextConversationTree() => CurrentConversationTree = new ConversationTree( _record, _config );
    }
}