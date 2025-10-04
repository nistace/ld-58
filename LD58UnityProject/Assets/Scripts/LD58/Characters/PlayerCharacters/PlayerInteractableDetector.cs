using LD58.Interactables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD58.Characters.PlayerCharacters
{
    public class PlayerInteractableDetector : MonoBehaviour
    {
        private readonly HashSet<IInteractable> _interactablesInRange = new();

        public bool TryGetInteractable( out IInteractable closest )
        {
            closest = _interactablesInRange.OrderBy( t => Vector3.SqrMagnitude( transform.position - t.Position ) ).FirstOrDefault();

            return closest != null;
        }

        private void OnTriggerEnter( Collider other )
        {
            if( other.gameObject.TryGetComponent<IInteractable>( out var interactable ) )
            {
                _interactablesInRange.Add( interactable );
            }
        }

        private void OnTriggerExit( Collider other )
        {
            _interactablesInRange.RemoveWhere( t => t.gameObject == other.gameObject );
        }
    }
}