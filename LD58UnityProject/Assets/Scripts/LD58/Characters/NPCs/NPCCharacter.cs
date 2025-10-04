using LD58.Characters.NpcBehaviours;
using UnityEngine;
namespace LD58.Characters {
   [ RequireComponent(typeof(Character)) ]
   public class NPCCharacter : MonoBehaviour, ICharacterMovementHandler {
      [ SerializeField ] private Character _character;
      [ SerializeField ] private NpcBrain _brain;

      public Vector3 NormalVelocity { get; private set; }
      public NpcBrain Brain => _brain;

      private void Reset() {
         _character = GetComponent<Character>();
         _brain = GetComponent<NpcBrain>();
      }

      public void Move(Vector3 normalVelocity, float deltaTime) {
         NormalVelocity = normalVelocity;
         transform.position += NormalVelocity * (_character.CharacterConfig.MovementSpeed * deltaTime);
      }
   }
}
