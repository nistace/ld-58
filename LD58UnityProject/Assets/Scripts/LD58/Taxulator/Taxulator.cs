using LD58.Conversations;
using System;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LD58.Taxulator
{
    [RequireComponent( typeof(OverlayItemUi) )]
    public class Taxulator3000 : MonoBehaviour
    {
        [SerializeField] private TMP_Text _firstOperandText;
        [SerializeField] private TMP_Text _operationText;
        [SerializeField] private TMP_Text _secondOperandText;

        [SerializeField] private Transform _outAnchor;
        [SerializeField] private Transform _inAnchor;
        [SerializeField] private ConversationManager _conversationManager;
        [SerializeField] private OverlayItemUi _overlayItemUi;
        [SerializeField] private UiInputButton[] _uiButtons;
        [SerializeField] private Button _doOperationButton;
        [SerializeField] private Button _taxButton;
        [SerializeField] private InputActionReference _doOperationAction;
        [SerializeField] private string _digitChars = "0123456789";
        [SerializeField] private string _operationChars = "+-/*";

        private RectTransform _rectTransform;
        private RectTransform _parent;

        private TMP_Text _textFieldCurrentlyBeingWritten;
        private bool _resetOnTextInput;

        private bool IsOperable()
        {
            var worldPivot = _rectTransform.TransformPoint( Vector3.Scale( _rectTransform.rect.size, _rectTransform.pivot ) );

            return _parent.rect.Contains( ( Vector2 )_parent.InverseTransformPoint( worldPivot ) );
        }

        private void Reset()
        {
            _overlayItemUi = GetComponent<OverlayItemUi>();
        }

        private void Start()
        {
            _rectTransform = _overlayItemUi.RectTransform;
            _parent = _rectTransform.parent as RectTransform;

            _firstOperandText.text = "0";
            _operationText.text = string.Empty;
            _secondOperandText.text = string.Empty;
            _textFieldCurrentlyBeingWritten = _firstOperandText;
            Keyboard.current.onTextInput += HandleTextInput;

            foreach( var uiButton in _uiButtons )
            {
                uiButton.Initialize();
                uiButton.OnClick.AddListener( HandleUiButtonClicked );
            }

            _doOperationButton.onClick.AddListener( SolveCalculus );
            _doOperationAction.action.started += SolveCalculus;
            _taxButton.onClick.AddListener( HandleTaxButtonClicked );
        }

        private void SolveCalculus( InputAction.CallbackContext obj ) => SolveCalculus();

        private void Update() => _overlayItemUi.Anchor = _conversationManager.ExpectingTaxAmount ? _inAnchor : _outAnchor;

        private void HandleTaxButtonClicked()
        {
            SolveCalculus();
            _conversationManager.SetTaxAmount( int.Parse( _firstOperandText.text ) );

            _resetOnTextInput = true;
            _overlayItemUi.UnlockOverlay();
        }

        private void HandleUiButtonClicked( char inputChar ) => HandleTextInput( inputChar );

        private void HandleTextInput( char inputChar )
        {
            if( !IsOperable() ) return;

            if( _digitChars.Contains( inputChar ) )
            {
                if( _resetOnTextInput )
                {
                    _textFieldCurrentlyBeingWritten.text = string.Empty;
                    _resetOnTextInput = false;
                }

                if( _textFieldCurrentlyBeingWritten.text.Length > 5 )
                {
                    return;
                }

                _textFieldCurrentlyBeingWritten.text += inputChar;
                var value = int.Parse( _textFieldCurrentlyBeingWritten.text );
                _textFieldCurrentlyBeingWritten.text = "" + value;
            }
            else if( _operationChars.Contains( inputChar ) )
            {
                if( _textFieldCurrentlyBeingWritten.text.Length > 0 )
                {
                    SolveCalculus();
                }

                _operationText.text = inputChar.ToString();
                _textFieldCurrentlyBeingWritten = _secondOperandText;
                _resetOnTextInput = false;
            }
        }

        private void SolveCalculus()
        {
            if( !IsOperable() ) return;

            if( _operationText.text.Length == 0 ) return;

            var result = 0;

            try
            {
                result = _operationText.text switch
                {
                    "+" => int.Parse( _firstOperandText.text ) + int.Parse( _secondOperandText.text ),
                    "-" => Mathf.Max( 0, int.Parse( _firstOperandText.text ) - int.Parse( _secondOperandText.text ) ),
                    "*" => int.Parse( _firstOperandText.text ) * int.Parse( _secondOperandText.text ),
                    "/" => int.Parse( _firstOperandText.text ) / int.Parse( _secondOperandText.text ),
                    _ => 0
                };
            }
            catch
            {
                // ignored
            }

            _firstOperandText.text = result.ToString();
            _operationText.text = string.Empty;
            _secondOperandText.text = string.Empty;

            _textFieldCurrentlyBeingWritten = _firstOperandText;
            _resetOnTextInput = true;
        }

        [Serializable]
        private class UiInputButton
        {
            [SerializeField] private char _input;
            [SerializeField] private Button _button;

            public UnityEvent<char> OnClick { get; } = new();

            public UiInputButton( char input, Button button )
            {
                _input = input;
                _button = button;
            }

            public UiInputButton() { }

            public void Initialize()
            {
                _button.onClick.AddListener( HandleButtonClick );
            }

            private void HandleButtonClick() => OnClick.Invoke( _input );
        }

        #if UNITY_EDITOR
        [ContextMenu( "Gather Buttons" )] private void GatherButtons()
        {
            _uiButtons = GetComponentsInChildren<Button>()
                .Where( t => t.name.StartsWith( "Button" ) && t.name.Length == "ButtonX".Length )
                .Select( t => new UiInputButton( t.name.Last(), t ) )
                .ToArray();

            EditorUtility.SetDirty( this );
        }
        #endif
    }
}