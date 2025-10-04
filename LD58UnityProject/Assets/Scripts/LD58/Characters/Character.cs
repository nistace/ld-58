using UnityEngine;

namespace LD58.Characters
{
    public class Character : MonoBehaviour
    {
        public enum EStates
        {
            Talking = 1 << 0
        }

        [SerializeField] private CharacterConfig _characterConfig;

        public EStates States
        {
            get;
            set;
        }

        public CharacterConfig CharacterConfig => _characterConfig;
    }
}