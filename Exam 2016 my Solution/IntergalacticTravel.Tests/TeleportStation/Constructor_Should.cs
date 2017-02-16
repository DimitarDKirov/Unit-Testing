using IntergalacticTravel.Contracts;
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
    class Constructor_Should
    {
        [Test]
        public void SetAllFields_WhenParametersAreCorrect()
        {
            var mockedOwner = new Mock<IBusinessOwner>();
            var galacticMap = new List<IPath>();
            var mockedLocation = new Mock<ILocation>();

            var station = new ExtendedTeleportStation(mockedOwner.Object, galacticMap, mockedLocation.Object);

            Assert.AreSame(mockedOwner.Object, station.Owner);
            Assert.AreSame(galacticMap, station.GalacticMap);
            Assert.AreSame(mockedLocation.Object, station.Location);
        }
    }
}
