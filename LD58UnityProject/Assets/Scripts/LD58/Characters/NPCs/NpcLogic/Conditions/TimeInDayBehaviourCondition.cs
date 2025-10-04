using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters.NpcBehaviours {
   public class TimeInDayBehaviourCondition : NpcBehaviourCondition {
      [ SerializeField ] private float _minTimeInDayNormalized = .2f;
      [ SerializeField ] private float _maxTimeInDayNormalized = .5f;

      public override bool Check(NpcInfo npcInfo) {
         return GameTimeManager.TimeInDayNormalized >= _minTimeInDayNormalized && GameTimeManager.TimeInDayNormalized <= _maxTimeInDayNormalized;
      }
   }
}
