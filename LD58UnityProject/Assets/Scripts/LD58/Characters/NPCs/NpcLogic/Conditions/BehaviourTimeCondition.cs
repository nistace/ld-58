using LD58.Characters.Data;
using UnityEngine;

namespace LD58.Characters.NpcBehaviours
{
    public class BehaviourTimeCondition : NpcBehaviourCondition
    {
        [SerializeField] private NpcBehaviour _behaviour;
        [SerializeField] private float _minNormalizedDurationDoingTask = 0;
        [SerializeField] private float _maxNormalizedDurationDoingTask = 1;

        public override bool Check( NpcInfo npcInfo )
        {
            var time = npcInfo.GetTimeNormalizedAtDestinationToday( _behaviour );

            return time >= _minNormalizedDurationDoingTask && time <= _maxNormalizedDurationDoingTask;
        }
    }
}