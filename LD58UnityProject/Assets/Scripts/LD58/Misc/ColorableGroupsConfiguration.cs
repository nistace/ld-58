using System;
using UnityEngine;

namespace LD58.Misc
{
    [CreateAssetMenu]
    public class ColorableGroupsConfiguration : ScriptableObject
    {
        [SerializeField] private string[] _keys;
        [SerializeField] private Color[] _colors;

        public int Count => Math.Min( _keys.Length, _colors.Length );

        public string Key( int i ) => _keys[ i ];
        public Color Color( int i ) => _colors[ i ];
    }
}