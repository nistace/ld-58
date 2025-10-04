using LD58.Characters.NpcBehaviours;
using System;
using UnityEngine;

namespace LD58.Records
{
    [Serializable]
    public class NpcRecordedRoutine
    {
        [SerializeField] private NpcBehaviour _npcBehaviour;

        public NpcRecordedRoutine( NpcBehaviour npcBehaviour )
        {
            _npcBehaviour = npcBehaviour;
        }
    }
}