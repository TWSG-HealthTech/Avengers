using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PowerPuff.Speech
{
    public interface IIntentProcessor
    {
        void Process(string intentName);
    }

    public class IntentProcessor : IIntentProcessor
    {
        private readonly IIntentHandler _defaultHandler;

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
            LuisResult result = new LuisResult();
            result.Load(JToken.Parse(intentPayload));

            router.Route(result, null);
        }
    }
}
