using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Console.Command
{
    public class Client
    {
        public string RunCommand(object destination) {
            Invoker invoker = new Invoker();
            Interpreter interpreter = new Interpreter(invoker.CommandText);
            Executor executor = new Executor(interpreter);
            executor.Execute(destination);
            return invoker.CommandText;
        }
    }

    public class Invoker {

        public string CommandText { get; set; }
        public Invoker() {
            CommandText = System.Console.ReadLine();
        }
    }
    public class Interpreter
    {
        private string _commandText;
        public Interpreter(string commandText) {
            _commandText = commandText;
        }

        public List<object> Get {
            get {
                if (string.IsNullOrEmpty(_commandText)) return null;
                if (_commandText.IndexOf("->") > -1)
                {
                    var commands = _commandText.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length != 2) return null;
                    return new List<object> { "Post", new Core.Models.User { UserName = commands[0].Trim() }, new Core.Models.Message { MessageText = commands[1] } };
                }
                else if (_commandText.IndexOf("follows", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    var commands = _commandText.Split(new string[] { "follows" }, StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length != 2) return null;
                    return new List<object> { "Follow", new Core.Models.User { UserName = commands[0].Trim() }, new Core.Models.User { UserName = commands[1].Trim() } };
                }
                else if (_commandText.IndexOf("wall", StringComparison.CurrentCultureIgnoreCase) > -1) {
                    var commands = _commandText.Split(new string[] { "wall" }, StringSplitOptions.RemoveEmptyEntries);
                    if (commands.Length != 1) return null;
                    return new List<object> { "Wall", new Core.Models.User { UserName = commands[0].Trim() }};
                } else
                    return new List<object> { "Read", new Core.Models.User { UserName = _commandText.Trim() } };
            }
        }
    }

    public class Executor {
        private Interpreter _interpreter;
        public Executor(Interpreter interpreter) {
            _interpreter = interpreter;
        }
        public void Execute(object destination) {
            if (destination == null) return;
            var action = _interpreter.Get;
            if (action == null || !action.Any()) return;
            var actionCommand = action.First().ToString();
            action.Remove(action.First());

            try
            {
                var method = destination.GetType().GetMethod(actionCommand);
                if (method == null) {
                    System.Console.WriteLine("Method not found");
                    return;
                }

                method.Invoke(destination, action.ToArray());

            }
            catch (Exception ex){
                System.Console.WriteLine("Command Failed" + ex.Message);
            }


        }
    }
}
