using System;
using LD58.Characters.Data;
using UnityEngine;

namespace LD58.Characters
{
    [Serializable]
    public class Npc
    {
        [SerializeField] private NpcCharacter _npcCharacter;

        [SerializeField] private NpcInfo _info;

        public Npc( NpcInfo info, NpcCharacter npcCharacter )
        {
            _info = info;
            _npcCharacter = npcCharacter;
        }

        public void Tick( float deltaTime )
        {
            _info.CurrentLocation = _npcCharacter.transform.position;

            var targetPosition = _info.CurrentLocation;

            if( _npcCharacter.Brain.TryPickBehaviour( _info, out var behaviour ) )
            {
                targetPosition = behaviour.GetTargetLocation( _info );
            }

            _npcCharacter.MoveTowards( targetPosition, deltaTime );
        }
    }
}