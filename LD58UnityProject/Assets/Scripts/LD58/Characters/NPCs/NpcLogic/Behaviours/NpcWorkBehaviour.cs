using LD58.Characters.Data;
using UnityEngine;

namespace LD58.Characters.NpcBehaviours
{
    public class NpcWorkBehaviour : NpcBehaviour
    {
        public override bool CheckConditions( NpcInfo info ) => info.Job.JobDefinition.IsWithinWorkingHours( GameTimeManager.TimeInDayNormalized );
        public override Vector3 GetTargetLocation( NpcInfo info ) => info.Job.Location.Entrance.position;
        public override string ToDisplayString() => "Working";

        public override void ActAtDestination( NpcInfo info, float deltaTime )
        {
            if( info.WorkedEnoughToday ) return;

            info.WorkTimeTodayNormalized += deltaTime / GameTimeManager.TimeInDay;

            if( info.WorkedEnoughToday )
            {
                info.GetPaid();
            }
        }
    }
}