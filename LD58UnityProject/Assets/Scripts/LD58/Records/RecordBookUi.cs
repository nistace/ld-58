using LD58.Conversations;
using LD58.Records;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordBookUi : MonoBehaviour
{
    [SerializeField] private Transform[] _anchors;
    [SerializeField] private RecordManager _recordManager;
    [SerializeField] private ConversationManager _conversationManager;
    [SerializeField] private NpcRecordUi _npcRecordUiPrefab;
    [SerializeField] private RectTransform _npcRecordsContainer;
    [SerializeField] private float _updateDuration = 5;
    [SerializeField] private RecordUi[] _allNonNpcRecords;
    [SerializeField] private RecordUi[] _displayConversationVisibleRecords;
    [SerializeField] private FoodRecordUi _foodRecordUi;
    [SerializeField] private JobRecordUi _jobRecordUi;

    private readonly List<NpcRecord> _orderedRecords = new();
    private readonly Dictionary<NpcRecord, NpcRecordUi> _npcRecordUis = new();
    private readonly Dictionary<NpcRecord, float> _updatedRecords = new();

    private float _updatedFoodRecordShownUntil;
    private float _updatedJobRecordShownUntil;

    private void Start()
    {
        _recordManager.OnNewRecord.AddListener( HandleNewRecord );
        _recordManager.OnRecordUpdated.AddListener( HandleRecordUpdated );
        _conversationManager.OnConversationStarted.AddListener( HandleConversationStarted );
        _conversationManager.OnConversationEnded.AddListener( HandleConversationEnded );
        FoodRecord.OnChanged.AddListener( HandleFoodRecordChanged );
        JobRecord.OnChanged.AddListener( HandleJobRecordChanged );

        RefreshAnchors();
    }

    private void HandleJobRecordChanged()
    {
        _updatedJobRecordShownUntil = Time.time + _updateDuration;
        RefreshVisibleRecords();
    }

    private void HandleFoodRecordChanged()
    {
        _updatedFoodRecordShownUntil = Time.time + _updateDuration;
        RefreshVisibleRecords();
    }

    private void HandleRecordUpdated( NpcRecord updatedRecord )
    {
        if( !_orderedRecords.Contains( updatedRecord ) )
        {
            HandleNewRecord( updatedRecord );

            return;
        }

        var ui = _npcRecordUis[ updatedRecord ];

        ui.SetUp( updatedRecord );
        _updatedRecords[ updatedRecord ] = Time.time + _updateDuration;

        RefreshVisibleRecords();
    }

    private void Update()
    {
        var refresh = false;

        if( _updatedRecords.Any( t => t.Value < Time.time ) )
        {
            foreach( var updatedRecordToRemove in _updatedRecords.Where( t => t.Value < Time.time ).ToArray() )
            {
                _updatedRecords.Remove( updatedRecordToRemove.Key );
            }

            refresh = true;
        }

        if( _updatedFoodRecordShownUntil > 0 && _updatedFoodRecordShownUntil < Time.time )
        {
            _updatedFoodRecordShownUntil = -1;
            refresh = true;
        }

        if( _updatedJobRecordShownUntil > 0 && _updatedJobRecordShownUntil < Time.time )
        {
            _updatedJobRecordShownUntil = -1;
            refresh = true;
        }

        if( refresh )
        {
            RefreshVisibleRecords();
        }
    }

    private void HandleNewRecord( NpcRecord newRecord )
    {
        if( !_orderedRecords.Contains( newRecord ) )
        {
            _orderedRecords.Add( newRecord );
            var npcRecordUi = Instantiate( _npcRecordUiPrefab, _npcRecordsContainer );
            _npcRecordUis.Add( newRecord, npcRecordUi );

            npcRecordUi.RecordUi.SetRecordTitle( $"Income Unit #{_orderedRecords.Count:000}" );

            RefreshAnchors();
        }

        HandleRecordUpdated( newRecord );
    }

    private void RefreshAnchors()
    {
        for( var i = 0; i < _allNonNpcRecords.Length; i++ )
        {
            _allNonNpcRecords[ i ].SetAnchor( _anchors[ i ] );
        }

        for( var i = 0; i < _orderedRecords.Count; i++ )
        {
            _npcRecordUis[ _orderedRecords[ i ] ].RecordUi.SetAnchor( _anchors[ i + _allNonNpcRecords.Length ] );
        }
    }

    private void HandleConversationStarted( Conversation conversation ) => RefreshVisibleRecords();
    private void HandleConversationEnded( Conversation conversation ) => RefreshVisibleRecords();

    private void RefreshVisibleRecords()
    {
        foreach( var recordUi in _allNonNpcRecords )
        {
            var visible = _conversationManager.IsConversationOnGoing && _displayConversationVisibleRecords.Contains( recordUi );
            if( !visible && recordUi == _foodRecordUi.RecordUi && _updatedFoodRecordShownUntil > 0 ) visible = true;
            if( !visible && recordUi == _jobRecordUi.RecordUi && _updatedJobRecordShownUntil > 0 ) visible = true;

            recordUi.SetVisible( visible );
        }

        foreach( var (record, ui) in _npcRecordUis )
        {
            var visible = _conversationManager.IsConversationOnGoing && record == _conversationManager.CurrentConversation.Record;
            if( !visible ) visible = _updatedRecords.ContainsKey( record );

            ui.RecordUi.SetVisible( visible );
        }
    }
}