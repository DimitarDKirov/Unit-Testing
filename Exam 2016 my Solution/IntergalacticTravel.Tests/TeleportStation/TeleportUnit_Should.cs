using IntergalacticTravel.Contracts;
using IntergalacticTravel.Exceptions;
using IntergalacticTravel.Tests.TeleportStationTests;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.TeleportStationTest
{
    [TestFixture]
    public class TeleportUnit_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithMessage_WhenunitToTeleportIsNull()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var locationsStub = new Mock<ILocation>();

            var station = new TeleportStation(ownerStub.Object, galacticMap, locationsStub.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => station.TeleportUnit(null, locationsStub.Object));
            StringAssert.Contains("unitToTeleport", exception.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessage_WhenDestibationIsNull()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var locationsStub = new Mock<ILocation>();
            var unitStub = new Mock<IUnit>();

            var station = new TeleportStation(ownerStub.Object, galacticMap, locationsStub.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => station.TeleportUnit(unitStub.Object, null));
            StringAssert.Contains("destination", exception.Message);
        }

        [Test]
        public void ThrowTeleportOutOfRangeExceptionWithMessage_WhenUnitIsFromDistantLocation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var locationStationMock = new Mock<ILocation>();
            var unitMock = new Mock<IUnit>();
            var locationStub = new Mock<ILocation>();

            locationStationMock.SetupGet(s => s.Planet.Name).Returns("Planet 1");
            locationStationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy 1");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Name).Returns("Planet 2");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Galaxy.Name).Returns("Galaxy2 2");

            var station = new TeleportStation(ownerStub.Object, galacticMap, locationStationMock.Object);

            var result = Assert.Throws<TeleportOutOfRangeException>(() => station.TeleportUnit(unitMock.Object, locationStub.Object));
            StringAssert.Contains("unitToTeleport.CurrentLocation", result.Message);
        }

        [Test]
        public void ThrowsInvalidTeleportationLocationException_WhenTargetLocationIsInUse()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var targetUnit = new Mock<IUnit>();
            var targetLocationUnits = new List<IUnit>();

            galacticMap.Add(pathMock.Object);

            targetUnit.SetupGet(u => u.CurrentLocation.Coordinates.Latitude).Returns(10.0);
            targetUnit.SetupGet(u => u.CurrentLocation.Coordinates.Longtitude).Returns(20.0);
            targetLocationUnits.Add(targetUnit.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(targetLocationUnits);
            targetUnit.SetupGet(l => l.CurrentLocation).Returns(targetLocationMock.Object);

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Name).Returns("Planet");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Galaxy.Name).Returns("Galaxy");

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);

            var result = Assert.Throws<InvalidTeleportationLocationException>(() => station.TeleportUnit(unitMock.Object, targetLocationMock.Object));
            StringAssert.Contains("units will overlap", result.Message);
        }

        [Test]
        public void ThrowsLocationNotFoundException_WhenGalaxyIsNotReachable()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Name).Returns("Planet");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Galaxy.Name).Returns("Galaxy");

            pathMock.SetupGet(p => p.TargetLocation.Planet.Galaxy.Name).Returns("Some galaxy");

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);

            var result = Assert.Throws<LocationNotFoundException>(() => station.TeleportUnit(unitMock.Object, targetLocationMock.Object));
            StringAssert.Contains("Galaxy", result.Message);
        }

        [Test]
        public void ThrowsLocationNotFoundException_WhenPlanetIsNotReachable()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Name).Returns("Planet");
            unitMock.SetupGet(c => c.CurrentLocation.Planet.Galaxy.Name).Returns("Galaxy");

            pathMock.SetupGet(p => p.TargetLocation.Planet.Galaxy.Name).Returns("Target Galaxy");
            pathMock.SetupGet(p => p.TargetLocation.Planet.Name).Returns("Some planet");

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);

            var result = Assert.Throws<LocationNotFoundException>(() => station.TeleportUnit(unitMock.Object, targetLocationMock.Object));
            StringAssert.Contains("Planet", result.Message);
        }

        [Test]
        public void ThrowsInsufficientResourcesException_WhenTeleportingUnitWithUnsufficientResources()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();

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
            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(false);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);

            var result = Assert.Throws<InsufficientResourcesException>(() => station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object));
            StringAssert.Contains("FREE LUNCH", result.Message);
        }

        [Test]
        public void RequireaPaymentFormUnit_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var costMock = new Mock<IResources>();
            var unitsCurrentLocationStub = new List<IUnit>();
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
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(costMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            unitToTeleportMock.Verify(u => u.Pay(It.IsAny<IResources>()), Times.Once);
        }

        [Test]
        public void ReceiveaPaymentFormUnitAndIncreaseResources_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var paymentMock = new Mock<IResources>();
            var unitsCurrentLocationStub = new List<IUnit>();

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

            var station = new ExtendedTeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            Assert.AreEqual(10, station.Resources.GoldCoins);
            Assert.AreEqual(20, station.Resources.SilverCoins);
            Assert.AreEqual(30, station.Resources.BronzeCoins);
        }

        [Test]
        public void SetUnitToTeleportPreviousLocation_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var costMock = new Mock<IResources>();
            var unitCurrentLocation = new Mock<ILocation>();
            var unitsInCurrentLocationStub = new List<IUnit>();
            unitsInCurrentLocationStub.Add(unitToTeleportMock.Object);

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(new List<IUnit>());

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");

            unitCurrentLocation.SetupGet(l => l.Planet.Name).Returns("Planet");
            unitCurrentLocation.SetupGet(l => l.Planet.Galaxy.Name).Returns("Galaxy");
            unitCurrentLocation.SetupGet(l => l.Planet.Units).Returns(unitsInCurrentLocationStub);

            unitToTeleportMock.SetupGet(u => u.CurrentLocation).Returns(unitCurrentLocation.Object);

            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(true);
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(costMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            unitToTeleportMock.VerifySet(u => u.PreviousLocation = unitCurrentLocation.Object);
        }

        [Test]
        public void SetUnitToTeleportCurrentLocation_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var costMock = new Mock<IResources>();
            var unitCurrentLocation = new Mock<ILocation>();
            var unitsInCurrentLocationStub = new List<IUnit>();
            unitsInCurrentLocationStub.Add(unitToTeleportMock.Object);

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(new List<IUnit>());

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");

            unitCurrentLocation.SetupGet(l => l.Planet.Name).Returns("Planet");
            unitCurrentLocation.SetupGet(l => l.Planet.Galaxy.Name).Returns("Galaxy");
            unitCurrentLocation.SetupGet(l => l.Planet.Units).Returns(unitsInCurrentLocationStub);

            unitToTeleportMock.SetupGet(u => u.CurrentLocation).Returns(unitCurrentLocation.Object);

            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(true);
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(costMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            unitToTeleportMock.VerifySet(u => u.CurrentLocation = targetLocationMock.Object);
        }

        [Test]
        public void AddThUnitToTeleportInListInTargetLocation_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var costMock = new Mock<IResources>();
            var unitCurrentLocation = new Mock<ILocation>();
            var unitsInTargetLocation = new List<IUnit>();
            var unitsInCurrentLocationStub = new List<IUnit>();
            unitsInCurrentLocationStub.Add(unitToTeleportMock.Object);

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(unitsInTargetLocation);

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");

            unitCurrentLocation.SetupGet(l => l.Planet.Name).Returns("Planet");
            unitCurrentLocation.SetupGet(l => l.Planet.Galaxy.Name).Returns("Galaxy");
            unitCurrentLocation.SetupGet(l => l.Planet.Units).Returns(unitsInCurrentLocationStub);

            unitToTeleportMock.SetupGet(u => u.CurrentLocation).Returns(unitCurrentLocation.Object);

            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(true);
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(costMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            CollectionAssert.Contains(unitsInTargetLocation, unitToTeleportMock.Object);
        }

        [Test]
        public void RemoveUnitFormCurrentLocation_WhenUnitIsReadyForTeleportation()
        {
            var ownerStub = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var stationLocationMock = new Mock<ILocation>();
            var unitToTeleportMock = new Mock<IUnit>();
            var targetLocationMock = new Mock<ILocation>();
            var pathMock = new Mock<IPath>();
            var costMock = new Mock<IResources>();
            var unitCurrentLocation = new Mock<ILocation>();
            var unitsInCurrentLocationStub = new List<IUnit>();
            unitsInCurrentLocationStub.Add(unitToTeleportMock.Object);

            galacticMap.Add(pathMock.Object);

            targetLocationMock.SetupGet(l => l.Planet.Name).Returns("Target Planet");
            targetLocationMock.SetupGet(l => l.Planet.Galaxy.Name).Returns("Target Galaxy");
            targetLocationMock.SetupGet(l => l.Coordinates.Latitude).Returns(10.0);
            targetLocationMock.SetupGet(l => l.Coordinates.Longtitude).Returns(20.0);
            targetLocationMock.SetupGet(l => l.Planet.Units).Returns(new List<IUnit>());

            stationLocationMock.SetupGet(s => s.Planet.Name).Returns("Planet");
            stationLocationMock.SetupGet(s => s.Planet.Galaxy.Name).Returns("Galaxy");

            unitCurrentLocation.SetupGet(l => l.Planet.Name).Returns("Planet");
            unitCurrentLocation.SetupGet(l => l.Planet.Galaxy.Name).Returns("Galaxy");
            unitCurrentLocation.SetupGet(l => l.Planet.Units).Returns(unitsInCurrentLocationStub);

            unitToTeleportMock.SetupGet(u => u.CurrentLocation).Returns(unitCurrentLocation.Object);

            unitToTeleportMock.Setup(u => u.CanPay(It.IsAny<IResources>())).Returns(true);
            unitToTeleportMock.Setup(u => u.Pay(It.IsAny<IResources>())).Returns(costMock.Object);

            pathMock.SetupGet(p => p.TargetLocation).Returns(targetLocationMock.Object);

            var station = new TeleportStation(ownerStub.Object, galacticMap, stationLocationMock.Object);
            station.TeleportUnit(unitToTeleportMock.Object, targetLocationMock.Object);

            CollectionAssert.DoesNotContain(unitsInCurrentLocationStub, unitToTeleportMock.Object);
        }
    }
}

