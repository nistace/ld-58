using LD58.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LD58.Characters.PlayerCharacters
{
    public class PlayerInteractInput : PlayerInputHandler
    {
        [SerializeField] private PlayerInteractableDetector _interactableDetector;
        [SerializeField] private InputActionReference _interactInput;

        private void Start()
        {
            _interactInput.action.started += HandleInteractInputStarted;
        }

        private void OnDestroy()
        {
            _interactInput.action.started -= HandleInteractInputStarted;
        }

        private void HandleInteractInputStarted( InputAction.CallbackContext context )
        {
            if( !TryGetCurrentInteractTargetIfAllowed( out var closestInteractable ) ) return;

            closestInteractable.Interact();
        }

        public bool TryGetCurrentInteractTargetIfAllowed( out IInteractable interactable )
        {
            interactable = default;

            if( PlayerCharacter.IsBusy ) return false;

            return _interactableDetector.TryGetInteractable( out interactable );
        }
    }
}