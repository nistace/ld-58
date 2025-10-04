using UnityEngine;

namespace LD58.Characters.PlayerCharacters
{
    [RequireComponent( typeof(PlayerCharacter) )]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _playerCharacter;

        protected PlayerCharacter PlayerCharacter => _playerCharacter;
        protected Character Character => PlayerCharacter.Character;
        protected CharacterConfig CharacterConfig => Character.CharacterConfig;

        private void Reset()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
        }
    }
}