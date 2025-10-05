using System.Collections.Generic;
using LD58.Characters.Data;
using LD58.Misc;
using Unity.Mathematics;
using UnityEngine;

namespace LD58.Characters
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private NpcCharacter _npcCharacterPrefab;

        private List<NpcCharacter> ActiveNpcList { get; } = new();

        private int LastDayUpdate { get; set; }

        public NpcCharacter CreateNpc( NpcInfo info, ColorableGroupsConfiguration outfit )
        {
            var npcCharacter = Instantiate( _npcCharacterPrefab, info.Home.Entrance.position, quaternion.identity );

            npcCharacter.Info = info;
            info.ResetDailyInformation();
            npcCharacter.SetOutfit( outfit );

            ActiveNpcList.Add( npcCharacter );

            return npcCharacter;
        }

        private void Update()
        {
            if( GameTimeManager.Day > LastDayUpdate )
            {
                foreach( var npc in ActiveNpcList )
                {
                    npc.Info.ResetDailyInformation();
                }

                LastDayUpdate = GameTimeManager.Day;
            }

            foreach( var npc in ActiveNpcList )
            {
                npc.Tick( Time.deltaTime );
            }
        }
    }
}