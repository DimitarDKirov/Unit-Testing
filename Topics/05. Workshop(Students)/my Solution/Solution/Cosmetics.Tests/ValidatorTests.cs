using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cosmetics.Common;
using Cosmetics.Products;

namespace Cosmetics.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckIfNull_ShouldThrowNullReferenceException_IfCalledWithNull()
        {
            Validator.CheckIfNull(null);
        }

        [TestMethod]
         public void CheckIfNull_ShouldNotThrow_IfCalledWithWithObject()
        {
            var shampoo = new Shampoo("test", "test", 0, GenderType.Men, 0, UsageType.EveryDay);
            Validator.CheckIfNull(shampoo);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckIfStringIsNullOrEmpty_ShouldThrowNullReferenceException_WhenParameterIsEmpty()
        {
            Validator.CheckIfStringIsNullOrEmpty(string.Empty);
        }

        [TestMethod]
        public void CheckIfStringIsNullOrEmpty_ShouldNotThrow_WhenParameterIsString()
        {
            Validator.CheckIfStringIsNullOrEmpty("test");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CheckIfStringLengthIsValid_ShouldThrowIndexOutOfRangeException_WhenParameterLengthIsLow()
        {
            Validator.CheckIfStringLengthIsValid("a", 2, 12);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CheckIfStringLengthIsValid_ShouldThrowIndexOutOfRangeException_WhenParameterLengthIsHigh()
        {
            Validator.CheckIfStringLengthIsValid(new string('a', 50), 2, 12);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CheckIfStringLengthIsValid_ShouldNotThrow_WhenParameterLengthIsOK()
        {
            Validator.CheckIfStringLengthIsValid("abcde", 2, 12);
        }
    }
}
