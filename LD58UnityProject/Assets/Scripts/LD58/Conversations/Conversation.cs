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

        public NpcCharacter Npc => _npc;

        public NpcRecord Record => _record;

        public Conversation( NpcCharacter npc, NpcRecord record )
        {
            _npc = npc;
            _record = record;
        }
    }
}