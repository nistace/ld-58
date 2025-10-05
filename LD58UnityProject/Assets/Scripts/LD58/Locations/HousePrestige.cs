using UnityEngine;

namespace LD58.Locations
{
    [CreateAssetMenu]
    public class HousePrestige : ScriptableObject
    {
        [SerializeField] private string _displayName;
        [SerializeField] private int _minTax = 2;
        [SerializeField] private int _maxTax = 4;

        public string DisplayName => _displayName;

        public int RandomTax => Random.Range( _minTax, _maxTax + 1 );
    }
}