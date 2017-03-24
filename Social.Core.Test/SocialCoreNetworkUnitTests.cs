using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Social.Core.Test
{
    [TestFixture]
    public class SocialCoreNetworkUnitTests
    {
        private Interfaces.ICoreNetwork _network;
        private Mock<Interfaces.ILoger> _logerMock = new Mock<Interfaces.ILoger>();
        private Mock<Interfaces.IStorage> _storageMock = new Mock<Interfaces.IStorage>();

        private List<Interfaces.IUser> _users = new List<Interfaces.IUser>{
                new Models.User { UserName = "UserA" },
                new Models.User { UserName = "UserB" },
                new Models.User { UserName = "UserC" },
                new Models.User { UserName = "UserD" }
            };
        

        [SetUp]
        public void Setup()
        {
            _network = new Implementation.CoreNetwork(_storageMock.Object, _logerMock.Object);
        }

        [Test]
        public void follow_notexisting_user_warning() {
            _network.Follow(_users[0], _users[1]);
            _logerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.Once);
        }


    }
}
