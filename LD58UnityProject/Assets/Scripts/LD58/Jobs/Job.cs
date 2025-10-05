using System;
using LD58.Locations;
using UnityEngine;

namespace LD58.Jobs
{
    [Serializable]
    public class Job
    {
        [SerializeField] private JobDefinition _jobDefinition;
        [SerializeField] private Location _location;
        [SerializeField] private int _pay;
        [SerializeField] private float _startNormalizedTime;
        [SerializeField] private float _endNormalizedTime;

        public JobDefinition JobDefinition => _jobDefinition;
        public Location Location => _location;

        public bool IsWithinWorkingHours( float timeInDayNormalized ) => timeInDayNormalized <= _endNormalizedTime && timeInDayNormalized >= _startNormalizedTime;

        public Job( JobDefinition jobDefinition, Location location )
        {
            _jobDefinition = jobDefinition;
            _location = location;
            jobDefinition.GetRandomWorkingHours( out _startNormalizedTime, out _endNormalizedTime );
        }
    }
}