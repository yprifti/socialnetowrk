using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Interfaces
{
    public interface ILoger
    {
        void Log(string log);
        void Warn(string log);

        void Write(string log);
    }
}
