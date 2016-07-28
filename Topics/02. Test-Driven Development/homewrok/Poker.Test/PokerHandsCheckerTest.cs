using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Poker.Test
{
    [TestClass]
    public class PokerHandsCheckerTest
    {
        [TestMethod]
        public void IsValidHand_ShouldReturnFalse_IfHandIsNull()
        {
            Hand hand = new Hand(null);
            PokerHandsChecker checker = new PokerHandsChecker();
            Assert.IsFalse(checker.IsValidHand(hand));
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
            Hand hand = new Hand(new List<ICard>
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
            Hand hand = new Hand(new List<ICard>
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
            Hand hand = new Hand(new List<ICard>
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
    }
}
