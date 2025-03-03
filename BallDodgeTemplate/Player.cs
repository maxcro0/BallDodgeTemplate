﻿using System.Drawing;

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

        public void Move(string direction)
        {

            if (direction == "right" && x <= GameScreen.screenWidth + width)
            {
                x += speed;
            }

            if (direction == "left" && x >= 0)
            {
                x -= speed;
            }

            if (direction == "up" && y >= 0)
            {
                y -= speed;
            }

            if (direction == "down" && y <= GameScreen.screenHeight - height)
            {
                y += speed;
            }
        }

        public bool Collision(Ball b)
        {

            Rectangle heroRec = new Rectangle(x, y, width, height);
            Rectangle chaseRec = new Rectangle(b.x, b.y, b.size, b.size);

            if (heroRec.IntersectsWith(chaseRec))
            {
                b.ySpeed = b.ySpeed * -1;
                return true;
            }

            return false;

        }
    }
}
