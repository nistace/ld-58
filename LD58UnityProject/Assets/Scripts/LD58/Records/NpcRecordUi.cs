using LD58.Records;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent( typeof(RecordUi) )]
public class NpcRecordUi : MonoBehaviour
{
    [SerializeField] private RecordUi _recordUi;
    [SerializeField] private ColorableImagesHolder _picture;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _jobText;
    [SerializeField] private TMP_Text _payText;
    [SerializeField] private TMP_Text _lastTaxText;
    [SerializeField] private TMP_Text _routineText;

    public RecordUi RecordUi => _recordUi;

    private void Start()
    {
        transform.localRotation = Quaternion.Euler( 0, 0, Random.value * 2 - 1 );
        transform.localPosition = new Vector3( Random.value * 10, 0, 0 );
    }

    public void SetUp( NpcRecord record )
    {
        _picture.Colorize( record.Npc.Outfit );
        _nameText.text = $"Name: {record.GetOrDefault( NpcRecord.EInformation.Name )}";

        if( record.IsKnown( NpcRecord.EInformation.JobName ) || record.IsKnown( NpcRecord.EInformation.JobStartTime ) || record.IsKnown( NpcRecord.EInformation.JobEndTime ) )
        {
            _jobText.text = $"Job: {record.GetOrDefault( NpcRecord.EInformation.JobName )}, "
                + $"{record.GetOrDefault( NpcRecord.EInformation.JobStartTime )}-{record.GetOrDefault( NpcRecord.EInformation.JobEndTime )}";
        }
        else
        {
            _jobText.text = "Job: ?";
        }

        _payText.text = record.GetOrDefault( NpcRecord.EInformation.JobPay );
        if( _payText.text != "?" ) _payText.text = $"${_payText.text}";
        _payText.text = "Pay: " + _payText.text;

        _lastTaxText.text = "Last Tax: ?";

        if( record.TryGet( NpcRecord.EInformation.LastTaxAmount, out var lastTaxAmount ) )
        {
            _lastTaxText.text = $"${lastTaxAmount}";
        }

        if( record.TryGet( NpcRecord.EInformation.LastTaxDay, out var lastTaxDay ) )
        {
            _lastTaxText.text += ", on Day " + lastTaxDay;
        }

        if( record.IsAnyRoutineKnown() )
        {
            _routineText.text = "Routines: " + string.Join( ", ", record.KnownBehaviours.Select( t => t.ToDisplayString() ) );
        }
        else
        {
            _routineText.text = "Routines: ?";
        }
    }
}