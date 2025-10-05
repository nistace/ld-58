using System;
using UnityEngine;

namespace LD58
{
    public class GameTimeManager : MonoBehaviour
    {
        private static GameTimeManager instance { get; set; }

        public static int Day => instance ? Mathf.FloorToInt( instance._absoluteTime ) : 1;
        public static float TimeInDay => instance ? TimeInDayNormalized * instance._timeInDay : 0;
        public static float TimeInDayNormalized => instance ? instance._absoluteTime % 1 : 0;
        public static float ActionSpeedCoefficient => instance ? instance._actionSpeedCoefficient : 0;

        private float _absoluteTime;

        [SerializeField] private int _timeInDay = 2;
        [SerializeField] private float _actionSpeedCoefficient = 1;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            _absoluteTime += Time.deltaTime / _timeInDay;
        }
    }
}