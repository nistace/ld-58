using TMPro;
using UnityEngine;

namespace LD58.Characters.PlayerCharacters
{
    public class PlayerInteractTooltipUi : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PlayerInteractInput _playerInteractInput;
        [SerializeField] private TMP_Text _interactionText;
        [SerializeField] private Camera _camera;

        private void LateUpdate()
        {
            var hasInteractable = _playerInteractInput.TryGetCurrentInteractTargetIfAllowed( out var target );

            _canvasGroup.alpha = hasInteractable ? 1 : 0;

            if( hasInteractable )
            {
                _interactionText.text = target.InteractionDisplayText;
                transform.position = _camera.WorldToScreenPoint( target.TooltipWorldAnchor );
            }
        }
    }
}