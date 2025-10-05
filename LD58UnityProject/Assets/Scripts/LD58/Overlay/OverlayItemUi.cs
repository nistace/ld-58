using UnityEngine;
using UnityEngine.EventSystems;

public class OverlayItemUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _autoMovementSmoothness = .2f;

    private Vector3 _smoothMovement;
    private bool _selfVisible;
    private bool _dragging;
    private Camera _dragCamera;
    private Vector2 _dragPointerOffset;
    private Vector3 _itemDragStartPosition;
    private Vector3 _cursorDragStartPosition;
    private RectTransform _parentRectTransform;
    private bool _hasMoved;

    public RectTransform RectTransform => _rectTransform;
    public bool IsLockedInOverlay { get; private set; }
    public Transform Anchor { get; set; }

    private void Start()
    {
        _parentRectTransform = _rectTransform.parent as RectTransform;
    }

    private void Update()
    {
        if( IsLockedInOverlay )
        {
            if( _dragging && RectTransformUtility.ScreenPointToLocalPointInRectangle( _parentRectTransform, Input.mousePosition, _dragCamera, out var localPointerPos ) )
            {
                if( !_hasMoved && _dragPointerOffset.sqrMagnitude > 1 )
                {
                    _hasMoved = true;
                }

                _rectTransform.anchoredPosition = localPointerPos + _dragPointerOffset;
            }
        }
        else if( Anchor )
        {
            RectTransform.position = Vector3.SmoothDamp( RectTransform.position, Anchor.position, ref _smoothMovement, _autoMovementSmoothness );
        }
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        if( eventData.button == PointerEventData.InputButton.Left )
        {
            _dragCamera = eventData.pressEventCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle( _parentRectTransform, eventData.position, _dragCamera, out var dragLocalPointerPosition );

            _dragPointerOffset = _rectTransform.anchoredPosition - dragLocalPointerPosition;

            _dragging = true;
            IsLockedInOverlay = true;
        }
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if( eventData.button == PointerEventData.InputButton.Left )
        {
            if( !_hasMoved )
            {
                IsLockedInOverlay = false;
            }

            _dragging = false;
        }

        if( eventData.button == PointerEventData.InputButton.Right )
        {
            IsLockedInOverlay = false;
        }
    }

    public void UnlockOverlay() => IsLockedInOverlay = false;
}