using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace noMoreTeckmatorp2014
{
    class block:objects
    {
        public sbyte type;

        public Rectangle hitbox;

        public block(float x2, float y2, sbyte type2)
        {
            type = type2;
            setCoords(x2, y2);
            setSize(16, 16);
            switch (type)
            {
                case 1:
                    setSpriteCoords(frame(2), 1);
                    break;
                case 2:
                    setSpriteCoords(100, 17);
                    break;
                case 3:
                    setSpriteCoords(83, 17);
                    break;
            }
        }
        public void update(List<bullet> bullets)
        {
            switch (type)
            {
                case 1:
                    setSpriteCoords(frame(2), 1);
                    break;
                case 2:
                    setSpriteCoords(100, 17);
                    break;
                case 3:
                    setSpriteCoords(83, 17);
                    break;
            }
            hitbox = new Rectangle((int)x, (int)y, 16, 16);
            switch (type)
            {
                case 1:
                    setSpriteCoords(frame(2), 1);
                    break;
                case 2:
                    setSpriteCoords(83, 1);
                    break;
                case 3:
                    setSpriteCoords(83, 17);
                    break;
            }
        }
    }
}
