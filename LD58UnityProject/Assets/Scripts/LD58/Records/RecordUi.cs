using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class RecordUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text _recordTitle;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _unlockedObject;
    [SerializeField] private Transform _anchor;
    [SerializeField] private bool _visible;
    [SerializeField] private float _rotation;
    [SerializeField] private Vector3 _offset = new Vector3( 3, 0, 0 );
    [SerializeField] private float _smoothness = .5f;

    private Vector3 _smoothMovement;
    private bool _selfVisible;
    private bool _locked;
    private bool _dragging;
    private Camera _dragCamera;
    private Vector2 _dragPointerOffset;
    private Vector3 _itemDragStartPosition;
    private Vector3 _cursorDragStartPosition;
    private RectTransform _parentRectTransform;

    private bool Visible => _selfVisible || _visible;

    private void Start()
    {
        _parentRectTransform = _rectTransform.parent as RectTransform;
        _rotation = Random.Range( -1f, 1f );
        _offset = new Vector3( Random.Range( 0, 10f ), 0, 0 );
        transform.rotation = Quaternion.Euler( 0, 0, _rotation );
        _rectTransform.pivot = Vector2.one;
        transform.localPosition = Vector3.SmoothDamp( transform.localPosition, _offset, ref _smoothMovement, _smoothness );
    }

    public void OnPointerEnter( PointerEventData eventData )
    {
        _selfVisible = true;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        _selfVisible = false;
        transform.SetSiblingIndex( _anchor.GetSiblingIndex() );
    }

    private void Update()
    {
        if( _locked )
        {
            if( _dragging && RectTransformUtility.ScreenPointToLocalPointInRectangle( _parentRectTransform, Input.mousePosition, _dragCamera, out var localPointerPos ) )
            {
                _rectTransform.anchoredPosition = localPointerPos + _dragPointerOffset;
            }
        }
        else
        {
            var newPivot = Visible ? Vector2.up : Vector2.one;

            if( newPivot != _rectTransform.pivot )
            {
                var deltaPivot = newPivot - _rectTransform.pivot;

                var deltaPosition = new Vector3( deltaPivot.x * _rectTransform.rect.width * _rectTransform.localScale.x,
                    deltaPivot.y * _rectTransform.rect.height * _rectTransform.localScale.y,
                    0f
                );

                _rectTransform.pivot = newPivot;
                _rectTransform.localPosition += deltaPosition;
            }

            _rectTransform.position = Vector3.SmoothDamp( _rectTransform.position, _anchor.position + _offset, ref _smoothMovement, _smoothness );
        }
    }

    public void SetVisible( bool visible ) => _visible = visible;

    public void SetAnchor( Transform anchor )
    {
        _anchor = anchor;
        transform.SetSiblingIndex( _anchor.GetSiblingIndex() );
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        if( eventData.button == PointerEventData.InputButton.Left )
        {
            _dragCamera = eventData.pressEventCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle( _parentRectTransform, eventData.position, _dragCamera, out var dragLocalPointerPosition );

            _dragPointerOffset = _rectTransform.anchoredPosition - dragLocalPointerPosition;

            _dragging = true;
            _locked = true;
            _unlockedObject.SetActive( false );
        }
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if( eventData.button == PointerEventData.InputButton.Left )
        {
            _dragging = false;
        }

        if( eventData.button == PointerEventData.InputButton.Right )
        {
            _locked = false;
            _unlockedObject.SetActive( true );
        }
    }

    public void SetRecordTitle( string value ) => _recordTitle.text = value;
}