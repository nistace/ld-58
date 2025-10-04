using System;
using UnityEngine;
namespace LD58 {
   public class GameTimeManager : MonoBehaviour {
      private static GameTimeManager instance { get; set; }

      public static int Day => instance ? Mathf.FloorToInt(instance.absoluteGameTime) : 1;
      public static float TimeInDay => instance ? TimeInDayNormalized * instance._timeInDay : 0;
      public static float TimeInDayNormalized => instance ? instance.absoluteGameTime % 1 : 0;

      private float absoluteGameTime;

      [ SerializeField ] private int _timeInDay = 2;

      private void Awake() {
         instance = this;
      }

      private void Update() {
         absoluteGameTime += Time.deltaTime / _timeInDay;
      }
   }
}
