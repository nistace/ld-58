using UnityEngine;

namespace LD58.Characters.PlayerCharacters
{
    [RequireComponent( typeof(Character) )]
    public class PlayerCharacter : MonoBehaviour, ICharacterConversationHandler
    {
        [SerializeField] private Character _character;

        public Character Character => _character;

        private void Reset()
        {
            _character = GetComponent<Character>();
        }

        public bool IsTalking { get; set; }
        public bool IsBusy => IsTalking;
    }
}