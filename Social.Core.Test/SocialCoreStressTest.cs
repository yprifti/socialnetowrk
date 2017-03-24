using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Test
{
    [TestFixture]
    public class SocialCoreStressTest
    {
        private static Interfaces.ICoreNetwork _network = new Implementation.CoreNetwork(new Implementation.Storage(), new Implementation.Log());
        
        [SetUp]
        public void Setup() {

        }

        //Benchmark: Single Thread, Azure, 4MB Ram, 2 Core
        //10K  : 5  Seconds
        //100K : 52 Seconds
        //1M   : 

        [Test]
        public void add_1M_users_2M_messages_1M_subscriptions() {
            //List<Task> _all = new List<Task>();
            
            for (int i = 0; i < 1000; i++) {
              //  Task t =Task.Factory.StartNew(() => { });


                {
                    var usr = new Models.User { UserName = "User" + i.ToString() };
                    var msg = new Models.Message { MessageText = "MSG" + i.ToString() };
                    var msg1 = new Models.Message { MessageText = "MSG" + (i * 2).ToString() };

                    var prevUsr = new Models.User { UserName = "User" + (i - 1).ToString() };

                    _network.Post(usr, msg);
                    _network.Post(prevUsr, msg1);
                    _network.Follow(usr, prevUsr);
                    _network.Wall(usr);
                    _network.Read(prevUsr);
                }

            //    _all.Add(t);
            }
           // Task.WaitAll(_all.ToArray());
        }
        
    }

}
