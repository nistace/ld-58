using LD58.Characters;
using System;
using System.Collections.Generic;
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

        [Serializable]
        public class Record
        {
            [SerializeField] private int _day;
            [SerializeField] private NpcCharacter _character;
            [SerializeField] private int _amount;
            [SerializeField] private int _correctAmount;

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