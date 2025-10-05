using LD58.Characters.Data;
using LD58.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Taxes
{
    [Serializable]
    public class TaxRules
    {
        public static TaxRules Current
        {
            get => current;
            set
            {
                current = value;

                OnInitialized.Invoke();
            }
        }

        public static UnityEvent OnInitialized { get; } = new();

        public static int DaysBetweenTwoCollections => Current?._daysBetweenTwoCollections ?? 0;

        [SerializeField] private float _jobPayTax;
        [SerializeField] private int _daysBetweenTwoCollections;
        private static TaxRules current;

        public float JobPayTax => _jobPayTax;
        public IReadOnlyDictionary<HousePrestige, int> HousingTaxes { get; }
        public IReadOnlyDictionary<FoodType, int> FoodTaxes { get; }

        public TaxRules( float jobPayTax, int daysBetweenTwoCollections, IReadOnlyDictionary<HousePrestige, int> housingTaxes, IReadOnlyDictionary<FoodType, int> foodTaxes )
        {
            _jobPayTax = jobPayTax;
            _daysBetweenTwoCollections = daysBetweenTwoCollections;
            HousingTaxes = housingTaxes.ToDictionary( t => t.Key, t => t.Value );
            FoodTaxes = foodTaxes.ToDictionary( t => t.Key, t => t.Value );
        }

        public int EvaluateCorrectTax( NpcInfo npcInfo )
        {
            var tax = 0;
            tax += Mathf.CeilToInt( npcInfo.Job.Pay * _jobPayTax );
            tax += FoodTaxes[ npcInfo.FoodType ];
            tax += HousingTaxes[ npcInfo.Home.Prestige ];

            return tax;
        }
    }
}