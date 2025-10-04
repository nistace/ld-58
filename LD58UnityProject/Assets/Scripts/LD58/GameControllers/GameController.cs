using System.Linq;
using LD58.Characters;
using LD58.Jobs;
using LD58.Locations;
using LD58.Misc;
using UnityEngine;

namespace LD58.GameControllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LocationMap _locationMap;
        [SerializeField] private NpcManager _npcManager;
        [SerializeField] private JobDefinition[] _allJobs;
        [SerializeField] private ColorableGroupsConfiguration[] _allNpcOutfits;
        [SerializeField] private int _npcCountToGenerate = 2;

        private void Start()
        {
            SetUpGame();
        }

        private void SetUpGame()
        {
            var randomNpcJobs = _allJobs.OrderBy( _ => Random.value ).Take( _npcCountToGenerate ).ToArray();
            var randomNpcOutfits = _allNpcOutfits.OrderBy( _ => Random.value ).Take( _npcCountToGenerate ).ToArray();

            for( var npcIndex = 0; npcIndex < _npcCountToGenerate; ++npcIndex )
            {
                var npcJobDefinition = randomNpcJobs[ npcIndex ];
                var npcOutfit = randomNpcOutfits[ npcIndex ];
                var job = new Job( npcJobDefinition, _locationMap.InstantiateLocationInRandomSpot( npcJobDefinition.LocationPrefab ) );
                var house = _locationMap.InstantiateHouseInRandomSpot();

                _npcManager.CreateNpc( house, job, 0, npcOutfit );
            }
        }
    }
}