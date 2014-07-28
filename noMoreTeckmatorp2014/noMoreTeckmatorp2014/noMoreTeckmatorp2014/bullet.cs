using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace noMoreTeckmatorp2014
{
    class bullet:objects
    {
        public sbyte onTank;

        public bool hitWall;

        sbyte hp;

        public Rectangle hitboxs;

        public bullet(float ang, float x2, float y2, sbyte onTank2)
        {
            hp = 3;
            setCoords(x2, y2);
            setSpriteCoords(frame(2), 17);
            setSize(5, 5);
            angle = ang;
            speed = 7;
            onTank = onTank2;
        }

        public void update(List<block> blocks)
        {
            hitboxs = new Rectangle((int)x, (int)y, 5, 5);
            if (hp <= 0)
            {
                destroy = true;
            }
            foreach (block bl in blocks)
            {
                if (hitboxs.Intersects(bl.hitbox))
                {
                    if (bl.type == 1)
                    {
                        destroy = true;
                    }
                    
                    if (bl.type == 3)
                    {
                        bl.type = 2;
                        hp -= 1;
                    }
                }
            }
        }

        public void movment()
        {
            AngleMath();
            x += veclocity_x;
            y += veclocity_y;
        }
        
    }
}
