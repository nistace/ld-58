using LD58.Characters;
using LD58.Characters.NpcBehaviours;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD58.Records
{
    [Serializable]
    public class NpcRecord
    {
        public enum EInformation
        {
            Name = 0,
            JobName = 1,
            JobStartTime = 2,
            JobEndTime = 3,
            JobPay = 4,
            LastTaxAmount = 5,
            LastTaxDay = 6
        }

        public enum EDetailedInformation
        {
            Routines = 0
        }

        [SerializeField] private NpcCharacter _npc;
        [SerializeField] private string[] _knownInformation;

        private HashSet<NpcBehaviour> _knownBehaviours = new();
        private HashSet<EInformation> _newlyLearnedInformation = new();
        private HashSet<NpcBehaviour> _newlyLearnedBehaviours = new();

        public IEnumerable<NpcBehaviour> KnownBehaviours => _knownBehaviours;

        public UnityEvent OnNewInformationLearned { get; } = new();

        public NpcCharacter Npc => _npc;

        public NpcRecord( NpcCharacter npc )
        {
            _npc = npc;
            _knownInformation = new string[ Enum.GetValues( typeof(EInformation) ).Length ];
        }

        public void Learn( NpcBehaviour npcBehaviour )
        {
            if( !_npc.HasBehaviour( npcBehaviour ) ) return;

            if( !_knownBehaviours.Add( npcBehaviour ) ) return;

            _newlyLearnedBehaviours.Add( npcBehaviour );
            OnNewInformationLearned.Invoke();
        }

        public void Learn( EInformation information, string value )
        {
            if( _knownInformation[ ( int )information ] == value )
            {
                return;
            }

            _knownInformation[ ( int )information ] = value;
            _newlyLearnedInformation.Add( information );

            OnNewInformationLearned.Invoke();
        }

        public void ResetNewlyLearnedInformation()
        {
            _newlyLearnedInformation.Clear();
            _newlyLearnedBehaviours.Clear();
        }

        public string GetOrDefault( EInformation information, string defaultValue = "?" ) => TryGet( information, out var value ) ? value : defaultValue;

        public bool TryGet( EInformation information, out string value )
        {
            value = _knownInformation[ ( int )information ];

            return !string.IsNullOrEmpty( value );
        }

        public bool IsKnown( EInformation information ) => !string.IsNullOrEmpty( _knownInformation[ ( int )information ] );

        public bool IsAnyRoutineKnown() => _knownBehaviours.Count > 0;
    }
}