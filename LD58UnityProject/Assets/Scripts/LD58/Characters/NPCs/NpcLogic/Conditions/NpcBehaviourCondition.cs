using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters.NpcBehaviours {
   public abstract class NpcBehaviourCondition : MonoBehaviour {
      public abstract bool Check(NpcInfo npcInfo);
   }
}
