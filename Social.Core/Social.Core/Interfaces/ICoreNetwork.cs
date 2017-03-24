using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Interfaces
{
    public interface ICoreNetwork
    {
        void Post(IUser user, IMessage message);
        IEnumerable<IMessage> Read(IUser viewing);
        void Follow(IUser subscriber, IUser subscribeTo);
        IEnumerable<IMessage> Wall(IUser dashboard);
        
    }
}
