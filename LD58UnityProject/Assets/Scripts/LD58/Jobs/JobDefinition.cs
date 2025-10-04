using LD58.Locations;
using UnityEngine;
namespace LD58.Jobs {
   [ CreateAssetMenu ]
   public class JobDefinition : ScriptableObject {
      [ SerializeField ] private int _pay = 100;
      [ SerializeField ] private float _workingStartTimeNormalized = .2f;
      [ SerializeField ] private float _workingEndTimeNormalized = .6f;
      [ SerializeField ] private Location _locationPrefab;

      public int Pay => _pay;
      public Location LocationPrefab => _locationPrefab;

      public bool IsWithinWorkingHours(float timeInDayNormalized) => timeInDayNormalized <= _workingEndTimeNormalized && timeInDayNormalized >= _workingStartTimeNormalized;
   }
}
