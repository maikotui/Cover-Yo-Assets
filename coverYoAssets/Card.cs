using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoverYourAssets
{
    public struct Card
    {
        public CardType cardType;
        public int value;

        public Card(CardType type, int value)
        {
            this.cardType = type;
            this.value = value;
        }

        public bool IsWildcard()
        {
            return cardType.Equals(CardType.Silver) || cardType.Equals(CardType.Gold);
        }

        public String GetCardTypeAsString()
        {
            return Regex.Replace(cardType.ToString(), "(\\B[A-Z])", " $1");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(GetCardTypeAsString());
            sb.Append(',');
            sb.Append('$');
            sb.Append(value);
            sb.Append(']');
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj is Card card)
            {
                return cardType == card.cardType && value == card.value;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum CardType
    {
        NullCard = 0,
        Gold,
        Silver,
        Home,
        ClassicAuto,
        Jewels,
        Stocks,
        Bank,
        CoinCollection,
        StampCollection,
        BaseballCards,
        PiggyBank,
        CashUnderTheMattress
    }
}
