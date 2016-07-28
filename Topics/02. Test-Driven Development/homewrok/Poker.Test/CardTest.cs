using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Poker.Test
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void Card_ToStringShouldWorkWithCardAceHearts()
        {
            Card card = new Card(CardFace.Ace, CardSuit.Hearts);
            var expectedResult = "Ace.Hearts";
            Assert.AreEqual(expectedResult, card.ToString());
        }

        [TestMethod]
        public void Card_ToStringShouldWorkWithCardNine()
        {
            Card card = new Card(CardFace.Nine, CardSuit.Diamonds);
            var expectedResult = "Nine.Diamonds";
            Assert.AreEqual(expectedResult, card.ToString());
        }

        [TestMethod]
        public void Card_ToStringShouldWorkWithCardTwoClubs()
        {
            Card card = new Card(CardFace.Two, CardSuit.Clubs);
            var expectedResult = "Two.Clubs";
            Assert.AreEqual(expectedResult, card.ToString());
        }

        [TestMethod]
        public void Card_ToStringShouldWorkWithCardKingSpades()
        {
            Card card = new Card(CardFace.King, CardSuit.Spades);
            var expectedResult = "King.Spades";
            Assert.AreEqual(expectedResult, card.ToString());
        }
    }
}
