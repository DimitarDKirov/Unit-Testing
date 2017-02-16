using IntergalacticTravel.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.TeleportStationTests
{
    [TestFixture]
    public class PayProfits_Should
    {
        [Test]
        public void ReturnTheTotalAmountsOfProfits_WhenCalledWithOwner()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var paymentMock = new Mock<IResources>();
            var unitsCurrentLocationStub = new List<IUnit>();

            ownerStub.SetupGet(o => o.IdentificationNumber).Returns(1);
            paymentMock.SetupGet(p => p.GoldCoins).Returns(10);
            paymentMock.SetupGet(p => p.SilverCoins).Returns(20);
            paymentMock.SetupGet(p => p.BronzeCoins).Returns(30);

            unitsCurrentLocationStub.Add(unitToTeleportMock.Object);

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(new List<IUnit>());

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");
            unitToTeleportMock.SetupGet(c => c.CurrentLocation.Planet.Name).Returns("Planet");
            unitToTeleportMock.SetupGet(c => c.CurrentLocation.Planet.Galaxy.Name).Returns("Galaxy");
            unitToTeleportMock.SetupGet(u => u.CurrentLocation.Planet.Units).Returns(unitsCurrentLocationStub);
            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(true);
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(paymentMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);
            var result = station.PayProfits(ownerStub.Object);

            Assert.AreEqual(10, result.GoldCoins);
            Assert.AreEqual(20, result.SilverCoins);
            Assert.AreEqual(30, result.BronzeCoins);
        }
    }
}
