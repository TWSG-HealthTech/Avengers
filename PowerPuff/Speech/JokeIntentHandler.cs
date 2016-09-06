using Microsoft.Cognitive.LUIS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPuff.Speech
{
    public class JokeIntentHandler : IIntentHandler
    {
        private readonly ISpeechSynthesiser _speechSynthesiser;
        private Random _random = new Random();

        private IList<string> _jokes = new[]
        {
            "What’s round and bad tempered? A vicious circle!",
            "What do frogs wear on their feet? Open toad sandles!",
            "What do you call a train loaded with toffee? A chew chew train!",
            "Why did the pony have need a drink of water? Because it was a little horse!",
            "A man walks into a bar... he says Ouch!",
            "What do you call a line of men waiting for a haircut? A barbeque!",
            "Why didn't the skeleton go to the party? He had nobody to go with!",
            "What do you get when you cross fish and an elephant? Swimming trunks!",
            "What do you call a bear with no teeth? A gummy bear!",
            "Why shouldn't you play cards in the jungle? Because there are so many cheetahs!"
        };

        public JokeIntentHandler(ISpeechSynthesiser speechSynthesiser)
        {
            _speechSynthesiser = speechSynthesiser;
        }

        [IntentHandler(0.75, Name = "TellJoke")]
        public async Task<bool> TellJoke(LuisResult result, object context)
        {
            _speechSynthesiser.Speak(_jokes[_random.Next(_jokes.Count)]);
            return true;
        }
    }
}
