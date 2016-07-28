using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Test
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void Hand_ToStringShoulShowThe5CardsInTheHandDividedWithDash()
        {
            Hand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Diamonds),
                new Card(CardFace.Jack, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Ten, CardSuit.Diamonds)
            });

            var expectedString = "Ace.Clubs-Eight.Diamonds-Jack.Hearts-Queen.Spades-Ten.Diamonds";
            Assert.AreEqual(expectedString, hand.ToString());
        }

        [TestMethod]
        public void Hand_ToStringShouldDisplayOnlyOneCardWithoutDashes_WhenTheCardIsOnlyOne()
        {
            Hand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Hearts)
            });
            Assert.AreEqual("Ace.Hearts", hand.ToString());
        }

        [TestMethod]
        public void Hand_ToStringShouldShowEmptyString_IfNoCard()
        {
            Hand hand = new Hand(null);
            Assert.AreEqual(string.Empty, hand.ToString());
        }
    }
}

