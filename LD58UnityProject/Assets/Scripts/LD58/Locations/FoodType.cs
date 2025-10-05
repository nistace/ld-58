using UnityEngine;

namespace LD58.Locations
{
    [CreateAssetMenu]
    public class FoodType : ScriptableObject
    {
        [SerializeField] private int _minTax = 0;
        [SerializeField] private int _maxTax = 2;

        public string DisplayName => name;

        public int RandomTax => Random.Range( _minTax, _maxTax + 1 );
    }
}