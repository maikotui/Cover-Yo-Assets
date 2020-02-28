using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverYourAssets
{
    class GameController
    {
        public bool GameIsActive;

        private IGameView view;
        private CardSet cardSet;

        public GameController(IGameView view)
        {
            GameIsActive = true;
            this.view = view;

            int numPlayers = -1;
            while(numPlayers > 1 && numPlayers < 8)
            {
                numPlayers = view.PromptForNumberOfPlayers();
            }
            cardSet = new CardSet(view.PromptForNumberOfPlayers());
        }
    }
}
