using LD58.Characters.Data;
using UnityEngine;

namespace LD58.Characters.NpcBehaviours
{
    public abstract class NpcBehaviour : MonoBehaviour
    {
        public abstract bool CheckConditions( NpcInfo info );
        public abstract Vector3 GetTargetLocation( NpcInfo info );
        public abstract string ToDisplayString();
    }
}