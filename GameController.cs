using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverYourAssets
{
    class GameController
    {
        private IGameView view;
        public GameController(IGameView view)
        {
            this.view = view;
        }
    }
}
