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
        [SerializeField] private Restaurant[] _restaurantPrefabs;
        [SerializeField] private LocationSpot[] _spots;

        private HashSet<LocationSpot> TakenSpots { get; } = new();

        public House InstantiateHouseInRandomSpot() => InstantiateRandomInRandomSpot( _housePrefabs );
        public Restaurant InstantiateRestaurantInRandomSpot() => InstantiateRandomInRandomSpot( _restaurantPrefabs );

        private T InstantiateRandomInRandomSpot<T>( T[] items ) where T : Object
        {
            var randomHousePrefab = items[ Random.Range( 0, items.Length ) ];

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

        public HousePrestige[] GetDistinctHousePrestigeLevels() => _housePrefabs.Select( t => t.Prestige ).Distinct().ToArray();
    }
}