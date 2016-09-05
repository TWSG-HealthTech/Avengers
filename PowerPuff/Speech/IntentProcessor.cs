using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;

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

        public IntentProcessor(IEnumerable<IIntentHandler> intentHandlers, IIntentHandler defaultHandler)
        {
            _defaultHandler = defaultHandler;
            foreach (var intentHandler in intentHandlers)
            {
                router.RegisterHandler(intentHandler);
            }
        }

        public async void Process(string intentPayload)
        {
            LuisResult result = new LuisResult();
            result.Load(JToken.Parse(intentPayload));

            bool handled = await router.Route(result, null);
            if (!handled) { _defaultHandler.Handle(); }
        }
    }

    public interface IIntentHandler
    {
        void Handle();
    }
}
