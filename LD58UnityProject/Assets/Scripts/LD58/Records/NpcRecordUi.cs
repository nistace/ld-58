using LD58.Records;
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
    [SerializeField] private TMP_Text _houseQuality;
    [SerializeField] private TMP_Text _regime;
    [SerializeField] private TMP_Text _lastTaxText;

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
        _jobText.text = $"Job: {record.GetOrDefault( NpcRecord.EInformation.JobName )}";
        _houseQuality.text = $"Living: {record.GetOrDefault( NpcRecord.EInformation.HouseQuality )}";
        _regime.text = $"Regime: {record.GetOrDefault( NpcRecord.EInformation.Food )}";

        _lastTaxText.text = "Last Tax: ?";

        if( record.TryGet( NpcRecord.EInformation.LastTaxAmount, out var lastTaxAmount ) )
        {
            _lastTaxText.text = $"Last Tax: ${lastTaxAmount}";
        }

        if( record.TryGet( NpcRecord.EInformation.LastTaxDay, out var lastTaxDay ) )
        {
            _lastTaxText.text += ", on Day " + lastTaxDay;
        }
    }
}