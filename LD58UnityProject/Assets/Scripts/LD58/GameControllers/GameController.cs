using System.Linq;
using LD58.Characters;
using LD58.Characters.Data;
using LD58.Conversations;
using LD58.Jobs;
using LD58.Locations;
using LD58.Misc;
using LD58.Records;
using LD58.Taxes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace LD58.GameControllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ConversationManager _conversationManager;
        [SerializeField] private LocationMap _locationMap;
        [SerializeField] private NpcManager _npcManager;
        [SerializeField] private JobDefinition[] _allJobs;
        [SerializeField] private ColorableGroupsConfiguration[] _allNpcOutfits;
        [SerializeField] private TaxRulesConfig _taxRulesConfig;
        [SerializeField] private string[] _npcNames;
        [FormerlySerializedAs( "_restaurants" )]
        [SerializeField] private int _restaurantsCount = 2;
        [SerializeField] private int _npcCountToGenerate = 2;
        [SerializeField] private int _differentJobs = 3;
        [SerializeField] private FoodType[] _foodTypes;
        [SerializeField] private int _foodTypesToUse = 10;
        [SerializeField] private PresetLocation[] _presetLocations;

        [SerializeField] private OutroUi _outroUi;

        private readonly Dictionary<PresetLocationIdentifier, HashSet<Location>> _identifiedLocations = new();

        private void Start()
        {
            SetUpGame();
        }

        private void IdentifyLocation( PresetLocation location )
        {
            if( !_identifiedLocations.TryGetValue( location.Identifier, out var locations ) )
            {
                locations = new HashSet<Location>();
                _identifiedLocations.Add( location.Identifier, locations );
            }

            locations.Add( location.Location );
        }

        private void Update()
        {
            if( GameTimeManager.Day == 11 && GameTimeManager.TimeScale > 0 )
            {
                GameTimeManager.TimeScale = 0;
                _conversationManager.EndAny();
                _outroUi.FillInAndShow();
            }
        }

        private void SetUpGame()
        {
            _identifiedLocations.Clear();

            foreach( var presetLocation in _presetLocations )
            {
                IdentifyLocation( presetLocation );
            }

            FoodRecord.Current = new FoodRecord( _foodTypes.OrderBy( _ => Random.value ).Take( _foodTypesToUse ).ToList() );

            var restaurants = FindObjectsByType<Restaurant>( FindObjectsInactive.Exclude, FindObjectsSortMode.None ).ToList();

            while( restaurants.Count < _restaurantsCount )
            {
                var restaurant = _locationMap.InstantiateRestaurantInRandomSpot();
                restaurants.Add( restaurant );
                IdentifyLocation( restaurant.PresetLocation );
            }

            var foodsPerRestaurant = _foodTypesToUse / restaurants.Count;

            for( var i = 0; i < restaurants.Count; i++ )
            {
                restaurants[ i ]
                    .Initialize( FoodRecord
                        .Current
                        .ActiveFoodTypes
                        .Skip( i * foodsPerRestaurant )
                        .Take( foodsPerRestaurant + ( i == _restaurantsCount - 1 ? _foodTypesToUse % _restaurantsCount : 0 ) )
                        .ToArray()
                    );
            }

            Dictionary<JobDefinition, Location> randomNpcJobDefinitions = new();

            foreach( var jobDefinition in _allJobs.OrderBy( _ => Random.value ).Take( _differentJobs ) )
            {
                if( jobDefinition.PresetLocation && _identifiedLocations.TryGetValue( jobDefinition.PresetLocation, out var locations ) && locations.Count > 0 )
                {
                    randomNpcJobDefinitions.Add( jobDefinition, locations.OrderBy( _ => Random.value ).First() );
                }
                else
                {
                    var newLocation = _locationMap.InstantiateInRandomSpot( jobDefinition.LocationPrefab );

                    if( newLocation.TryGetComponent<PresetLocation>( out var presetLocation ) )
                    {
                        IdentifyLocation( presetLocation );
                    }

                    randomNpcJobDefinitions.Add( jobDefinition, newLocation );
                }
            }

            JobRecord.Current = new JobRecord( randomNpcJobDefinitions.Keys.ToList() );

            var jobs = new List<Job>();

            while( jobs.Count < _npcCountToGenerate )
            {
                jobs.AddRange( randomNpcJobDefinitions.OrderBy( _ => Random.value ).Select( t => new Job( t.Key, t.Value ) ) );
            }

            var regimes = new List<(Restaurant restaurant, FoodType food)>();

            while( regimes.Count < _npcCountToGenerate )
            {
                regimes.AddRange( restaurants.OrderBy( _ => Random.value ).Select( t => ( t, t.FoodTypes.OrderBy( _ => Random.value ).First() ) ).ToArray() );
            }

            var randomNpcOutfits = _allNpcOutfits.OrderBy( _ => Random.value ).Take( _npcCountToGenerate ).ToArray();
            var randomNpcNames = _npcNames.OrderBy( _ => Random.value ).Take( _npcCountToGenerate ).ToArray();

            for( var npcIndex = 0; npcIndex < _npcCountToGenerate; ++npcIndex )
            {
                var job = jobs[ npcIndex ];
                var outfit = randomNpcOutfits[ npcIndex ];
                var npcName = randomNpcNames[ npcIndex ];
                var house = _locationMap.InstantiateHouseInRandomSpot();
                var regime = regimes[ npcIndex ];
                var info = new NpcInfo( house, 0, job, npcName, regime.restaurant, regime.food );

                _npcManager.CreateNpc( info, outfit );
            }

            TaxRules.Current = _taxRulesConfig.RandomRules( _locationMap.GetDistinctHousePrestigeLevels(), FoodRecord.Current.ActiveFoodTypes );
            TaxRecordTracker.Current = new TaxRecordTracker();
        }
    }
}