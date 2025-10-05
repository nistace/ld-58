using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent( typeof(OverlayItemUi) )]
public class RecordUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private OverlayItemUi _overlayItemUi;
    [SerializeField] private TMP_Text _recordTitle;
    [SerializeField] private GameObject _unlockedObject;
    [SerializeField] private bool _visible;
    [SerializeField] private float _rotation;
    [SerializeField] private Vector3 _offset = new Vector3( 3, 0, 0 );

    private bool _selfVisible;

    private bool Visible => _selfVisible || _visible;

    private void Start()
    {
        _overlayItemUi = GetComponent<OverlayItemUi>();
        _rotation = Random.Range( -1f, 1f );
        _offset = new Vector3( Random.Range( 0, 30f ), 0, 0 );
        transform.rotation = Quaternion.Euler( 0, 0, _rotation );
        _overlayItemUi.RectTransform.pivot = Vector2.one;
        transform.localPosition = _offset;
    }

    public void OnPointerEnter( PointerEventData eventData )
    {
        _selfVisible = true;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        _selfVisible = false;

        if( _overlayItemUi && _overlayItemUi.Anchor )
        {
            transform.SetSiblingIndex( _overlayItemUi.Anchor.GetSiblingIndex() );
        }
    }

    private void Update()
    {
        _unlockedObject.SetActive( !_overlayItemUi.IsLockedInOverlay );

        if( _overlayItemUi.IsLockedInOverlay )
        {
            return;
        }

        var newPivot = Visible ? Vector2.up : Vector2.one;

        if( newPivot != _overlayItemUi.RectTransform.pivot )
        {
            var deltaPivot = newPivot - _overlayItemUi.RectTransform.pivot;

            var deltaPosition = new Vector3( deltaPivot.x * _overlayItemUi.RectTransform.rect.width * _overlayItemUi.RectTransform.localScale.x,
                deltaPivot.y * _overlayItemUi.RectTransform.rect.height * _overlayItemUi.RectTransform.localScale.y,
                0f
            );

            _overlayItemUi.RectTransform.pivot = newPivot;
            _overlayItemUi.RectTransform.localPosition += deltaPosition;
        }
    }

    public void SetVisible( bool visible ) => _visible = visible;

    public void SetAnchor( Transform anchor )
    {
        _overlayItemUi.Anchor = anchor;
        transform.SetSiblingIndex( _overlayItemUi.Anchor.GetSiblingIndex() );
    }

    public void SetRecordTitle( string value ) => _recordTitle.text = value;
}