using System.Linq;
using TMPro;
using UnityEngine;

namespace LD58.Records
{
    [RequireComponent( typeof(RecordUi) )]
    public class JobRecordUi : MonoBehaviour
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
            if( JobRecord.Current != null ) { Refresh(); }

            JobRecord.OnChanged.AddListener( Refresh );
        }

        private void Refresh()
        {
            _text.text = string.Join( "<br>",
                JobRecord.Current.ActiveJobsIncomes.OrderBy( t => t.Key.JobName ).Select( t => $"{t.Key.JobName}:  {( JobRecord.Current.KnownIncomes ? $"{t.Value}" : "?" )}" )
            );
        }
    }
}