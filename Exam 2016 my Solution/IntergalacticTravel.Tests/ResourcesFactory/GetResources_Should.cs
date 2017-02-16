using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.ResourcesFactoryTests
{
    [TestFixture]
    public class GetResources_Should
    {
        [Test]
        [TestCase("create resources gold(20) silver(30) bronze(40)")]
        [TestCase("create resources gold(20) bronze(40) silver(30)")]
        [TestCase("create resources silver(30) bronze(40) gold(20)")]
        [TestCase("create resources silver(30) gold(20) bronze(40)")]
        [TestCase("create resources bronze(40) gold(20) silver(30)")]
        [TestCase("create resources bronze(40) silver(30) gold(20)")]
        public void ReturnResourceWithCorrectProperties_WhenStringIsInCorrectFormat(string command)
        {
            //Arrange
            var factroy = new ResourcesFactory();

            //Act
            var resource = factroy.GetResources(command);

            //Assert
            Assert.AreEqual(20, resource.GoldCoins);
            Assert.AreEqual(30, resource.SilverCoins);
            Assert.AreEqual(40, resource.BronzeCoins);
        }

        [Test]
        [TestCase("create resources x y z")]
        [TestCase("tansta resources a b")]
        [TestCase("absolutelyRandomStringThatMustNotBeAValidCommand")]
        public void ThrowInvalidOperationException_withStringcommand_WhenCommandIsNotValid(string command)
        {
            //Arrange
            var factroy = new ResourcesFactory();

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => factroy.GetResources(command));
            StringAssert.Contains("command", exception.Message);
        }

        [Test]
        [TestCase("create resources silver(10) gold(97853252356623523532) bronze(20)")]
        [TestCase("create resources silver(555555555555555555555555555555555) gold(97853252356623523532999999999) bronze(20)")]
        [TestCase("create resources silver(10) gold(20) bronze(4444444444444444444444444444444444444)")]
        public void ThrowOverflowException_WhenCommandIsInCorrectFormat_WithValuesAreLarge(string command)
        {
            //Arrange
            var factroy = new ResourcesFactory();

            //Act & Assert
            Assert.Throws<OverflowException>(() => factroy.GetResources(command));
        }

    }
}
