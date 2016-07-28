using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Poker.Test
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void Card_ToStringShouldWork()
        {
            Card card = new Card(CardFace.Ace, CardSuit.Hearts);
            var expectedResult = "Ace.Hearts";
            Assert.AreEqual(expectedResult, card.ToString());
        }
    }
}
