using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PowerPuff.Speech
{
    public interface IIntentProcessor
    {
        void Process(string intentName);
    }

    public class IntentProcessor : IIntentProcessor
    {
        private readonly IIntentHandler _defaultHandler;
        public ReadOnlyDictionary<string, IIntentHandler> _handlers;

        public IntentProcessor(IEnumerable<IIntentHandler> intentHandlers, IIntentHandler defaultHandler)
        {
            _defaultHandler = defaultHandler;
            _handlers = new ReadOnlyDictionary<string, IIntentHandler>(intentHandlers.ToDictionary(handler => handler.IntentName));
        }

        public void Process(string intentName)
        {
            IIntentHandler handler;
            if (!_handlers.TryGetValue(intentName, out handler))
            {
                handler = _defaultHandler;
            };
            handler.Handle();
        }
    }

    public interface IIntentHandler
    {
        string IntentName { get; }
        void Handle();
    }
}
