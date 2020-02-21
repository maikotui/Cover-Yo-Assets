﻿using System;
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
    }

    public enum CardType
    {
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
        CashUnderTheMattress,
        NullCard = 0
    }
}