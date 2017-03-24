using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Models
{
    public class NetworkActivity
    {
        public IList<IMessage> Messages { get; set; }
        public IList<IUser> Subscriptions { get; set; }
    }
}
