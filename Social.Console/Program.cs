using Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using @W=System.Console;

namespace Social.Console
{
    class Program
    {
        static Core.Implementation.CoreNetwork _network = new Core.Implementation.CoreNetwork(new Core.Implementation.Storage(), new Core.Implementation.Log());

        static void Main(string[] args)
        {
            W.WriteLine("Enter the commands follwing the required pattern");
            W.WriteLine("Type Exit to quit");


            var methods = typeof(ICoreNetwork).GetMethods();
            var client = new Command.Client();
            while (true) {
                string action = client.RunCommand(_network);
                if (action.Equals("Exit", StringComparison.CurrentCultureIgnoreCase)) break;
                W.WriteLine();
            }

        }
       
    }
}
