using System;
using LD58.Jobs;
using LD58.Locations;
using UnityEngine;

namespace LD58.Characters.Data
{
    [Serializable]
    public class NpcInfo
    {
        [Flags]
        public enum EStates
        {
            Talking = 1 << 0
        }

        [SerializeField] private string _name;
        [SerializeField] private House _home;
        [SerializeField] private int _money;
        [SerializeField] private Job _job;

        public House Home => _home;
        public string Name => _name;
        public int Money => _money;
        public Job Job => _job;
        public Vector3 CurrentLocation { get; set; }
        public EStates State { get; private set; }
        public float WorkTimeTodayNormalized { get; set; }
        public bool WorkedEnoughToday => WorkTimeTodayNormalized >= Job.JobDefinition.NeededTimeNormalizedToGetPaid;

        public NpcInfo( House home, int money, Job job, string name )
        {
            _home = home;
            _money = money;
            _job = job;
            _name = name;
        }

        public bool HasStates( EStates state ) => ( int )( State & state ) == ( int )state;

        public void ChangeStates( EStates states, bool enable )
        {
            if( enable )
            {
                State |= states;
            }
            else
            {
                State &= ~states;
            }
        }

        public void ResetDailyInformation()
        {
            WorkTimeTodayNormalized = 0;
        }

        public void GetPaid() => _money += Job.JobDefinition.Pay;
    }
}