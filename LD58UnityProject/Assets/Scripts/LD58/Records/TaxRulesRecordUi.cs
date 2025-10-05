using LD58.Records;
using LD58.Taxes;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent( typeof(RecordUi) )]
public class TaxRulesRecordUi : MonoBehaviour
{
    [SerializeField] private string _taxRuleTemplate;
    [SerializeField] private RecordUi _recordUi;
    [SerializeField] private TMP_Text _text;

    public RecordUi RecordUi => _recordUi;

    private void Reset()
    {
        _recordUi = GetComponent<RecordUi>();
    }

    private void Start()
    {
        if( TaxRules.Current != null )
        {
            Refresh();
        }

        TaxRules.OnInitialized.AddListener( Refresh );
    }

    private void Refresh()
    {
        _text.text = _taxRuleTemplate
            .Replace( "{jobTax}", $"{TaxRules.Current.JobPayTax * 100}" )
            .Replace( "{housingTax}", string.Join( "<br>   - ", TaxRules.Current.HousingTaxes.Select( t => $"{t.Key.DisplayName}: {t.Value}" ) ) )
            .Replace( "{taxCooldown}", $"{TaxRules.DaysBetweenTwoCollections}" );
    }
}