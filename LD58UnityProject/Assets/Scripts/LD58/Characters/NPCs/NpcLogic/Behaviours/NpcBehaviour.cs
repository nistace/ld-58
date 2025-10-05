using LD58.Characters.Data;
using LD58.Locations;
using LD58.Records;
using System.Linq;
using UnityEngine;

namespace LD58.Characters.NpcBehaviours
{
    public abstract class NpcBehaviour : MonoBehaviour
    {
        [SerializeField] private NpcBehaviourCondition[] _conditions;
        [SerializeField] private bool _revealsInformation;
        [SerializeField] private NpcRecord.EInformation _revealedInformation;
        [SerializeField] private float _revealDuration = 1;
        [SerializeField] private bool _canGoIn;
        [SerializeField] protected float _canGoInAfterNormalizedDuration = .05f;

        public abstract Location GetLocation( NpcInfo info );
        public virtual bool CheckConditions( NpcInfo info ) => _conditions.All( t => t.Check( info ) );
        public Vector3 GetTargetLocation( NpcInfo info ) => GetLocation( info ).Entrance.position;
        public void AddTimeAtDestination( NpcInfo info, float deltaTime ) => info.AddTimeAtAction( this, deltaTime / GameTimeManager.TimeInDay );

        public virtual bool CanGoInside( NpcInfo info ) =>
            _canGoIn && GetLocation( info ).CanEnter && _canGoInAfterNormalizedDuration < info.GetTimeNormalizedForCurrentAction( this );

        public bool CanRevealInformation( out NpcRecord.EInformation information, out float duration )
        {
            information = _revealedInformation;
            duration = _revealDuration;

            return _revealsInformation;
        }
    }
}