using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD58.Locations
{
    public class LocationPimper : MonoBehaviour
    {
        [SerializeField] private Colorables[] _colorables;
        [SerializeField] private Hideables[] _hideables;
        [SerializeField] private Spritables[] _spritables;

        public void Start()
        {
            foreach( var colorable in _colorables ) colorable.Randomize();
            foreach( var hideable in _hideables ) hideable.Randomize();
            foreach( var spritable in _spritables ) spritable.Randomize();

            Destroy( this );
        }

        [Serializable]
        private class Hideables
        {
            [SerializeField] private SpriteRenderer[] _hidables;
            [SerializeField] private float _presenceRatio = 1;

            public void Randomize()
            {
                var visible = Random.value < _presenceRatio;

                if( Random.value < _presenceRatio )
                {
                    foreach( var hidable in _hidables )
                    {
                        hidable.enabled = visible;
                    }
                }
            }
        }

        [Serializable]
        private class Spritables
        {
            [SerializeField] private SpriteRenderer[] _spritables;
            [SerializeField] private Sprite[] _distinctSprites;

            public void Randomize()
            {
                var sprite = _distinctSprites[ Random.Range( 0, _distinctSprites.Length ) ];

                foreach( var colorable in _spritables )
                {
                    colorable.sprite = sprite;
                }
            }
        }

        [Serializable]
        private class Colorables
        {
            [SerializeField] private SpriteRenderer[] _colorables;
            [SerializeField] private Color[] _colors;

            public void Randomize()
            {
                var color = _colors[ Random.Range( 0, _colors.Length ) ];

                foreach( var colorable in _colorables )
                {
                    colorable.color = color;
                }
            }
        }
    }
}