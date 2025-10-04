using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
namespace LD58.Locations {
   public class LocationMap : MonoBehaviour {
      [ SerializeField ] private Location _housePrefab;
      [ SerializeField ] private LocationSpot[] _spots;

      private HashSet<LocationSpot> TakenSpots { get; } = new();

      public Location InstantiateHouseInRandomSpot() => InstantiateLocationInRandomSpot(_housePrefab);

      public Location InstantiateLocationInRandomSpot(Location prefab) {
         var spot = _spots.Where(t => !TakenSpots.Contains(t)).OrderBy(_ => Random.value).FirstOrDefault();

         if (!spot) {
            return default;
         }

         TakenSpots.Add(spot);

         return Instantiate(prefab, spot.transform);
      }

      private void OnDrawGizmos() {
         Gizmos.color = Color.cyan;

         foreach (var spot in _spots) {
            Gizmos.DrawSphere(spot.transform.position, 0.1f);
         }
      }

      [ ContextMenu("Gather Spots") ]
      private void GatherSpots() => _spots = GetComponentsInChildren<LocationSpot>();
   }
}
