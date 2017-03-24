using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Interfaces
{
    public interface IStorage
    {
        bool Exists(IUser user);
        bool Add(IUser user);

        void Subscribe(IUser subscriber, IUser subscribeTo);

        void Post(IUser user, IMessage message);

        IEnumerable<IMessage> Read(IUser user);

        IEnumerable<IMessage> Wall(IUser user);

        
    }
}
