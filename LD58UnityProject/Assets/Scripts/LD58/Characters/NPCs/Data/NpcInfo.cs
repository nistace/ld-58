using System;
using LD58.Jobs;
using LD58.Locations;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
        [SerializeField] private Restaurant _restaurant;
        [SerializeField] private FoodType _foodType;
        [SerializeField] private int _money;
        [SerializeField] private Job _job;
        [SerializeField] private int _lastTaxDay = -10;
        [SerializeField] private int _lastTaxAmount = 0;

        public House Home => _home;
        public Restaurant Restaurant => _restaurant;
        public string Name => _name;
        public int Money => _money;
        public Job Job => _job;
        public Vector3 CurrentLocation { get; set; }
        public EStates State { get; private set; }
        public FoodType FoodType => _foodType;
        private Dictionary<Object, float> TimeNormalizedAtDestinationToday { get; } = new();
        private Object CurrentAction { get; set; }
        private float NormalizedTimeInCurrentAction { get; set; }

        public int LastTaxDay
        {
            get => _lastTaxDay;
            set => _lastTaxDay = value;
        }

        public int LastTaxAmount
        {
            get => _lastTaxAmount;
            set => _lastTaxAmount = value;
        }

        public NpcInfo( House home, int money, Job job, string name, Restaurant restaurant, FoodType foodType )
        {
            _home = home;
            _money = money;
            _job = job;
            _name = name;
            _restaurant = restaurant;
            _foodType = foodType;
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
            TimeNormalizedAtDestinationToday.Clear();
        }

        public void GetPaid() => _money += Job.Pay;

        public void AddTimeAtAction( Object key, float additionalTime )
        {
            TimeNormalizedAtDestinationToday.TryAdd( key, 0 );
            TimeNormalizedAtDestinationToday[ key ] += additionalTime;

            if( CurrentAction != key )
            {
                CurrentAction = key;
                NormalizedTimeInCurrentAction = 0;
            }

            NormalizedTimeInCurrentAction += additionalTime;
        }

        public float GetTimeNormalizedAtDestinationToday( Object key ) => TimeNormalizedAtDestinationToday.GetValueOrDefault( key );
        public float GetTimeNormalizedForCurrentAction( Object ifKey ) => CurrentAction == ifKey ? NormalizedTimeInCurrentAction : 0;
    }
}