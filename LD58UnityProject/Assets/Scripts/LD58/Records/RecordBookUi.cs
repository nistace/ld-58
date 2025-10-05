using LD58.Conversations;
using LD58.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordBookUi : MonoBehaviour
{
    [SerializeField] private Transform[] _anchors;
    [SerializeField] private NpcRecordManager _recordManager;
    [SerializeField] private ConversationManager _conversationManager;
    [SerializeField] private NpcRecordUi _npcRecordUiPrefab;
    [SerializeField] private RectTransform _npcRecordsContainer;
    [SerializeField] private float _updateDuration = 5;
    [SerializeField] private RecordUi[] _allNonNpcRecords;
    [SerializeField] private RecordUi[] _displayConversationVisibleRecords;

    private readonly List<NpcRecord> _orderedRecords = new();
    private readonly Dictionary<NpcRecord, NpcRecordUi> _npcRecordUis = new();
    private readonly Dictionary<NpcRecord, float> _updatedRecords = new();

    private void Start()
    {
        _recordManager.OnNewRecord.AddListener( HandleNewRecord );
        _recordManager.OnRecordUpdated.AddListener( HandleRecordUpdated );
        _conversationManager.OnConversationStarted.AddListener( HandleConversationStarted );
        _conversationManager.OnConversationEnded.AddListener( HandleConversationEnded );

        RefreshAnchors();
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
        if( _updatedRecords.Any( t => t.Value > Time.time ) )
        {
            foreach( var updatedRecordToRemove in _updatedRecords.Where( t => t.Value > Time.time ).ToArray() )
            {
                _updatedRecords.Remove( updatedRecordToRemove.Key );
            }

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
            recordUi.SetVisible( _conversationManager.IsConversationOnGoing && _displayConversationVisibleRecords.Contains( recordUi ) );
        }

        foreach( var (record, ui) in _npcRecordUis )
        {
            var visible = _conversationManager.IsConversationOnGoing && record == _conversationManager.CurrentConversation.Record;
            if( !visible ) visible = _updatedRecords.ContainsKey( record );

            ui.RecordUi.SetVisible( visible );
        }
    }
}