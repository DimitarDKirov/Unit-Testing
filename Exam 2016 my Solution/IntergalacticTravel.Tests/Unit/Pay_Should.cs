using IntergalacticTravel.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.UnitTests
{
    [TestFixture]
    public class Pay_Should
    {
        [Test]
        public void ThrowNullReferenceException_IfObjectPassedIsNUll()
        {
            //Arrange
            var unit = new Unit(1, "Mitko");

            //Act & Assert
            Assert.Throws<NullReferenceException>(()=>unit.Pay(null));
        }

        [Test]
        public void DecreaseAmountOfResourcesByAmountOfCost_WhenPay()
        {
            //Arrange
            var mockedInitialResource = new Mock<IResources>();
            mockedInitialResource.SetupGet(r => r.GoldCoins).Returns(100);
            mockedInitialResource.SetupGet(r => r.SilverCoins).Returns(200);
            mockedInitialResource.SetupGet(r => r.BronzeCoins).Returns(300);

            var mockedPayment = new Mock<IResources>();
            mockedPayment.SetupGet(r => r.GoldCoins).Returns(10);
            mockedPayment.SetupGet(r => r.SilverCoins).Returns(20);
            mockedPayment.SetupGet(r => r.BronzeCoins).Returns(30);

            var unit = new Unit(1, "Mitko");
            unit.Resources.Add(mockedInitialResource.Object);

            //Act
            unit.Pay(mockedPayment.Object);

            //Assert
            Assert.AreEqual(mockedInitialResource.Object.GoldCoins - mockedPayment.Object.GoldCoins, unit.Resources.GoldCoins);
            Assert.AreEqual(mockedInitialResource.Object.SilverCoins - mockedPayment.Object.SilverCoins, unit.Resources.SilverCoins);
            Assert.AreEqual(mockedInitialResource.Object.BronzeCoins - mockedPayment.Object.BronzeCoins, unit.Resources.BronzeCoins);
        }

        [Test]
        public void ReturnInstanceofIResourcesWithAmountOfCost_WhenPay()
        {
            //Arrange
            var mockedInitialResource = new Mock<IResources>();
            mockedInitialResource.SetupGet(r => r.GoldCoins).Returns(100);
            mockedInitialResource.SetupGet(r => r.SilverCoins).Returns(200);
            mockedInitialResource.SetupGet(r => r.BronzeCoins).Returns(300);

            var mockedPayment = new Mock<IResources>();
            mockedPayment.SetupGet(r => r.GoldCoins).Returns(10);
            mockedPayment.SetupGet(r => r.SilverCoins).Returns(20);
            mockedPayment.SetupGet(r => r.BronzeCoins).Returns(30);

            var unit = new Unit(1, "Mitko");
            unit.Resources.Add(mockedInitialResource.Object);

            //Act
            var result=unit.Pay(mockedPayment.Object);

            //Assert
            Assert.AreNotSame(result, mockedPayment.Object);
            Assert.AreEqual(mockedPayment.Object.GoldCoins, result.GoldCoins);
            Assert.AreEqual( mockedPayment.Object.SilverCoins, result.SilverCoins);
            Assert.AreEqual(mockedPayment.Object.BronzeCoins, result.BronzeCoins);
        }
    }
}
