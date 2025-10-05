using LD58.Characters.Data;
using LD58.Locations;

namespace LD58.Characters.NpcBehaviours
{
    public class NpcGoToRestaurantBehaviour : NpcBehaviour
    {
        public override Location GetLocation( NpcInfo info ) => info.Restaurant.PresetLocation.Location;
    }
}