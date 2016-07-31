using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class PokerHandsChecker : IPokerHandsChecker
    {
        public bool IsValidHand(IHand hand)
        {
            if (hand == null)
                throw new NullReferenceException();
            if (hand.Cards == null)
                return false;

            if (hand.Cards.Count != 5)
                return false;

            for (int i = 0; i < hand.Cards.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Cards.Count; j++)
                {
                    if (hand.Cards[i].Face == hand.Cards[j].Face && hand.Cards[i].Suit == hand.Cards[j].Suit)
                        return false;
                }
            }

            return true;
        }

        public bool IsStraightFlush(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsFourOfAKind(IHand hand)
        {
            if (hand == null)
            {
                throw new NullReferenceException();
            }

            if (!this.IsValidHand(hand))
                return false;
            var h = hand.Cards.GroupBy(c => c.Face);
            if (hand.Cards.GroupBy(c => c.Face).Any(g => g.Count() == 4))
                return true;

            return false;
        }

        public bool IsFullHouse(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsFlush(IHand hand)
        {
            if (hand == null)
            {
                throw new NullReferenceException();
            }

            if (!this.IsValidHand(hand))
                return false;

            if (hand.Cards.Any(c => c.Suit != hand.Cards[0].Suit))
                return false;

            return true;
        }

        public bool IsStraight(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsThreeOfAKind(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsTwoPair(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsOnePair(IHand hand)
        {
            throw new NotImplementedException();
        }

        public bool IsHighCard(IHand hand)
        {
            throw new NotImplementedException();
        }

        public int CompareHands(IHand firstHand, IHand secondHand)
        {
            throw new NotImplementedException();
        }
    }
}
