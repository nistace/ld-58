using UnityEngine;

namespace LD58.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private Transform _entrance;
        [SerializeField] private bool _canEnter = true;

        public bool CanEnter => _canEnter;

        public Transform Entrance => _entrance;
    }
}