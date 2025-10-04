using LD58.Records;
using System;
using UnityEngine;

namespace LD58.Conversations
{
    [CreateAssetMenu]
    public class ConversationTreeBuilderConfig : ScriptableObject
    {
        [SerializeField] private string[] _conversationStarters;
        [SerializeField] private string _shareInformationOptionName = "Would you share with me...";
        [SerializeField] private RequestableInformation[] _requestableInformations;

        public string RandomConversationStarter => _conversationStarters[ UnityEngine.Random.Range( 0, _conversationStarters.Length ) ];

        public string ShareInformationOptionName => _shareInformationOptionName;
        public RequestableInformation[] RequestableInformations => _requestableInformations;

        [Serializable]
        public class RequestableInformation
        {
            [SerializeField] private NpcRecord.EInformation _information;
            [SerializeField] private bool _onlyIfUnknown;
            [SerializeField] private string _optionText;
            [SerializeField] private string _discussionText;

            public NpcRecord.EInformation Information => _information;
            public bool OnlyIfUnknown => _onlyIfUnknown;
            public string OptionText => _optionText;
            public string DiscussionText => _discussionText;
        }
    }
}