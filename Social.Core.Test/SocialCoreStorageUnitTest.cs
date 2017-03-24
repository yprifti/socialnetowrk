using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Core.Test
{
    [TestFixture]
    public class SocialCoreStorageUnitTest
    {
        private Interfaces.IStorage _storage = new Implementation.Storage();

        private List<Interfaces.IUser> _users = new List<Interfaces.IUser>{
                new Models.User { UserName = "UserA" },
                new Models.User { UserName = "UserB" },
                new Models.User { UserName = "UserC" },
                new Models.User { UserName = "UserD" }
            };

        private List<Interfaces.IMessage> _messages = new List<Interfaces.IMessage> {
            new Models.Message { MessageText = "Msg 1 - Just Testing 1" },
            new Models.Message { MessageText = "Msg 2 - Just Testing 2" },
            new Models.Message { MessageText = "Msg 3 - Just Testing 3" },
            new Models.Message { MessageText = "Msg 4 - Just Testing 4" }
        };

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void non_existing_user_returns_false() {

            var usr = new Models.User { UserName = "Not Existing User" }; 
            Assert.IsFalse(_storage.Exists(usr));
        }

        [Test]
        public void add_user_existing_user_returns_true()
        {
            var usr1 = new Models.User { UserName = "TmpUsr1" };
            var usr2 = new Models.User { UserName = "TmpUsr1" };

            bool added = _storage.Add(usr1);
            bool exist = _storage.Exists(usr2);

            Assert.IsTrue(usr1.Equals(usr2));
            Assert.IsTrue(added);
            Assert.IsTrue(exist);

        }

        [Test]
        public void existing_user_posts_and_return() {
            var usr = _users.First();

            _storage.Add(usr);
            _storage.Post(usr, _messages[0]);
            _storage.Post(usr, _messages[1]);

            var _returnMessages = _storage.Read(usr);
            Assert.AreEqual(_returnMessages.Count(), 2);
            Assert.AreEqual(_returnMessages.First().MessageText, _messages[0].MessageText);
            Assert.AreEqual(_returnMessages.Last().MessageText, _messages[1].MessageText);

        }

        [Test]
        public void user_subscribe_and_see_correctwall() {
            _users.ForEach(x => _storage.Add(x));

            _storage.Subscribe(_users[0], _users[3]);
            _storage.Subscribe(_users[0], _users[2]);

            _storage.Subscribe(_users[1], _users[2]);


            _storage.Post(_users[3], _messages[0]);
            _storage.Post(_users[3], _messages[1]);

            _storage.Post(_users[2], _messages[2]);
            _storage.Post(_users[2], _messages[3]);


            var wallUsr0 = _storage.Wall(_users[0]);
            var wallUsr1 = _storage.Wall(_users[1]);

            Assert.AreEqual(wallUsr0.Count(), 4);
            Assert.AreEqual(wallUsr1.Count(), 2);
            Assert.AreEqual(wallUsr0.Last().MessageText, _messages[3].MessageText);
            Assert.AreEqual(wallUsr1.First().MessageText, _messages[2].MessageText);



        }
        

    }

}
