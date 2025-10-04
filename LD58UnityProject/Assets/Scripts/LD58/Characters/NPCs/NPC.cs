using System;
using LD58.Characters.Data;
using UnityEngine;
namespace LD58.Characters {
   [ Serializable ]
   public class NPC {
      [ SerializeField ] private NPCCharacter _npcCharacter;

      [ SerializeField ] private NpcInfo _info;

      public NPC(NpcInfo info, NPCCharacter npcCharacter) {
         _info = info;
         _npcCharacter = npcCharacter;
      }

      public void Tick(float deltaTime) {
         _info.CurrentLocation = _npcCharacter.transform.position;

         var behaviour = _npcCharacter.Brain.PickBehaviour(_info);
         var targetPosition = behaviour.GetTargetLocation(_info);

         _npcCharacter.Move(Vector3.ClampMagnitude(targetPosition - _info.CurrentLocation, 1), deltaTime);
      }
   }
}
