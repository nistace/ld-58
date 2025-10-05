using LD58.Interactables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD58.Characters.PlayerCharacters
{
    public class PlayerInteractableDetector : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;
        private readonly Dictionary<Collider, IInteractable> _interactablesInRange = new();

        public bool TryGetInteractable( out IInteractable closest )
        {
            closest = _interactablesInRange
                .Values
                .Where( t => t.IsInteractable )
                .Where( t => Vector3.SqrMagnitude( t.Position - _sphereCollider.transform.position ) < _sphereCollider.radius * _sphereCollider.radius )
                .OrderBy( t => -t.PriorityFactor )
                .ThenBy( t => Vector3.SqrMagnitude( transform.position - t.Position ) )
                .FirstOrDefault();

            return closest != null;
        }

        private void OnTriggerEnter( Collider other )
        {
            var interactable = other.gameObject.GetComponentInParent<IInteractable>();

            if( interactable != null )
            {
                _interactablesInRange[ other ] = interactable;
            }
        }

        private void OnTriggerExit( Collider other ) => _interactablesInRange.Remove( other );
    }
}