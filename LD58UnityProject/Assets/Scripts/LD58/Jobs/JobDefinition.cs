using LD58.Locations;
using UnityEngine;

namespace LD58.Jobs
{
    [CreateAssetMenu]
    public class JobDefinition : ScriptableObject
    {
        [SerializeField] private int[] _possiblePays = { 50, 60, 70 };
        [SerializeField] private float[] _workingStartTimeNormalized = { .2f, .3f, .4f };
        [SerializeField] private float[] _workingEndTimeNormalized = { .6f, .7f, .8f };
        [SerializeField] private PresetLocationIdentifier _presetLocation;
        [SerializeField] private Location _locationPrefab;

        public string JobName => name;
        public int RandomPay => _possiblePays[ Random.Range( 0, _possiblePays.Length ) ];
        public PresetLocationIdentifier PresetLocation => _presetLocation;
        public Location LocationPrefab => _locationPrefab;

        public void GetRandomWorkingHours( out float start, out float end )
        {
            var index = Random.Range( 0, _workingStartTimeNormalized.Length );

            start = _workingStartTimeNormalized[ index ];
            end = _workingEndTimeNormalized[ index ];
        }
    }
}