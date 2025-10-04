using System.Collections.Generic;
using LD58.Characters.Data;
using LD58.Jobs;
using LD58.Locations;
using LD58.Misc;
using Unity.Mathematics;
using UnityEngine;

namespace LD58.Characters
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private NpcCharacter _npcCharacterPrefab;

        private List<NpcCharacter> ActiveNpcList { get; } = new();

        public NpcCharacter CreateNpc( Location home, Job job, int money, ColorableGroupsConfiguration outfit )
        {
            var npcCharacter = Instantiate( _npcCharacterPrefab, home.Entrance.position, quaternion.identity );

            npcCharacter.Info = new NpcInfo( home, money, job );
            npcCharacter.SetOutfit( outfit );

            ActiveNpcList.Add( npcCharacter );

            return npcCharacter;
        }

        private void Update()
        {
            foreach( var npc in ActiveNpcList )
            {
                npc.Tick( Time.deltaTime );
            }
        }
    }
}