using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoverYourAssets
{
    public class Cards
    {
        public Stack<Card> Deck { get; private set; }
        public Stack<Card> Pile { get; private set;  }

        private List<Card[]> playersCards;
        private List<Stack<Queue<Card>>> playersAssets;

        public Cards(int numberOfPlayers)
        {
            BuildDeck();
            DealCards(numberOfPlayers);
            Pile = new Stack<Card>();
            Pile.Push(Deck.Pop());
        }

        private void BuildDeck()
        {
            Deck = new Stack<Card>(110);
            for (int i = 0; i < 10; i++)
            {
                if (i < 4)
                {
                    Deck.Push(new Card(CardType.Gold, 50_000));
                }
                if (i < 8)
                {
                    Deck.Push(new Card(CardType.Silver, 25_000));
                    Deck.Push(new Card(CardType.Home, 20_000));
                }
                Deck.Push(new Card(CardType.ClassicAuto, 15_000));
                Deck.Push(new Card(CardType.Jewels, 15_000));
                Deck.Push(new Card(CardType.Stocks, 10_000));
                Deck.Push(new Card(CardType.Bank, 10_000));
                Deck.Push(new Card(CardType.CoinCollection, 10_000));
                Deck.Push(new Card(CardType.StampCollection, 5_000));
                Deck.Push(new Card(CardType.BaseballCards, 5_000));
                Deck.Push(new Card(CardType.PiggyBank, 5_000));
                Deck.Push(new Card(CardType.CashUnderTheMattress, 5_000));
            }

            Random rnd = new Random();
            var values = Deck.ToArray();
            Deck.Clear();
            foreach (var value in values.OrderBy(x => rnd.Next()))
            {
                Deck.Push(value);
            }
        }

        private void DealCards(int numberOfPlayers)
        {
            playersCards = new List<Card[]>();
            playersAssets = new List<Stack<Queue<Card>>>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Card[] hand = new Card[4];
                for (int j = 0; j < 4; j++)
                {
                    hand[j] = Deck.Pop();
                }
                playersCards.Add(hand);

                playersAssets.Add(new Stack<Queue<Card>>());
            }
        }

        public Card[] GetPlayersHand(int playerID)
        {
            return playersCards[playerID];
        }
        public Stack<Queue<Card>> GetPlayersAssets(int playerID)
        {
            return playersAssets[playerID];
        }
    }

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
    }
}
