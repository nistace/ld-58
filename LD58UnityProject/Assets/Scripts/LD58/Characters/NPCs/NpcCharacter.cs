using LD58.Characters.Data;
using LD58.Characters.NpcBehaviours;
using LD58.Misc;
using UnityEngine;

namespace LD58.Characters
{
    [RequireComponent( typeof(Character) )]
    public class NpcCharacter : MonoBehaviour, ICharacterMovementHandler, ICharacterConversationHandler

    {
        [SerializeField] private Character _character;
        [SerializeField] private NpcBrain _brain;
        [SerializeField] private ColorableSpritesHolder _outfitBodyParts;
        [SerializeField] private NpcInfo _info;
        public Vector3 NormalVelocity { get; private set; }
        public NpcBrain Brain => _brain;

        public ColorableGroupsConfiguration Outfit { get; private set; }

        public NpcInfo Info
        {
            get => _info;
            set => _info = value;
        }

        public bool IsTalking
        {
            get => _info.HasStates( NpcInfo.EStates.Talking );
            set => _info.ChangeStates( NpcInfo.EStates.Talking, value );
        }

        public void SetOutfit( ColorableGroupsConfiguration outfit )
        {
            Outfit = outfit;
            _outfitBodyParts.Colorize( Outfit );
        }

        private void Reset()
        {
            _character = GetComponent<Character>();
            _brain = GetComponent<NpcBrain>();
        }

        public void Tick( float deltaTime )
        {
            _info.CurrentLocation = transform.position;

            var targetPosition = _info.CurrentLocation;

            if( Brain.TryPickBehaviour( _info, out var behaviour ) )
            {
                targetPosition = behaviour.GetTargetLocation( Info );
            }

            MoveTowards( targetPosition, deltaTime );
        }

        public void MoveTowards( Vector3 destination, float deltaTime )
        {
            NormalVelocity = Vector3.ClampMagnitude( destination - transform.position, 1 );
            transform.position = Vector3.MoveTowards( transform.position, destination, _character.CharacterConfig.MovementSpeed * deltaTime );
        }

        public bool HasBehaviour( NpcBehaviour npcBehaviour ) => Brain.HasBehaviour( npcBehaviour );
    }
}