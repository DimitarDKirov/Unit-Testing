using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Poker.Test
{
    [TestClass]
    public class PokerHandsCheckerTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsValidHand_ShouldThrow_IfHandIsNull()
        {
            Hand hand = new Hand(null);
        }

        [TestMethod]
        public void IsValidHand_ShouldReturnFalse_IfCardsInHandAre4()
        {
            Hand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Diamonds),
                new Card(CardFace.Jack, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades)
            });
            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsValidHand(hand));
        }

        [TestMethod]
        public void IsValidHand_ShouldReturnFalse_IfCardsInHandAre6()
        {
            IHand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Diamonds),
                new Card(CardFace.Jack, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Jack, CardSuit.Hearts)
            });
            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsValidHand(hand));
        }

        [TestMethod]
        public void IsValidHand_ShouldReturnFalse_IfThereAreEqualCards()
        {
            IHand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Diamonds),
                new Card(CardFace.Jack, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Ace, CardSuit.Clubs),
               new Card(CardFace.Ace, CardSuit.Clubs),
            });
            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsValidHand(hand));
        }

        [TestMethod]
        public void IsValidHand_ShouldReturnTrue_IfHandHas5DifferentCards()
        {
            IHand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Diamonds),
                new Card(CardFace.Jack, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Ace, CardSuit.Clubs),
            });
            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsTrue(checker.IsValidHand(hand));
        }

        [TestMethod]
        public void IsFlush_ShouldReturnFalse_IfOneCardIsInDifferentSuit()
        {
            IHand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Clubs),
                new Card(CardFace.Jack, CardSuit.Clubs),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Two, CardSuit.Clubs),
            });

            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsFlush(hand));
        }

        [TestMethod]
        public void IsFlush_ShouldReturnTrue_IfAllCardsAreTheSameSuit()
        {
            IHand hand = new Hand(new List<ICard>
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Clubs),
                new Card(CardFace.Jack, CardSuit.Clubs),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Two, CardSuit.Clubs),
            });

            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsFlush(hand));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsFlush_ShouldThrowIfHandIsNull()
        {

        }
    }
}
