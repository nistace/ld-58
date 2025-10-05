using LD58.Locations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD58.Taxes
{
    [CreateAssetMenu]
    public class TaxRulesConfig : ScriptableObject
    {
        [SerializeField] private float[] _jobPayTaxRules = { .1f, .2f, .3f, .4f };
        [SerializeField] private int _daysBetweenTwoCollections = 3;

        public float RandomJobPayTaxRule => _jobPayTaxRules[ Random.Range( 0, _jobPayTaxRules.Length ) ];

        public TaxRules RandomRules( IEnumerable<HousePrestige> housePrestigeLevels, IEnumerable<FoodType> foodTypes ) =>
            new( RandomJobPayTaxRule,
                _daysBetweenTwoCollections,
                housePrestigeLevels.ToDictionary( t => t, t => t.RandomTax ),
                foodTypes.ToDictionary( t => t, t => t.RandomTax )
            );
    }
}