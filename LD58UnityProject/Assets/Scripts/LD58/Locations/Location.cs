using UnityEngine;
namespace LD58.Locations {
   public class Location : MonoBehaviour {
      [ SerializeField ] private Transform _entrance;

      public Transform Entrance => _entrance;
   }
}
