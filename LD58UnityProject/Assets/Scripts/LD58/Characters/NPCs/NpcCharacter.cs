using LD58.Characters.NpcBehaviours;
using UnityEngine;

namespace LD58.Characters
{
    [RequireComponent( typeof(Character) )]
    public class NpcCharacter : MonoBehaviour, ICharacterMovementHandler
    {
        [SerializeField] private Character _character;
        [SerializeField] private NpcBrain _brain;
        [SerializeField] private ColorableGroupsHolder _outfitBodyParts;

        public Vector3 NormalVelocity { get; private set; }
        public NpcBrain Brain => _brain;

        public ColorableGroupsHolder OutfitBodyParts => _outfitBodyParts;

        private void Reset()
        {
            _character = GetComponent<Character>();
            _brain = GetComponent<NpcBrain>();
        }

        public void MoveTowards( Vector3 destination, float deltaTime )
        {
            NormalVelocity = Vector3.ClampMagnitude( destination - transform.position, 1 );
            transform.position = Vector3.MoveTowards( transform.position, destination, _character.CharacterConfig.MovementSpeed * deltaTime );
        }
    }
}