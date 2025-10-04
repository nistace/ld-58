using System.Collections.Generic;
using LD58.Characters.Data;
using LD58.Jobs;
using LD58.Locations;
using Unity.Mathematics;
using UnityEngine;
namespace LD58.Characters {
   public class NPCManager : MonoBehaviour {
      [ SerializeField ] private NPCCharacter _npcCharacterPrefab;

      private List<NPC> NPCs { get; } = new();

      public NPC CreateNPC(Location home, Job job, int money) {
         var npcCharacter = Instantiate(_npcCharacterPrefab, home.Entrance.position, quaternion.identity);
         var npcInfo = new NpcInfo(home, money, job);
         var newNpc = new NPC(npcInfo, npcCharacter);

         NPCs.Add(newNpc);

         return newNpc;
      }

      private void Update() {
         foreach (var npc in NPCs) {
            npc.Tick(Time.deltaTime);
         }
      }
   }
}
