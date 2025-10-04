using UnityEngine;
using UnityEngine.InputSystem;

namespace LD58.Characters.PlayerCharacters
{
    [RequireComponent( typeof(Character) )]
    public class CharacterMovementInput : PlayerInputHandler, ICharacterMovementHandler
    {
        [SerializeField] private InputActionReference _moveInputActionReference;

        public Vector3 NormalVelocity { get; private set; }

        private void FixedUpdate()
        {
            NormalVelocity = Vector3.zero;

            if( PlayerCharacter.IsBusy ) return;
            if( !TryGetNonZeroInput( out var input ) ) return;

            NormalVelocity = new Vector3( input.x, 0, input.y );

            transform.position += NormalVelocity * ( Character.CharacterConfig.MovementSpeed * Time.fixedDeltaTime );
        }

        private bool TryGetNonZeroInput( out Vector2 input )
        {
            input = _moveInputActionReference.action.ReadValue<Vector2>();

            return input != Vector2.zero;
        }
    }
}