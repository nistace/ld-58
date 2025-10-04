using UnityEngine;
using UnityEngine.InputSystem;

namespace LD58.Characters.PlayerCharacters {
   [ RequireComponent(typeof(Character)) ]
   public class CharacterMovementInput : MonoBehaviour, ICharacterMovementHandler {
      [ SerializeField ] private Character _character;
      [ SerializeField ] private InputActionReference _moveInputActionReference;

      public Vector3 NormalVelocity { get; private set; }

      private void Reset() {
         _character = GetComponent<Character>();
      }

      private void FixedUpdate() {
         var input = _moveInputActionReference.action.ReadValue<Vector2>();

         if (input == Vector2.zero) {
            NormalVelocity = Vector3.zero;

            return;
         }

         NormalVelocity = new Vector3(input.x, 0, input.y);

         transform.position += NormalVelocity * (_character.CharacterConfig.MovementSpeed * Time.fixedDeltaTime);
      }
   }
}
