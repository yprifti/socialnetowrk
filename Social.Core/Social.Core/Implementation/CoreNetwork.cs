using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Implementation
{
    public class CoreNetwork : ICoreNetwork
    {

        private readonly IStorage _storage;
        private readonly ILoger _log;

        public CoreNetwork(IStorage storage, ILoger loger) {
            _storage = storage;
            _log = loger;
        }

        public void Follow(IUser subscriber, IUser subscribeTo)
        {
            CheckExists(subscriber);

            if (!_storage.Exists(subscribeTo))
            {
                _log.Warn(string.Format("User {0} to subscribe to does not exist", subscribeTo.UserName));
                return;
            }
            _storage.Subscribe(subscriber, subscribeTo);
            _log.Log(string.Format("{0} subscribed to {1}", subscriber.UserName, subscribeTo.UserName));
        }

        public void Post(IUser user, IMessage message)
        {
            CheckExists(user);
            message.User = user;
            _storage.Post(user, message);
            _log.Log(string.Format("{0} Message posted", message.MessageText));
        }

        public IEnumerable<IMessage> Read(IUser viewing)
        {
            if (!_storage.Exists(viewing)) {
                _log.Warn(string.Format("{0} does not exist", viewing.UserName));
                return null;
            }
            var items = _storage.Read(viewing);
            if (items != null && items.Any())
                _log.Write(string.Join(Environment.NewLine, items.AsParallel().Select(x => x.CompactToString())));
            return items;

        }

        public IEnumerable<IMessage> Wall(IUser userDashboard)
        {
            CheckExists(userDashboard);
            var items = _storage.Wall(userDashboard);
            if (items != null && items.Any())
                _log.Write(string.Join(Environment.NewLine, items.AsParallel().Select(x => x.ToString())));
            return items;
        }

        private void CheckExists(IUser user) {
            if (!_storage.Exists(user))
            {
                _storage.Add(user);
                _log.Log(string.Format("User {0} added", user.UserName));
            }

        }
        
    }
}
