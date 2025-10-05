using UnityEngine;

namespace LD58.Conversations
{
    [CreateAssetMenu]
    public class ConversationTreeBuilderConfig : ScriptableObject
    {
        [SerializeField] private string[] _startConversationLines =
        {
            "Hello, taxpayer!",
            "Good day to you, citizen.",
            "Ah, my favorite source of income!",
            "Greetings, valued contributor!",
            "Hello there — ready for a quick transaction?",
            "Hi! Got a moment for your favorite collector?",
            "Pleasant morning! Let’s discuss your financial duties.",
            "Hello! Lovely weather for paying taxes, isn’t it?",
            "Good afternoon. You know why I’m here."
        };

        [SerializeField] private string[] _startAnswerLines =
        {
            "Good day.",
            "Oh... it’s you again.",
            "Hi, collector.",
            "Hello, sir.",
            "Hey. Make it quick.",
            "Oh great, tax time.",
            "Hi there! Hope it’s nothing serious.",
            "Greetings. Do I owe something again?",
        };
        [SerializeField] private string[] _askNameLines =
        {
            "What’s your name?",
            "Remind me of your name, taxpayer.",
            "Name, please — for the records.",
            "And who do I have the pleasure of taxing today?",
            "Your name, citizen?",
            "Could you confirm your identity, for bureaucratic joy?",
            "Let’s make this official — what’s your name?",
            "Name? Don’t worry, it’s only for *documentation purposes.*",
            "You look familiar… remind me of your name.",
            "Let me check the files — unless you’d rather just tell me?"
        };

        [SerializeField] private string[] _taxAnswerAlreadyPaidLines =
        {
            "No, I already paid!",
            "Wait, I gave you money!",
            "I’m sure I paid — check your books!",
            "Didn't I just pay already?",
            "You must be mistaken, I already paid.",
            "Not again! I settled this already!",
            "That can’t be right, I paid my dues!",
            "Ask the office — I already paid!",
            "You’re double-charging me!",
            "I’ve already given enough!"
        };

        [SerializeField] private string[] _taxConversationLines =
        {
            "Time to pay up!",
            "It’s tax time!",
            "Your contribution to progress is due.",
            "Let’s balance those books!",
            "Alright, citizen — time to settle your dues.",
            "Taxes don’t collect themselves!",
            "Payment time! Let’s see what you owe.",
            "You know the drill — coins, please.",
            "The Treasury thanks you in advance!",
            "Revenue waits for no one!"
        };

        [SerializeField] private string[] _taxAnswerHowMuchLines =
        {
            "How much is it this time?",
            "What’s the damage?",
            "Alright… how bad is it?",
            "How much do you want from me now?",
            "Let’s hear it — what’s the number?",
            "How much do I owe?",
            "Go on, break my wallet.",
            "Just tell me the amount, please.",
            "How many coins will make you leave?",
            "Fine. How much?"
        };

        [SerializeField] private string[] _taxAnswerTooMuchLines =
        {
            "That’s wrong! I won’t pay that much!",
            "Impossible — you’re overcharging me!",
            "No way, that’s not the right amount!",
            "Check your math, collector!",
            "You must have miscalculated.",
            "I refuse to pay that much!"
        };

        [SerializeField] private string[] _taxPayLines =
        {
            "Okay, here’s the money.",
            "Fine… take it.",
            "Alright, I’ll pay.",
            "Here, all counted.",
            "There you go. Happy now?",
            "Alright, alright, here’s your tax.",
            "Here, every last coin.",
            "I hope this keeps you off my doorstep.",
            "Okay… payment done."
        };

        [SerializeField] private string[] _endConversationLines =
        {
            "Good bye!",
            "Until next time!",
            "Pleasure doing business.",
            "Stay productive!",
            "Keep earning, I’ll be back!",
            "That’s all for today. Goodbye.",
            "Take care — and keep those coins ready.",
            "Alright, I’m off. Be good.",
            "We’ll talk again… soon.",
        };

        [SerializeField] private string[] _endAnswerLines =
        {
            "Bye.",
            "See you.",
            "Good riddance.",
            "Have a nice day.",
            "Until next time, I guess.",
            "Take care.",
            "Goodbye, collector.",
            "See you later.",
            "Farewell.",
        };

        public string RandomStartConversationLine => _startConversationLines[ Random.Range( 0, _startConversationLines.Length ) ];
        public string RandomStartAnswerLine => _startAnswerLines[ Random.Range( 0, _startAnswerLines.Length ) ];
        public string RandomAskNameLine => _askNameLines[ Random.Range( 0, _askNameLines.Length ) ];
        public string RandomTaxConversationLine => _taxConversationLines[ Random.Range( 0, _taxConversationLines.Length ) ];
        public string RandomTaxAnswerHowMuchLine => _taxAnswerHowMuchLines[ Random.Range( 0, _taxAnswerHowMuchLines.Length ) ];
        public string RandomTaxAnswerAlreadyPaidLine => _taxAnswerAlreadyPaidLines[ Random.Range( 0, _taxAnswerAlreadyPaidLines.Length ) ];
        public string RandomTaxAnswerTooMuchLine => _taxAnswerTooMuchLines[ Random.Range( 0, _taxAnswerTooMuchLines.Length ) ];
        public string RandomTaxPayLine => _taxPayLines[ Random.Range( 0, _taxPayLines.Length ) ];
        public string RandomEndConversationLine => _endConversationLines[ Random.Range( 0, _endConversationLines.Length ) ];
        public string RandomEndAnswerLine => _endAnswerLines[ Random.Range( 0, _endAnswerLines.Length ) ];
    }
}