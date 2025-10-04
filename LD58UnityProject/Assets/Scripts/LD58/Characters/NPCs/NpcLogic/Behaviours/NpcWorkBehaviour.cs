using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters.NpcBehaviours {
   public class NpcWorkBehaviour : NpcBehaviour {
      public override bool CheckConditions(NpcInfo info) => info.Job.JobDefinition.IsWithinWorkingHours(GameTimeManager.TimeInDayNormalized);
      public override Vector3 GetTargetLocation(NpcInfo info) => info.Job.Location.Entrance.position;
   }
}
