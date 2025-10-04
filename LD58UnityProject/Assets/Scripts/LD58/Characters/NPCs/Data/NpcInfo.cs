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

        [SerializeField] private Location _home;
        [SerializeField] private int _money;
        [SerializeField] private Job _job;

        public Location Home => _home;
        public int Money => _money;
        public Job Job => _job;
        public Vector3 CurrentLocation { get; set; }
        public EStates State { get; private set; }

        public NpcInfo( Location home, int money, Job job )
        {
            _home = home;
            _money = money;
            _job = job;
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
    }
}