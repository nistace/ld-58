using LD58.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorableSpritesHolder : MonoBehaviour
{
    [SerializeField] private RendererGroup[] _rendererGroups;

    public void Colorize( ColorableGroupsConfiguration configuration ) =>
        Colorize( Enumerable.Range( 0, configuration.Count ).ToDictionary( configuration.Key, configuration.Color ) );

    public void Colorize( IReadOnlyDictionary<string, Color> groupColors )
    {
        foreach( var (groupKey, color) in groupColors )
        {
            foreach( var rendererGroup in _rendererGroups )
            {
                if( rendererGroup.Key == groupKey )
                {
                    rendererGroup.Colorize( color );
                }
            }
        }
    }

    [Serializable] private class RendererGroup
    {
        [SerializeField] private string _key;
        [SerializeField] private SpriteRenderer[] _renderers;

        public string Key => _key;

        public void Colorize( Color value )
        {
            foreach( var renderer in _renderers )
            {
                renderer.color = value;
            }
        }
    }
}