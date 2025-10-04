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

        public NpcCharacter CreateNpc( NpcInfo info, ColorableGroupsConfiguration outfit )
        {
            var npcCharacter = Instantiate( _npcCharacterPrefab, info.Home.Entrance.position, quaternion.identity );

            npcCharacter.Info = info;
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