using Social.Core.Interfaces;
using Social.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Implementation
{
    public class Storage : IStorage
    {
        private readonly Dictionary<IUser, NetworkActivity> _storage;

        public Storage() {
            _storage = new Dictionary<IUser, NetworkActivity>();
        }

        public bool Add(IUser user)
        {
            if (!Exists(user))
            {
                _storage.Add(user, new NetworkActivity());
                return true;
            }
            return false;
        }

        public bool Exists(IUser user)
        {
            return _storage.ContainsKey(user);
        }

        public void Post(IUser user, IMessage message)
        {
            var activity = Get(user);

            if (activity.Messages == null) activity.Messages = new List<IMessage>();
            message.Posted = DateTime.Now;
            activity.Messages.Add(message);

            _storage[user] = activity;
        }

        public IEnumerable<IMessage> Read(IUser user)
        {
            var activity = Get(user);

            return activity.Messages;

        }

        public void Subscribe(IUser subscriber, IUser subscribeTo)
        {
            var activity = Get(subscriber);

            if (activity.Subscriptions == null) activity.Subscriptions = new List<IUser>();
            activity.Subscriptions.Add(subscribeTo);

            _storage[subscriber] = activity;
            
        }

        public IEnumerable<IMessage> Wall(IUser user)
        {
            var activity = Get(user);

            var result = new List<IMessage>();
            if (activity.Subscriptions == null) return null;
            activity.Subscriptions.ToList().ForEach(x => result.AddRange(Read(x)));
            return result.OrderByDescending(x => x.Posted);
        }

        private NetworkActivity Get(IUser user) {
            if(!_storage.ContainsKey(user)) throw new Exception("User does not esit! (this shouldn't happen)");
            var activity = _storage[user];
            if (activity == null) throw new Exception("Activity was null! (this shouldn't happen)");
            return activity;
        }

    }
}
