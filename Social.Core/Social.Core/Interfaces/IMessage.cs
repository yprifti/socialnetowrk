using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Interfaces
{
    public interface IMessage
    {
        IUser User { get; set; }
        string MessageText { get; set; }
        DateTime? Posted { get; set; }

        string CompactToString();

    }
}
