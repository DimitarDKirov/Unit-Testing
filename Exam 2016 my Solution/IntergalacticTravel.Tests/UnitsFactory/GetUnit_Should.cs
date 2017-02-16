using IntergalacticTravel.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergalacticTravel.Tests.UnitsFactoryTests
{
    [TestFixture]
    public class GetUnit_Should
    {
        [Test]
        public void Return_newProcyon_WhenValidCommandisPassed()
        {
            //Arrange
            var factory = new UnitsFactory();

            //Act
            var unit = factory.GetUnit("create unit Procyon Gosho 1");

            //Assert
            Assert.IsInstanceOf<Procyon>(unit);
        }

        [Test]
        public void Return_newLuyten_WhenValidCommandisPassed()
        {
            //Arrange
            var factory = new UnitsFactory();

            //Act
            var unit = factory.GetUnit("create unit Luyten Pesho 2");

            //Assert
            Assert.IsInstanceOf<Luyten>(unit);
        }

        [Test]
        public void Return_newLacaille_WhenValidCommandisPassed()
        {
            //Arrange
            var factory = new UnitsFactory();

            //Act
            var unit = factory.GetUnit("create unit Lacaille Tosho 3");

            //Assert
            Assert.IsInstanceOf<Lacaille>(unit);
        }

        [Test]
        [TestCase("create unit NotexistsLacaille Tosho 3")]
        [TestCase("create unit Lacaille Tosho InvalidId")]
        public void ThrowInvalidUnitCreationCommandException_WhenCommandIsNotValid(string command)
        {

            //Arrange
            var factory = new UnitsFactory();

            //Act & Assert
            Assert.Throws<InvalidUnitCreationCommandException>(() => factory.GetUnit(command));
        }
    }

}
