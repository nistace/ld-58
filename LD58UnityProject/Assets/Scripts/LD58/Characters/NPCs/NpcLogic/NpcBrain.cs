using LD58.Characters.Data;
using UnityEngine;

namespace LD58.Characters.NpcBehaviours
{
    public class NpcBrain : MonoBehaviour
    {
        [SerializeField] private NpcBehaviour[] _behaviours;
        [SerializeField] private NpcBehaviour _defaultBehaviour;

        public bool TryPickBehaviour( NpcInfo info, out NpcBehaviour behaviour )
        {
            behaviour = _defaultBehaviour;

            if( info.HasStates( NpcInfo.EStates.Talking ) )
            {
                return false;
            }

            foreach( var npcBehaviour in _behaviours )
            {
                if( npcBehaviour.CheckConditions( info ) )
                {
                    behaviour = npcBehaviour;

                    return true;
                }
            }

            return true;
        }
    }
}