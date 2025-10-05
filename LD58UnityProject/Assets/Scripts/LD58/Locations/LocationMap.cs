using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LD58.Locations
{
    public class LocationMap : MonoBehaviour
    {
        [SerializeField] private House[] _housePrefabs;
        [SerializeField] private LocationSpot[] _spots;

        private HashSet<LocationSpot> TakenSpots { get; } = new();

        public House InstantiateHouseInRandomSpot()
        {
            var randomHousePrefab = _housePrefabs[ Random.Range( 0, _housePrefabs.Length ) ];

            return InstantiateInRandomSpot( randomHousePrefab );
        }

        public T InstantiateInRandomSpot<T>( T prefab ) where T : Object
        {
            var spot = _spots.Where( t => !TakenSpots.Contains( t ) ).OrderBy( _ => Random.value ).FirstOrDefault();

            if( !spot )
            {
                return default;
            }

            TakenSpots.Add( spot );

            return Instantiate( prefab, spot.transform );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            foreach( var spot in _spots )
            {
                Gizmos.DrawSphere( spot.transform.position, 0.1f );
            }
        }

        [ContextMenu( "Gather Spots" )]
        private void GatherSpots() => _spots = GetComponentsInChildren<LocationSpot>();
    }
}