using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

namespace LD58.Conversations.Ui
{
    public class ConversationBubbleUi : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _fadeTime = .2f;

        private CancellationTokenSource CancellationTokenSource { get; set; }

        private void Start()
        {
            _canvasGroup.alpha = 0;
        }

        public void Show( string text, float duration )
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = new CancellationTokenSource();

            ShowAsync( text, duration, CancellationTokenSource.Token ).Forget();
        }

        private void OnDestroy()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = null;
        }

        private async UniTask ShowAsync( string text, float duration, CancellationToken cancellationToken )
        {
            _text.text = text;

            while( _canvasGroup.alpha < 1 )
            {
                _canvasGroup.alpha += Time.deltaTime / _fadeTime;
                await UniTask.NextFrame( cancellationToken );
            }

            await UniTask.Delay( TimeSpan.FromSeconds( duration ), cancellationToken: cancellationToken );

            while( _canvasGroup.alpha > 0 )
            {
                _canvasGroup.alpha -= Time.deltaTime / _fadeTime;
                await UniTask.NextFrame( cancellationToken );
            }
        }
    }
}