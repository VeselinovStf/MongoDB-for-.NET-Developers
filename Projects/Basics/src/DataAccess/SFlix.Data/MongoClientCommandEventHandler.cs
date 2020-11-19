using MongoDB.Bson;
using MongoDB.Driver.Core.Events;
using System;

namespace SFlix.Data
{
    internal class MongoClientCommandEventHandler : IEventSubscriber
    {
        public MongoClientCommandEventHandler()
        {
            Subscribe = new ReflectionEventSubscriber(this);
        }

        private ReflectionEventSubscriber Subscribe { get; }

        public bool TryGetEventHandler<TEvent>(out Action<TEvent> handler)
        {
            return Subscribe.TryGetEventHandler(out handler);
        }

        //Implement new Handle for TEvent
        public void Handle(CommandStartedEvent started)
        {          
               Console.WriteLine($"{started.CommandName} - {started.Command.ToJson()}");          
        }
    }
}