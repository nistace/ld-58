using LD58.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Records
{
    public class NpcRecordManager : MonoBehaviour
    {
        [SerializeField] private NpcManager _npcManager;

        private readonly Dictionary<NpcCharacter, NpcRecord> _npcRecords = new();

        public UnityEvent<NpcRecord> OnNewRecord { get; } = new();
        public UnityEvent<NpcRecord> OnRecordUpdated { get; } = new();

        public NpcRecord GetOrCreateRecord( NpcCharacter npc )
        {
            if( _npcRecords.TryGetValue( npc, out var record ) ) return record;

            var newRecord = new NpcRecord( npc );
            _npcRecords.Add( npc, newRecord );
            newRecord.OnNewInformationLearned.AddListener( () => OnRecordUpdated.Invoke( newRecord ) );

            OnNewRecord.Invoke( newRecord );

            return newRecord;
        }
    }
}