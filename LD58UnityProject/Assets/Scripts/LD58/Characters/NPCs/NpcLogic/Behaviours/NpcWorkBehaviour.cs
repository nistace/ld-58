using LD58.Characters.Data;
using LD58.Locations;

namespace LD58.Characters.NpcBehaviours
{
    public class NpcWorkBehaviour : NpcBehaviour
    {
        public override Location GetLocation( NpcInfo info ) => info.Job.Location;
        public override bool CheckConditions( NpcInfo info ) => base.CheckConditions( info ) && info.Job.IsWithinWorkingHours( GameTimeManager.TimeInDayNormalized );
    }
}