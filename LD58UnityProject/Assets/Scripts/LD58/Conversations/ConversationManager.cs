using Cysharp.Threading.Tasks;
using LD58.Characters;
using LD58.Characters.PlayerCharacters;
using LD58.Records;
using LD58.Taxes;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Conversations
{
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] private NpcManager _npcManager;
        [SerializeField] private RecordManager _recordManager;
        [SerializeField] private PlayerCharacter _playerCharacter;
        [SerializeField] private ConversationTreeBuilderConfig _conversationTreeBuilderConfig;
        [SerializeField] private float _conversationSpeedCoefficient = 1;

        public bool IsConversationOnGoing => _playerCharacter.IsTalking;
        public UnityEvent<string> OnPlayerSaidSomething { get; } = new();
        public UnityEvent<string> OnNpcSaidSomething { get; } = new();
        public UnityEvent<Conversation> OnConversationStarted { get; } = new();
        public UnityEvent<Conversation> OnConversationEnded { get; } = new();
        public Conversation CurrentConversation { get; private set; }
        private ConversationNode SelectedNodeInCurrentConversation { get; set; }
        private int TaxAmount { get; set; }
        private CancellationTokenSource ConversationCancellationToken { get; set; }

        public UnityEvent<Conversation> OnConversationTreeGenerated { get; } = new();
        public UnityEvent OnConversationOptionSelected { get; } = new();
        public bool ExpectingTaxAmount { get; private set; }

        private void Start()
        {
            ConversationStarterInteractable.OnConversationRequested.AddListener( HandleConversationRequested );
        }

        private void OnDestroy()
        {
            ConversationCancellationToken?.Cancel();
            ConversationCancellationToken?.Dispose();
            ConversationCancellationToken = null;
            ConversationStarterInteractable.OnConversationRequested.RemoveListener( HandleConversationRequested );
        }

        private void HandleConversationRequested( ConversationStarterInteractable source )
        {
            if( IsConversationOnGoing )
            {
                Debug.LogWarning( "Is already in conversation" );

                return;
            }

            ConversationCancellationToken?.Cancel();
            ConversationCancellationToken?.Dispose();
            ConversationCancellationToken = new CancellationTokenSource();

            ConverseAsync( source.ConversationTarget, ConversationCancellationToken.Token ).Forget();
        }

        private async UniTask ConverseAsync( NpcCharacter npcCharacter, CancellationToken cancellationToken )
        {
            try
            {
                CurrentConversation = new Conversation( npcCharacter, _recordManager.GetOrCreateRecord( npcCharacter ), _conversationTreeBuilderConfig, _playerCharacter );

                npcCharacter.IsTalking = true;
                _playerCharacter.IsTalking = true;

                OnConversationStarted.Invoke( CurrentConversation );

                OnPlayerSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomStartConversationLine );

                await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomStartAnswerLine );

                await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                while( !CurrentConversation.IsOver )
                {
                    CurrentConversation.GenerateNextConversationTree();
                    SelectedNodeInCurrentConversation = null;

                    OnConversationTreeGenerated.Invoke( CurrentConversation );

                    await UniTask.WaitWhile( () => SelectedNodeInCurrentConversation == null, cancellationToken: cancellationToken );

                    OnConversationOptionSelected.Invoke();

                    if( SelectedNodeInCurrentConversation is NameConversationNode )
                    {
                        OnPlayerSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomAskNameLine );

                        await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                        OnNpcSaidSomething.Invoke( npcCharacter.Info.Name );

                        await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                        CurrentConversation.Record.Learn( NpcRecord.EInformation.Name );
                    }
                    else if( SelectedNodeInCurrentConversation is TaxConversationNode )
                    {
                        OnPlayerSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomTaxConversationLine );

                        await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                        if( GameTimeManager.Day - npcCharacter.Info.LastTaxDay < TaxRules.DaysBetweenTwoCollections )
                        {
                            OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomTaxAnswerAlreadyPaidLine );

                            await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                            CurrentConversation.IsOver = true;
                        }
                        else
                        {
                            OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomTaxAnswerHowMuchLine );

                            TaxAmount = -1;
                            ExpectingTaxAmount = true;

                            await UniTask.WaitWhile( () => TaxAmount < 0, cancellationToken: cancellationToken );

                            ExpectingTaxAmount = false;

                            OnPlayerSaidSomething.Invoke( $"{TaxAmount}" );

                            await UniTask.Delay( TimeSpan.FromSeconds( .2f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                            if( TaxAmount > TaxRules.Current.EvaluateCorrectTax( npcCharacter.Info ) )
                            {
                                OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomTaxAnswerTooMuchLine );

                                await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                                CurrentConversation.IsOver = true;
                            }
                            else
                            {
                                OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomTaxPayLine );

                                await UniTask.Delay( TimeSpan.FromSeconds( .5f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                                npcCharacter.Info.LastTaxDay = GameTimeManager.Day;
                                npcCharacter.Info.LastTaxAmount = TaxAmount;

                                CurrentConversation.Record.Learn( NpcRecord.EInformation.LastTaxDay );
                                CurrentConversation.Record.Learn( NpcRecord.EInformation.LastTaxAmount );

                                TaxRecordTracker.Current.AddTaxRecord( npcCharacter, TaxAmount, GameTimeManager.Day );
                            }
                        }
                    }
                    else if( SelectedNodeInCurrentConversation is EndConversationNode )
                    {
                        OnPlayerSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomEndConversationLine );

                        await UniTask.Delay( TimeSpan.FromSeconds( .2f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                        OnNpcSaidSomething.Invoke( _conversationTreeBuilderConfig.RandomEndAnswerLine );

                        await UniTask.Delay( TimeSpan.FromSeconds( .2f * _conversationSpeedCoefficient ), cancellationToken: cancellationToken );

                        CurrentConversation.IsOver = true;
                    }
                }
            }
            finally
            {
                npcCharacter.IsTalking = false;
                _playerCharacter.IsTalking = false;

                OnConversationEnded.Invoke( CurrentConversation );

                CurrentConversation = null;
            }
        }

        public void SelectLeafOptionInCurrentConversationTree( ConversationNode leafOption ) => SelectedNodeInCurrentConversation = leafOption;

        public void SetTaxAmount( int amount ) => TaxAmount = amount;

        public void EndAny()
        {
            ConversationCancellationToken?.Dispose();
            ConversationCancellationToken?.Cancel();
            ConversationCancellationToken = null;
        }
    }
}