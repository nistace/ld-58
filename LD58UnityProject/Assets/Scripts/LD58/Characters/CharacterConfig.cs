using UnityEngine;
namespace LD58.Characters {
   [ CreateAssetMenu ]
   public class CharacterConfig : ScriptableObject {
      [ SerializeField ] private float _movementSpeed = 3;

      public float MovementSpeed => _movementSpeed;
   }
}
