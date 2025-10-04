using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters.NpcBehaviours {
   public class NpcGoHomeBehaviour : NpcBehaviour {
      public override bool CheckConditions(NpcInfo info) => true;
      public override Vector3 GetTargetLocation(NpcInfo info) => info.Home.Entrance.position;
   }
}
