using UnityEngine;

namespace LD58.Locations
{
    [RequireComponent( typeof(Location) )]
    public class House : MonoBehaviour
    {
        [SerializeField] private Location _location;
        [SerializeField] private HousePrestige _prestige;

        private void Reset()
        {
            _location = GetComponent<Location>();
        }

        public Transform Entrance => _location.Entrance;
    }
}