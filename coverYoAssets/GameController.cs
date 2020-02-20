using System;
using System.Collections.Generic;
using System.Text;

namespace CoverYourAssets
{
    class GameController
    {
        public GameState state { get; private set; }
        public bool IsActive { get; private set; }
        public Cards Cards { get; private set; }

        public int currentPlayerID;

        public GameController(int numberOfPlayers)
        {
            state = GameState.Drawing;

            IsActive = true;

            Cards = new Cards(numberOfPlayers);

            currentPlayerID = new Random().Next(0, numberOfPlayers);
        }

        public void EndGame()
        {
            IsActive = false;
        }
    }

    public enum GameState
    {
        Drawing,
        AwaitingResponse,
        Ending
    }
}
