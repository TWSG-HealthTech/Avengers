using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using PowerPuff.Common.Speech;

namespace PowerPuff.Speech
{
    public interface IIntentProcessor
    {
        void Process(string intentName);
    }

    public class IntentProcessor : IIntentProcessor
    {
        public IntentRouter router = new IntentRouter(null);

        public IntentProcessor(IEnumerable<IIntentHandler> intentHandlers)
        {
            foreach (var intentHandler in intentHandlers)
            {
                router.RegisterHandler(intentHandler);
            }
        }

        public void Process(string intentPayload)
        {
            var result = new LuisResult();
            result.Load(JToken.Parse(intentPayload));

            router.Route(result, null);
        }
    }
}
