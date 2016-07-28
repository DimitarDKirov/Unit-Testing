using NUnit.Framework;
using Santase.Logic.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Test
{
    [TestFixture]
    class DeckTest
    {
        [Test]
        public void Deck_MustContain24CardsOnLoad()
        {
            var deck = new Deck();
            Assert.AreEqual(24, deck.CardsLeft);
        }

        [Test]
        public void Deck_TrumpCardMustNotBeNull()
        {
            var deck = new Deck();
            Assert.IsNotNull(deck.TrumpCard);
        }

        [TestCase(0, 24)]
        [TestCase(1, 23)]
        [TestCase(12, 12)]
        [TestCase(23, 1)]
        [TestCase(24, 0)]
        public void Deck_GetSeveralCardsPlusCardsLeftInTheDeckMustBe24(int cardsToGet, int cardsRemain)
        {
            Deck deck = new Deck();
            for (int i = 0; i < cardsToGet; i++)
            {
                deck.GetNextCard();
            }
            Assert.AreEqual(cardsRemain, deck.CardsLeft);
        }

        [Test]
        public void Deck_Get25CardsMustThrow()
        {
            Deck deck = new Deck();
            for (int i = 0; i < 24; i++)
            {
                deck.GetNextCard();
            }
            Assert.Throws<InternalGameException>(() => deck.GetNextCard());
        }

        [Test]
        public void Deck_ChangeTrumpCard_ShouldWork()
        {
            Deck deck = new Deck();
            Card newTrumpCard = new Card(CardSuit.Club, CardType.Ace);
            deck.ChangeTrumpCard(newTrumpCard);
            Assert.AreSame(newTrumpCard, deck.TrumpCard);
        }

        [Test]
        public void Deck_TrumpCardMustBeTheLastCardinDeck()
        {
            Deck deck = new Deck();
            Card trumpCard = deck.TrumpCard;
            while (deck.CardsLeft > 1)
            {
                deck.GetNextCard();
            }
            var lastCard = deck.GetNextCard();
            Assert.AreSame(trumpCard, lastCard);
        }
    }
}
