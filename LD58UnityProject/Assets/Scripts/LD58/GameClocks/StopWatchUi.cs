using LD58;
using TMPro;
using UnityEngine;

[RequireComponent( typeof(OverlayItemUi) )]
public class StopWatchUi : MonoBehaviour
{
    [SerializeField] private OverlayItemUi _overlayItemUi;
    [SerializeField] private Transform _minutes;
    [SerializeField] private Transform _hours;
    [SerializeField] private TMP_Text _dayText;

    private void Update()
    {
        _hours.localRotation = Quaternion.Euler( 0, 0, -360 * ( GameTimeManager.TimeInDayNormalized % GameTimeManager.TIME_PER_HOUR_NORMALIZED ) * GameTimeManager.HOURS_IN_DAY );
        _minutes.localRotation = Quaternion.Euler( 0, 0, -360 * GameTimeManager.TimeInDayNormalized );
        _dayText.text = $"Day {GameTimeManager.Day}";
    }
}