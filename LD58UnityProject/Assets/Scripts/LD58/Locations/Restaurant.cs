using LD58.Interactables;
using LD58.Records;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD58.Locations
{
    [RequireComponent( typeof(PresetLocation) )]
    public class Restaurant : MonoBehaviour, IInteractable
    {
        [SerializeField] private PresetLocation _presetLocation;
        [SerializeField] private Transform _pricesInteractable;

        private Dictionary<FoodType, int> FoodPrices { get; } = new();
        public Vector3 Position => _pricesInteractable.position;
        public string InteractionDisplayText => "Take note";
        public Vector3 TooltipWorldAnchor => _pricesInteractable.position;
        public bool IsInteractable => FoodPrices.Count > 0 && !FoodRecord.Current.IsKnown( FoodPrices.Keys.First() );
        public float PriorityFactor => 20;
        public Transform Entrance => _presetLocation.Location.Entrance;
        public PresetLocation PresetLocation => _presetLocation;
        public IEnumerable<FoodType> FoodTypes => FoodPrices.Keys;

        private void Reset()
        {
            _presetLocation = GetComponent<PresetLocation>();
        }

        public void Initialize( IReadOnlyList<FoodType> foodTypes )
        {
            FoodPrices.Clear();

            foreach( var foodType in foodTypes )
            {
                FoodPrices.Add( foodType, foodType.RandomTax );
            }
        }

        public void Interact()
        {
            foreach( var foodPrice in FoodPrices )
            {
                FoodRecord.Current.RegisterFood( foodPrice.Key );
            }
        }
    }
}