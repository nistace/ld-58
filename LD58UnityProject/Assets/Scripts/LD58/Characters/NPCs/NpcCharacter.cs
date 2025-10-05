using LD58.Characters.Data;
using LD58.Characters.NpcBehaviours;
using LD58.Misc;
using LD58.Records;
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
        [SerializeField] private NpcRecord.EInformation _revealingInformation;

        public Vector3 NormalVelocity { get; private set; }
        public NpcBrain Brain => _brain;
        public Character Character => _character;

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

            var hasBehaviour = Brain.TryPickBehaviour( _info, out var behaviour );

            if( hasBehaviour )
            {
                targetPosition = behaviour.GetTargetLocation( Info );
            }

            MoveTowards( targetPosition, deltaTime, out var atDestination );

            if( atDestination && hasBehaviour )
            {
                behaviour.AddTimeAtDestination( Info, deltaTime );
            }

            var visible = !hasBehaviour;
            visible |= !atDestination;
            visible |= behaviour && !behaviour.CanGoInside( Info );

            gameObject.SetActive( visible );
        }

        public void MoveTowards( Vector3 destination, float deltaTime, out bool atDestination )
        {
            NormalVelocity = Vector3.ClampMagnitude( destination - transform.position, 1 );
            transform.position = Vector3.MoveTowards( transform.position, destination, _character.CharacterConfig.MovementSpeed * deltaTime );

            atDestination = transform.position == destination;
        }

        public bool HasBehaviour( NpcBehaviour npcBehaviour ) => Brain.HasBehaviour( npcBehaviour );

        public bool IsRevealingInformation( out NpcRecord.EInformation revealingInformation, out float revealingDuration )
        {
            revealingInformation = default;
            revealingDuration = .5f;

            if( !gameObject.activeSelf ) return false;
            if( !Brain.TryPickBehaviour( _info, out var behaviour ) ) return false;
            if( transform.position != behaviour.GetTargetLocation( Info ) ) return false;

            return behaviour.CanRevealInformation( out revealingInformation, out revealingDuration );
        }
    }
}