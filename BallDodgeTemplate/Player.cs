using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallDodgeTemplate
{
    internal class Player
    {
        public int x, y;
        public int width = 30;
        public int height = 10;
        public int speed = 6;

        public Player()
        {
            x = GameScreen.screenWidth / 2 - width / 2;
            y = GameScreen.screenHeight / 2 - height / 2;
        }

    }
}
