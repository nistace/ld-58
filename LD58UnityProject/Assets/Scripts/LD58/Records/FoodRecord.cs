using LD58.Locations;
using LD58.Taxes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace LD58.Records
{
    [Serializable]
    public class FoodRecord
    {
        private static FoodRecord current;

        public static FoodRecord Current
        {
            get => current;
            set
            {
                current = value;
                OnChanged.Invoke();
            }
        }

        public IReadOnlyList<FoodType> ActiveFoodTypes { get; }
        private HashSet<FoodType> KnownPrices { get; } = new();

        public static UnityEvent OnChanged { get; } = new();

        public FoodRecord( IEnumerable<FoodType> activeFoodTypes )
        {
            ActiveFoodTypes = activeFoodTypes.ToArray();
        }

        public void RegisterFood( FoodType foodType )
        {
            if( !KnownPrices.Add( foodType ) ) return;

            OnChanged.Invoke();
        }

        public void ClearKnownPrices()
        {
            if( KnownPrices.Count == 0 ) return;

            KnownPrices.Clear();
            OnChanged.Invoke();
        }

        public bool IsKnown( FoodType food ) => KnownPrices.Contains( food );

        public string TryGetKnownPrice( FoodType foodType, string defaultValue = "?" ) =>
            KnownPrices.Contains( foodType ) ? $"{TaxRules.Current.FoodTaxes[ foodType ]}" : defaultValue;
    }
}