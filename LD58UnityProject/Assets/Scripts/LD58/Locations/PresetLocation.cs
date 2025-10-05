using UnityEngine;

namespace LD58.Locations
{
    [RequireComponent( typeof(Location) )]
    public class PresetLocation : MonoBehaviour
    {
        [SerializeField] private Location _location;
        [SerializeField] private PresetLocationIdentifier _identifier;

        public PresetLocationIdentifier Identifier => _identifier;
        public Location Location => _location;

        private void Reset() => _location = GetComponent<Location>();
    }
}