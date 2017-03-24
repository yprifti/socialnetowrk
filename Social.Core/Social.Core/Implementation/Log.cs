using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Implementation
{
    public class Log : ILoger
    {
        private readonly TextWriter _logChannel;
        
        public Log()
        {
            _logChannel = Console.Out;
        }

        public void Warn(string log)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            _logChannel.WriteLine(log);
            Console.ForegroundColor = current;
        }

        void ILoger.Log(string log)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            _logChannel.WriteLine(log);
            Console.ForegroundColor = current;
        }

        public void Write(string log) {
            _logChannel.Write(log);
        }
    }
}
