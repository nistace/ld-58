using UnityEngine;
namespace LD58.Characters {
   public class Character : MonoBehaviour {
      [ SerializeField ] private CharacterConfig _characterConfig;

      public CharacterConfig CharacterConfig => _characterConfig;
   }
}
