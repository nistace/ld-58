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
            Name,
            JobName,
            HouseQuality,
            LastTaxAmount,
            LastTaxDay,
            Food
        }

        [SerializeField] private NpcCharacter _npc;

        private HashSet<EInformation> _knownInformation = new();

        public UnityEvent OnNewInformationLearned { get; } = new();

        public NpcCharacter Npc => _npc;

        public NpcRecord( NpcCharacter npc )
        {
            _npc = npc;
        }

        public void Learn( NpcBehaviour npcBehaviour )
        {
            if( !_npc.HasBehaviour( npcBehaviour ) ) return;

            OnNewInformationLearned.Invoke();
        }

        public void Learn( EInformation information )
        {
            if( !_knownInformation.Add( information ) )
            {
                return;
            }

            OnNewInformationLearned.Invoke();
        }

        public string GetOrDefault( EInformation information, string defaultValue = "?" ) => TryGet( information, out var value ) ? value : defaultValue;

        public bool TryGet( EInformation information, out string value )
        {
            if( _knownInformation.Contains( information ) )
            {
                value = information switch
                {
                    EInformation.Name => Npc.Info.Name,
                    EInformation.JobName => Npc.Info.Job.JobDefinition.JobName,
                    EInformation.HouseQuality => Npc.Info.Home.Prestige.DisplayName,
                    EInformation.Food => Npc.Info.FoodType.DisplayName,
                    EInformation.LastTaxAmount => $"{Npc.Info.LastTaxAmount}",
                    EInformation.LastTaxDay => $"{Npc.Info.LastTaxDay}",
                    _ => string.Empty
                };

                return true;
            }

            value = default;

            return false;
        }

        public bool IsKnown( EInformation information ) => _knownInformation.Contains( information );
    }
}