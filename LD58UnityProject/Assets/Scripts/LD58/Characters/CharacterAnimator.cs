using UnityEngine;

namespace LD58.Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int speedAnimParam = Animator.StringToHash( "Speed" );
        private static readonly int speedXAnimParam = Animator.StringToHash( "SpeedX" );
        private static readonly int talkingAnimParam = Animator.StringToHash( "Talking" );

        [SerializeField] private Animator _animator;

        private ICharacterMovementHandler _characterMovementHandler;
        private ICharacterConversationHandler _characterConversationHandler;

        private void Start()
        {
            _characterMovementHandler = gameObject.GetComponentInParent<ICharacterMovementHandler>();
            _characterConversationHandler = gameObject.GetComponentInParent<ICharacterConversationHandler>();
        }

        private void LateUpdate()
        {
            _animator.SetFloat( speedAnimParam, _characterMovementHandler?.NormalVelocity.magnitude ?? 0 );
            _animator.SetFloat( speedXAnimParam, _characterMovementHandler?.NormalVelocity.x ?? 0 );
            _animator.SetBool( talkingAnimParam, _characterConversationHandler?.IsTalking ?? false );
        }
    }
}