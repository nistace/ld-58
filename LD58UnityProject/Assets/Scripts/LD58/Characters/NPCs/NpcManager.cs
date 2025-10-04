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

        private List<Npc> ActiveNpcList { get; } = new();

        public Npc CreateNpc( Location home, Job job, int money, ColorableGroupsConfiguration npcOutfit )
        {
            var npcCharacter = Instantiate( _npcCharacterPrefab, home.Entrance.position, quaternion.identity );
            npcCharacter.OutfitBodyParts.Colorize( npcOutfit );

            var npcInfo = new NpcInfo( home, money, job );
            var newNpc = new Npc( npcInfo, npcCharacter );

            ActiveNpcList.Add( newNpc );

            return newNpc;
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