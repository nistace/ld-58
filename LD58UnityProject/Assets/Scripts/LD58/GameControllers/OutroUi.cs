using LD58.Characters;
using LD58.Taxes;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OutroUi : MonoBehaviour
{
    [SerializeField] private NpcManager _npcManager;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UnityEvent _onShow;
    [SerializeField] private string[] _scoreComments =
    {
        "You understood the job, right?",
        "We’ve seen better results from unpaid interns.",
        "That’s... technically a number.",
        "Mediocre. The Ministry expected at least a little greed.",
        "Not bad. You might pass as a part-time collector.",
        "Acceptable performance. You’ve squeezed some coins out.",
        "Good job. You’re starting to smell like authority.",
        "Impressive work! The citizens will definitely complain.",
        "Excellent! You milked the city beautifully.",
        "Flawless! The Ministry of Revenue salutes your greed.",
        "Flawless! The Ministry of Revenue salutes your greed."
    };

    public void FillInAndShow()
    {
        var total = 0;
        var best = 0;

        _text.text = "";
        _text.text += "<br>";
        _text.text += " So let's see how well you did...";
        _text.text += "<br><br>";

        foreach( var npc in _npcManager.AllNpcList )
        {
            var max = TaxRules.Current.EvaluateCorrectTax( npc.Info );
            best += max;

            var records = TaxRecordTracker.Current.GetRecords( npc );

            _text.text += $"- {npc.Info.Name} owed ${max} everytime. ";

            if( records.Count == 0 )
            {
                _text.text += "But you never taxed them.";
            }
            else
            {
                total += records.Max( t => t.Amount );

                if( records.Count == 1 ) _text.text += $"You taxed them once for ${records[ 0 ].Amount}";
                else
                {
                    _text.text += $"You taxed them for ${string.Join( ", $", records.Skip( 1 ) )} and ${records[ 0 ]}.";
                }
            }

            _text.text += "<br>";
        }

        var score = Mathf.FloorToInt( total * 10000f / best );

        _text.text += "<br>According to my Taxulator4000 (mine is better), your final score is:<br><br>";
        _text.text += "<align=center>" + score;
        _text.text += "<br><br>";
        _text.text += _scoreComments[ score / 1000 ];

        _onShow.Invoke();
        gameObject.SetActive( true );
    }

    public void PlayAgain() => SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );

    public void Quit() => Application.Quit();
}