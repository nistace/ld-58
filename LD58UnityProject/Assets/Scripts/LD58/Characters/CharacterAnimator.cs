using UnityEngine;
namespace LD58.Characters {
   public class CharacterAnimator : MonoBehaviour {
      private static readonly int speedAnimParam = Animator.StringToHash("Speed");
      private static readonly int speedXAnimParam = Animator.StringToHash("SpeedX");

      [ SerializeField ] private Animator _animator;

      private ICharacterMovementHandler characterMovementHandler;

      private void Start() {
         characterMovementHandler = gameObject.GetComponentInParent<ICharacterMovementHandler>();
      }

      private void LateUpdate() {
         _animator.SetFloat(speedAnimParam, characterMovementHandler?.NormalVelocity.magnitude ?? 0);
         _animator.SetFloat(speedXAnimParam, characterMovementHandler?.NormalVelocity.x ?? 0);
      }
   }
}
