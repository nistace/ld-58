using LD58.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD58.Taxes
{
    [Serializable]
    public class TaxRecordTracker
    {
        public static TaxRecordTracker Current { get; set; }

        [SerializeField] private List<Record> _records = new();

        public void AddTaxRecord( NpcCharacter taxedCharacter, int taxAmount, int day ) =>
            _records.Add( new Record( taxedCharacter, taxAmount, TaxRules.Current.EvaluateCorrectTax( taxedCharacter.Info ), day ) );

        public List<Record> GetRecords( NpcCharacter npc ) => _records.Where( t => t.Character == npc ).ToList();

        [Serializable]
        public class Record
        {
            [SerializeField] private int _day;
            [SerializeField] private NpcCharacter _character;
            [SerializeField] private int _amount;
            [SerializeField] private int _correctAmount;

            public int Day => _day;
            public NpcCharacter Character => _character;
            public int Amount => _amount;
            public int CorrectAmount => _correctAmount;

            public Record( NpcCharacter character, int amount, int correctAmount, int day )
            {
                _character = character;
                _amount = amount;
                _correctAmount = correctAmount;
                _day = day;
            }
        }
    }
}