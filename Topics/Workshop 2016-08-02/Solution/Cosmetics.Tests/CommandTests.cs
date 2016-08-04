using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cosmetics.Engine;
using System.Collections.Generic;

namespace Cosmetics.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void ParseShouldReturnValidCommand_WhenInputIsValid()
        {
            string commandString = "Add";
            var result = Command.Parse(commandString);
            Assert.IsInstanceOfType(result, typeof(Command));
        }

        [TestMethod]
        public void Parse_ShouldSetSCorrectValues_ToCommand()
        {
            string commandString = "Add param1 param2";
            var result = Command.Parse(commandString);
            Assert.AreEqual("Add", result.Name);
            Assert.AreEqual("param1", result.Parameters[0]);
            Assert.AreEqual("param2", result.Parameters[1]);
        }

        [TestMethod]
        public void Parse_ShoudThrowArgumentNullExceptionWithMessage_WhenInputIsNullOrEmpty()
        {
            Exception ex = null;
            string message = null;
            try
            {
                var result = Command.Parse(string.Empty);
            }
            catch (Exception e)
            {
                ex = e;
                message = e.Message;
            }
            Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            //Assert.IsTrue(message.IndexOf("Name") > 0);
            StringAssert.Contains(message, "Name");
        }

        [TestMethod]
        public void Parse_ShouldThrowArgumentNullException_WhenCommandParametersStringIsNull()
        {
            Exception ex = null;
            string message = null;
            try
            {
                var result = Command.Parse("Add ");
            }
            catch (Exception e)
            {
                ex = e;
                message = e.Message;
            }
            Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            StringAssert.Contains(message, "List");
        }
    }
}
