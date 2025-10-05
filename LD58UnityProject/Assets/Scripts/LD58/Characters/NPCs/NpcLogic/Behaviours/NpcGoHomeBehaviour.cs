using LD58.Characters.Data;
using LD58.Locations;

namespace LD58.Characters.NpcBehaviours
{
    public class NpcGoHomeBehaviour : NpcBehaviour
    {
        public override Location GetLocation( NpcInfo info ) => info.Home.Location;

        public override bool CanGoInside( NpcInfo info )
        {
            return base.CanGoInside( info ) || GameTimeManager.TimeInDayNormalized < _canGoInAfterNormalizedDuration;
        }
    }
}