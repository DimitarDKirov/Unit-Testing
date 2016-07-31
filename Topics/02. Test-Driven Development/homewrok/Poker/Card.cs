using System;

namespace Poker
{
    public class Card : ICard
    {
        public CardFace Face { get; private set; }
        public CardSuit Suit { get; private set; }

        public Card(CardFace face, CardSuit suit)
        {
            this.Face = face;
            this.Suit = suit;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Face, this.Suit);
        }

        //public override bool Equals(object obj)
        //{
        //    var otherCard = obj as Card;
        //    if (otherCard == null)
        //        return false;

        //    return this.Face == otherCard.Face && this.Suit == otherCard.Suit;
        //}
    }
}
