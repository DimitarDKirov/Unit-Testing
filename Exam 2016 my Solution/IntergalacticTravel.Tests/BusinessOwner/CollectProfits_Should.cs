using IntergalacticTravel.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.BusinessOwnerTests
{
    [TestFixture]
    public class CollectProfits_Should
    {
        [Test]
        public void IncreaseOwnerResources_WithSumOfOwnedStationsResources()
        {
            //Arrange
            var mockedResource = new Mock<IResources>();
            mockedResource.SetupGet(r => r.BronzeCoins).Returns(11);
            mockedResource.SetupGet(r => r.SilverCoins).Returns(22);
            mockedResource.SetupGet(r => r.GoldCoins).Returns(33);

            var mockedStation = new Mock<ITeleportStation>();
            mockedStation.Setup(s => s.PayProfits(It.IsAny<IBusinessOwner>())).Returns(mockedResource.Object);

            var owner = new BusinessOwner(1, "Mitko", new List<ITeleportStation> { mockedStation.Object });

            //Act
            owner.CollectProfits();

            //Assert
            mockedStation.Verify(s => s.PayProfits(It.Is<IBusinessOwner>(o => o.Equals(owner))), Times.Once);
            Assert.AreEqual(11, owner.Resources.BronzeCoins);
            Assert.AreEqual(22, owner.Resources.SilverCoins);
            Assert.AreEqual(33, owner.Resources.GoldCoins);
        }
    }
}
