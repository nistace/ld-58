using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters.NpcBehaviours {
   public class NpcBrain : MonoBehaviour {
      [ SerializeField ] private NpcBehaviour[] _behaviours;
      [ SerializeField ] private NpcBehaviour _defaultBehaviour;

      public NpcBehaviour PickBehaviour(NpcInfo info) {
         foreach (var npcBehaviour in _behaviours) {
            if (npcBehaviour.CheckConditions(info)) {
               return npcBehaviour;
            }
         }

         return _defaultBehaviour;
      }
   }
}
