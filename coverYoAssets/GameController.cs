using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoverYourAssets
{
    class GameController
    {
        public bool IsActive { get; private set; }
        private CardSet cards;

        public bool CurrentPlayerCanTakeFromPile
        {
            get
            {
                foreach (Card card in cards.GetPlayersHand(currentPlayerID))
                {
                    if(card.cardType == cards.Pile.Peek().cardType)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int playersCount;

        public int currentPlayerID;

        public GameController(int numberOfPlayers)
        {
            playersCount = numberOfPlayers;

            IsActive = true;

            cards = new CardSet(numberOfPlayers);

            currentPlayerID = new Random().Next(0, numberOfPlayers);
        }

        public List<Card> GetPlayersHand(int playerID)
        {
                
        }

        public void GetPlayersTopAsset(int playerID)
        {
            
        }

        public Card? GetTopOfPile()
        {
            if(cards.Pile.TryPeek(out Card card))
            {
                return card;
            }
            else
            {
                return null;
            }
        }

        public void EndGame()
        {
            IsActive = false;
        }

        private class CardSet
        {
            private Stack<Card> deck;
            private Stack<Card> pile;

            private List<Card[]> playersCards;
            private List<Stack<Queue<Card>>> playersAssets;

            public CardSet(int numberOfPlayers)
            {
                BuildDeck();
                DealCards(numberOfPlayers);
                pile = new Stack<Card>();
                pile.Push(deck.Pop());
            }

            private void BuildDeck()
            {
                deck = new Stack<Card>(110);
                for (int i = 0; i < 10; i++)
                {
                    if (i < 4)
                    {
                        deck.Push(new Card(CardType.Gold, 50_000));
                    }
                    if (i < 8)
                    {
                        deck.Push(new Card(CardType.Silver, 25_000));
                        deck.Push(new Card(CardType.Home, 20_000));
                    }
                    deck.Push(new Card(CardType.ClassicAuto, 15_000));
                    deck.Push(new Card(CardType.Jewels, 15_000));
                    deck.Push(new Card(CardType.Stocks, 10_000));
                    deck.Push(new Card(CardType.Bank, 10_000));
                    deck.Push(new Card(CardType.CoinCollection, 10_000));
                    deck.Push(new Card(CardType.StampCollection, 5_000));
                    deck.Push(new Card(CardType.BaseballCards, 5_000));
                    deck.Push(new Card(CardType.PiggyBank, 5_000));
                    deck.Push(new Card(CardType.CashUnderTheMattress, 5_000));
                }

                Random rnd = new Random();
                var values = deck.ToArray();
                deck.Clear();
                foreach (var value in values.OrderBy(x => rnd.Next()))
                {
                    deck.Push(value);
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
                        hand[j] = deck.Pop();
                    }
                    playersCards.Add(hand);

                    playersAssets.Add(new Stack<Queue<Card>>());
                }
            }

            public List<Card> GetDeck()
            {
                return deck.ToList<Card>();
            }

            public List<Card[]> GetAllPlayersHands()
            {
                return playersCards;
            }

            
        }

    }
}
