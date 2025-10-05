using LD58.Characters;
using LD58.Interactables;
using LD58.Records;
using UnityEngine;

namespace LD58.SpyingSystem
{
    public class NpcSpyingHint : MonoBehaviour, IInteractable
    {
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private bool _isRevealingInformation;
        [SerializeField] private NpcRecord.EInformation _revealingInformation;
        [SerializeField] private float _revealingUntilTime = -1;

        public NpcRecord NpcRecord { get; set; }
        private NpcCharacter SpiedCharacter => NpcRecord.Npc;

        private void Update()
        {
            if( SpiedCharacter.IsRevealingInformation( out var newRevealedInformation, out var revealingDuration ) && !NpcRecord.IsKnown( newRevealedInformation ) )
            {
                _isRevealingInformation = true;
                _revealingInformation = newRevealedInformation;
                _revealingUntilTime = Time.time + revealingDuration;
                transform.position = SpiedCharacter.transform.position;
            }

            _sphereCollider.enabled = Time.time < _revealingUntilTime;
        }

        public Vector3 Position => transform.position;
        public string InteractionDisplayText => "Take note";
        public Vector3 TooltipWorldAnchor => transform.position;
        public bool IsInteractable => _isRevealingInformation && Time.time < _revealingUntilTime && !NpcRecord.IsKnown( _revealingInformation );
        public float PriorityFactor => 50;

        public void Interact()
        {
            NpcRecord.Learn( _revealingInformation );
        }
    }
}