using System.Linq;
using TMPro;
using UnityEngine;

namespace LD58.Records
{
    [RequireComponent( typeof(RecordUi) )]
    public class FoodRecordUi : MonoBehaviour
    {
        [SerializeField] private RecordUi _recordUi;
        [SerializeField] private TMP_Text _text;
        public RecordUi RecordUi => _recordUi;

        private void Reset()
        {
            _recordUi = GetComponent<RecordUi>();
        }

        private void Start()
        {
            if( FoodRecord.Current != null ) { Refresh(); }

            FoodRecord.OnChanged.AddListener( Refresh );
        }

        private void Refresh()
        {
            _text.text = string.Join( "<br>",
                FoodRecord.Current.ActiveFoodTypes.OrderBy( t => t.DisplayName ).Select( t => $"{t.DisplayName}: {FoodRecord.Current.TryGetKnownPrice( t )}" )
            );
        }
    }
}