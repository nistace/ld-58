using UnityEngine;

namespace LD58
{
    public class GameTimeManager : MonoBehaviour
    {
        private static GameTimeManager Instance { get; set; }
        public const int HOURS_IN_DAY = 12;
        public const float TIME_PER_HOUR_NORMALIZED = 1f / HOURS_IN_DAY;

        public static int Day => 1 + ( Instance ? Mathf.FloorToInt( Instance._absoluteTime ) : 0 );
        public static float TimeInDay => Instance ? TimeInDayNormalized * Instance._timeInDay : 0;
        public static float TimeInDayNormalized => Instance ? Instance._absoluteTime % 1 : 0;
        public static float ActionSpeedCoefficient => Instance ? Instance._actionSpeedCoefficient : 0;

        private float _absoluteTime;

        [SerializeField] private int _timeInDay = 2;
        [SerializeField] private float _actionSpeedCoefficient = 1;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _absoluteTime += Time.deltaTime / _timeInDay;
        }
    }
}