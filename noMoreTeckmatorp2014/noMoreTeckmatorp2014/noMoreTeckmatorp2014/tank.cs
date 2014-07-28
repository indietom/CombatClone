using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace noMoreTeckmatorp2014
{
    class tank:objects
    {
        const float maxAccel = 3;
        const float minAccel = -3;

        public sbyte controllSch;

        public float accel;

        public bool inputActive;

        public Rectangle hitbox;

        public Color color;

        KeyboardState keyboard;

        Keys accelerate;
        Keys reverse;
        Keys left;
        Keys right;
        Keys fire;  

        public tank(sbyte controllSch2)
        {
            inputActive = true;
            setSize(32, 32);
            controllSch = controllSch2;
            if (controllSch == 1)
            {
                setSpriteCoords(1, 1);
                setCoords(64, 240);
                accelerate = Keys.Up;
                reverse = Keys.Down;
                left = Keys.Left;
                right = Keys.Right;
                fire = Keys.Enter;
            }
            else
            {
                angle = -180;
                setSpriteCoords(frame(1), 1);
                setCoords(640-64, 240);
                accelerate = Keys.W;
                reverse = Keys.S;
                left = Keys.A;
                right = Keys.D;
                fire = Keys.Space;
            }
            hp = 3;
        }

        public void animation()
        {
            imy = frame(currentFrame);
            Console.WriteLine(accel);
            if (accel > 0.2f || accel < -0.2f)
            {

                animationCount += 1;
            }
            else
            {
                animationCount = 0;
            }
            if (animationCount >= 8-accel)
            {
                currentFrame += 1;
            }
            if (currentFrame >= 6)
            {
                currentFrame = 0;
            }
        }
        
        public void movment()
        {
            AngleMath();
            speed = accel;
            x += veclocity_x;
            y += veclocity_y;

            if (accel >= 0)
            {
                accel -= 0.05f;
            }
            if (accel <= 0)
            {
                accel += 0.05f;
            }

        }

        public void update(List<bullet> bullets, List<block> blocks, tank ta)
        {
            if (hp <= 0)
                hp = 0;
            switch (hp)
            {
                case 3:
                    color = new Color(255, 255, 255);
                    break;
                case 2:
                    color = new Color(255, 200, 200);
                    break;
                case 1:
                    color = new Color(255, 100, 100);
                    break;
            }
            if (hp <= 0)
            {
                color = new Color(255, 0, 0);
            }
            hitbox = new Rectangle((int)x - 16+5, (int)y - 16+6, 20, 20);
            if (hitbox.Intersects(ta.hitbox) && controllSch != ta.controllSch)
            {
                accel = 0;
                x -= veclocity_x;
                y -= veclocity_y;
            }
            foreach (bullet b in bullets)
            {
                if (b.hitboxs.Intersects(hitbox) && b.onTank != controllSch)
                {
                    hp -= 1;
                    b.destroy = true;
                }
            }
            foreach (block bl in blocks)
            {
                if (hitbox.Intersects(bl.hitbox))
                {
                    if (bl.type == 1 || bl.type == 3)
                    {
                        accel = 0;
                        x -= veclocity_x;
                        y -= veclocity_y;
                    }
                }
            }
        }

        public void input(List<bullet> bullets, SoundEffect shootsfx)
        {
            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();
            if (inputActive)
            {
                if (angle >= 360 || angle <= -360)
                {
                    angle = 0;
                }
                if (keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire))
                {
                    bullets.Add(new bullet(angle, x-2, y-2, controllSch));
                    shootsfx.Play();
                }
                if (keyboard.IsKeyDown(accelerate))
                {
                    if(accel <= maxAccel)
                        accel += 0.1f;
                }
                if (keyboard.IsKeyDown(reverse))
                {
                    if (accel >= minAccel)
                        accel -= 0.1f;
                }
                if (keyboard.IsKeyDown(left))
                {
                    angle -= 2;
                }
                if (keyboard.IsKeyDown(right))
                {
                    angle += 2;
                }
            }
        }
    }
}
