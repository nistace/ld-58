using UnityEngine;

namespace LD58.Locations
{
    [RequireComponent( typeof(Location) )]
    public class House : MonoBehaviour
    {
        [SerializeField] private Location _location;
        [SerializeField] private HousePrestige _prestige;

        public Location Location => _location;

        private void Reset()
        {
            _location = GetComponent<Location>();
        }

        public Transform Entrance => _location.Entrance;
        public HousePrestige Prestige => _prestige;
    }
}