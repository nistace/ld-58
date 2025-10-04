using System.Linq;
using LD58.Characters;
using LD58.Jobs;
using LD58.Locations;
using UnityEngine;
namespace LD58.GameControllers {
   public class GameController : MonoBehaviour {
      [ SerializeField ] private LocationMap _locationMap;
      [ SerializeField ] private NPCManager _npcManager;
      [ SerializeField ] private JobDefinition[] _allJobs;
      [ SerializeField ] private int _npcCountToGenerate = 2;

      private void Start() {
         SetUpGame();
      }

      private void SetUpGame() {
         var randomJobsForNPCs = _allJobs.OrderBy(_ => Random.value).Take(_npcCountToGenerate).ToArray();

         for (var npcIndex = 0; npcIndex < _npcCountToGenerate; ++npcIndex) {
            var npcJobDefinition = randomJobsForNPCs[npcIndex];
            var job = new Job(npcJobDefinition, _locationMap.InstantiateLocationInRandomSpot(npcJobDefinition.LocationPrefab));
            var house = _locationMap.InstantiateHouseInRandomSpot();

            _npcManager.CreateNPC(house, job, 0);
         }
      }
   }
}
