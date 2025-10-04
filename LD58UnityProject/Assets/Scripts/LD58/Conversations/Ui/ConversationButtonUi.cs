using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD58.Conversations.Ui
{
    public class ConversationButtonUi : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private ConversationNode _node;

        public static UnityEvent<ConversationNode> OnConversationButtonClicked { get; } = new();

        private void Start()
        {
            _button.onClick.AddListener( () => OnConversationButtonClicked.Invoke( _node ) );
        }

        public void SetUp( ConversationNode node )
        {
            _node = node;
            _text.text = node.OptionText;
        }
    }
}