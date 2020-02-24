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

        public void MoveToNextPlayer()
        {
            if (++currentPlayerID == playersCount)
            {
                currentPlayerID = 0;
            }
        }

        public bool TryCreateAssetFromHand(int playerID, int card1ID, int card2ID)
        {
            // Try to get the players hand, if not then return false
            List<Card> playersHand = _TryGetPlayersHand(playerID);
            if (playersHand != null)
            {
                // Get the two cards if they are valid cardIDs (simplifies below code)
                Card card1 = card1ID >= 0 && card1ID < playersHand.Count ? playersHand[card1ID] : new Card(CardType.NullCard, 0);
                Card card2 = card2ID >= 0 && card2ID < playersHand.Count ? playersHand[card2ID] : new Card(CardType.NullCard, 0);

                // If these cards can create an asset, start creating the asset. Otherwise, return false.
                if (CardsCanCreateAsset(card1, card2))
                {
                    Queue<Card> smallAssetPile = new Queue<Card>();

                    // Put the card on top that is NOT a wildcard
                    if (!card1.IsWildcard())
                    {
                        smallAssetPile.Enqueue(playersHand[card1ID]);
                        smallAssetPile.Enqueue(playersHand[card2ID]);
                    }
                    else
                    {
                        smallAssetPile.Enqueue(playersHand[card2ID]);
                        smallAssetPile.Enqueue(playersHand[card1ID]);
                    }

                    // Remove cards from players hand now that they're in pile
                    // Note: The order matters since we're removing by ID. If I removed the smaller ID first, then the second ID could be offset by the removal of the first.
                    if (card1ID > card2ID)
                    {
                        playersHand.RemoveAt(card1ID);
                        playersHand.RemoveAt(card2ID);
                    }
                    else if (card2ID > card1ID)
                    {
                        playersHand.RemoveAt(card2ID);
                        playersHand.RemoveAt(card1ID);
                    }
                    else // If the same card was given, return false.
                    {
                        return false;
                    }

                    // Add the cards to the players asset pile and return true
                    cards.AllPlayersAssets[playerID].Push(smallAssetPile);

                    RefillAllHands();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool TryDiscardFromHand(int playerID, int cardID)
        {
            List<Card> playersHand = _TryGetPlayersHand(playerID);

            if(playersHand != null && cardID >= 0 && cardID < playersHand.Count)
            {
                cards.Pile.Push(playersHand[cardID]);
                playersHand.RemoveAt(cardID);
                RefillAllHands();
                return true;
            }

            return false;
        }

        public void RefillAllHands()
        {
            foreach(List<Card> hand in cards.AllPlayersCards)
            {
                while(hand.Count < 4)
                {
                    if(cards.Deck.TryPop(out Card topOfDeck))
                    {
                        hand.Add(topOfDeck);
                    }
                }
            }
        }

        public bool CardsCanCreateAsset(Card card1, Card card2)
        {
            if (card1.cardType.Equals(card2.cardType)) // cards are equal
            {
                if (card1.IsWildcard() && card2.IsWildcard()) // both are wildcards (can't do that)
                {
                    return false;
                }
                else // cards are equal and are not both wildcards
                {
                    return true;
                }
            }
            else if (card1.IsWildcard() || card2.IsWildcard()) // cards are not equal and one of them is a wildcard
            {
                return true;
            }
            else // cards are not equal and neither are wildcards
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

        public bool TryGetPlayersHand(int playerID, out List<Card> hand)
        {
            List<Card> handRef = _TryGetPlayersHand(playerID);

            hand = handRef == null ? null : new List<Card>(handRef);

            return handRef != null;
        }

        private List<Card> _TryGetPlayersHand(int playerID)
        {
            if (playerID >= 0 && playerID < cards.AllPlayersCards.Count)
            {
                return cards.AllPlayersCards[playerID];
            }
            return null;
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
