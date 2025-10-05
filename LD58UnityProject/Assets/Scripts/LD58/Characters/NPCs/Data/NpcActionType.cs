using UnityEngine;

namespace LD58.Characters.Data
{
    [CreateAssetMenu]
    public class NpcActionType : ScriptableObject
    {
        [SerializeField] private float _workSpeed = 1;

        public float WorkSpeed => _workSpeed;
    }
}