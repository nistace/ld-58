using UnityEngine;

namespace LD58.Cameras {
   public class FollowCamera : MonoBehaviour {
      [ SerializeField ] private Transform _target;
      [ SerializeField ] private Vector3 _offset = new( 0, 0, -10 );
      [ SerializeField ] private float _maxSpeed = 3;
      [ SerializeField ] private float _smoothFollow = 1;

      private Vector3 _currentVelocity;

      private void FixedUpdate() {
         transform.position = Vector3.SmoothDamp(transform.position, _target.position + _offset, ref _currentVelocity, _smoothFollow, _maxSpeed);
      }
   }
}
