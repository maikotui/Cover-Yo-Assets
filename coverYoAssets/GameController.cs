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
            return new List<Card>(cards.AllPlayersCards[playerID]);
        }

        public bool CreateAssetFromHand(int playerID, int card1ID, int card2ID)
        {
            List<Card> playersHand = cards.AllPlayersCards[playerID];
            if (playersHand[card1ID].Equals(playersHand[card2ID]))
            {
                //TODO: complete implementation
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateAssetFromPile(int playerID, int cardID)
        {
            List<Card> playersHand = cards.AllPlayersCards[playerID];
            if (TryGetTopOfPile(out Card topOfPile) && playersHand[cardID].Equals(topOfPile))
            {
                //TODO: complete implementation
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetPlayersTopAsset(int playerID, out Card topCard)
        {
            if (cards.AllPlayersAssets[playerID].TryPeek(out Queue<Card> assetPile))
            {
                return assetPile.TryPeek(out topCard);
            }
            topCard = default;
            return false;
        }

        public bool TryGetTopOfPile(out Card topOfPile)
        {
            return cards.Pile.TryPeek(out topOfPile);
        }

        public void EndGame()
        {
            IsActive = false;
        }

        private class CardSet
        {
            public Stack<Card> Deck { get; private set; }
            public Stack<Card> Pile { get; private set; }

            public List<List<Card>> AllPlayersCards { get; private set; }
            public List<Stack<Queue<Card>>> AllPlayersAssets { get; private set; }

            public CardSet(int numberOfPlayers)
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
                AllPlayersCards = new List<List<Card>>();
                AllPlayersAssets = new List<Stack<Queue<Card>>>();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    List<Card> hand = new List<Card>();
                    for (int j = 0; j < 4; j++)
                    {
                        hand.Add(Deck.Pop());
                    }
                    AllPlayersCards.Add(hand);

                    AllPlayersAssets.Add(new Stack<Queue<Card>>());
                }
            }
        }

    }
}
