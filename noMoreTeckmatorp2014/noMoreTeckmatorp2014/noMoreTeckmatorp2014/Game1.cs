using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace noMoreTeckmatorp2014
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;
        }

        int restartTimer;

        int score;
        int score2;

        tank tank;
        tank tank2;

        string winText = "";

        bool fogOfWar;

        List<bullet> bullets = new List<bullet>();
        List<block> blocks = new List<block>();

        KeyboardState keyboard;

        int[,] map = new int[480 / 16, 640 / 16] {
//{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
//{1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1},
//{1,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3,3,3,3,2,3,1},
//{1,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,3,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,2,2,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3,3,3,3,3,3,3,3,1},
//{1,3,3,3,3,3,3,3,3,3,3,3,2,3,3,3,3,2,2,2,3,3,3,3,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,1},
//{1,0,0,0,0,0,0,0,0,0,0,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,3,3,3,3,3,3,3,3,3,3,3,1},
//{1,3,3,3,3,3,3,3,0,0,0,3,2,3,3,3,3,2,2,2,3,3,3,3,2,2,3,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,3,3,3,3,3,3,3,2,2,3,3,3,3,3,3,3,0,3,3,3,3,3,3,3,3,2,2,3,1},
//{1,3,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,2,2,2,3,0,0,0,0,0,0,0,0,2,0,1,1,1,1,1,0,2,0,0,0,3,2,2,2,2,2,2,2,2,2,3,1},
//{1,3,2,2,3,3,3,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,3,2,2,2,2,2,3,3,2,2,3,1},
//{1,3,2,2,3,2,2,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,3,2,2,2,2,2,3,2,2,2,3,1},
//{1,3,2,2,3,2,2,3,0,0,0,0,0,0,0,0,2,0,0,0,3,0,0,0,2,0,0,0,3,2,2,2,2,2,3,2,2,2,3,1},
//{1,3,2,2,3,2,2,3,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,3,2,2,2,2,2,3,2,2,2,3,1},
//{1,3,2,2,3,3,2,3,3,3,3,3,3,3,0,0,2,0,0,0,0,0,0,0,2,0,0,0,3,3,3,2,2,3,3,3,3,3,3,1},
//{1,3,2,2,2,2,2,3,2,2,2,2,2,3,0,0,2,0,1,1,1,1,1,0,2,0,0,0,0,1,2,2,2,2,1,0,0,0,0,1},
//{1,3,2,2,2,2,2,3,2,2,2,2,2,3,0,0,2,0,0,0,0,0,0,0,2,0,0,0,0,1,2,2,2,2,1,0,0,0,0,1},
//{1,3,2,2,2,2,2,2,2,2,2,2,2,3,0,0,2,2,2,2,2,2,2,2,2,0,0,0,0,1,1,1,1,1,1,0,0,0,0,1},
//{1,3,2,2,2,2,2,2,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//{1,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        protected override void Initialize()
        {
            blocks.Clear();
            tank = new tank(1);
            tank2 = new tank(2);

            winText = "";

            bullets.Clear();

            for (int x = 0; x < 480 / 16; x++)
            {
                for (int y = 0; y < 640 / 16; y++)
                {
                    if(map[x,y] == 1)
                        blocks.Add(new block(y * 16, x * 16, 1));
                    if (map[x, y] == 0)
                        blocks.Add(new block(y * 16, x * 16, 2));
                    if (map[x, y] == 3)
                        blocks.Add(new block(y * 16, x * 16, 3));
                    if (map[x, y] == 4)
                        blocks.Add(new block(y * 16, x * 16, 4)); 
                }
            }
            bullets.Add(new bullet(0, 9000, 9000, 1));
            base.Initialize();
        }

        Texture2D spritesheet;
        SpriteFont font;
        SoundEffect shootsfx;
        SoundEffect explosionsfx;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            font = Content.Load<SpriteFont>("font");
            shootsfx = Content.Load<SoundEffect>("shoot_sfx");
            explosionsfx = Content.Load<SoundEffect>("explosion_sfx");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.F1) && prevKeyboard.IsKeyUp(Keys.F1))
            {
                graphics.ToggleFullScreen();
            }
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (keyboard.IsKeyDown(Keys.R) && winText != "")
            {
                Initialize();
            }

            tank.update(bullets, blocks, tank2);
            tank.movment();
            tank.animation();
            tank.input(bullets, shootsfx);

            tank2.update(bullets, blocks, tank);
            tank2.movment();
            tank2.animation();
            tank2.input(bullets, shootsfx);

            if (tank2.DistanceTo(tank.x, tank.y) <= 64)
            {
                fogOfWar = true;
            }
            else
            {
                fogOfWar = false;
            }

            if (tank.hp <= 0)
            {
                winText = "Player 2 Wins! :^))";
                tank2.inputActive = false;
                tank.inputActive = false;
                restartTimer += 1;
            }

            if (tank2.hp <= 0)
            {
                winText = "Player 1 Wins! :^))";
                tank2.inputActive = false;
                tank.inputActive = false;
                restartTimer += 1;
            }

            if (restartTimer >= 1)
            {
                if (restartTimer == 1)
                {
                    if (tank.hp <= 0)
                    {
                        score2 += 1;
                        explosionsfx.Play();
                    }
                    if (tank2.hp <= 0)
                    {
                        score += 1;
                        explosionsfx.Play();
                    }
                }
                restartTimer += 1;
                if (restartTimer >= 128)
                {
                    Initialize();
                    restartTimer = 0;
                }
            }

            foreach (bullet b in bullets)
            {
                b.movment();
                b.update(blocks);
            }
            foreach (block bl in blocks)
            {
                bl.update(bullets);
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].destroy)
                {
                    bullets.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (block bl in blocks)
            {
                if (fogOfWar)
                {
                    foreach (bullet b in bullets)
                    {
                        if (tank.DistanceTo(bl.x, bl.y) <= 64 || tank2.DistanceTo(bl.x, bl.y) <= 64 || b.DistanceTo(bl.x, bl.y) <= 16)
                        {
                            bl.drawSprite(spriteBatch, spritesheet);
                        }
                    }
                }
                if (!fogOfWar)
                {
                    bl.drawSprite(spriteBatch, spritesheet);
                }
            }
            tank.drawSprite(spriteBatch, spritesheet, tank.color);
            tank2.drawSprite(spriteBatch, spritesheet, tank2.color);
            foreach (bullet b in bullets)
            {
                b.drawSprite(spriteBatch, spritesheet);
            }
            spriteBatch.DrawString(font, winText, new Vector2(220, 240 - 7), Color.White);
            spriteBatch.DrawString(font, "Player 1: " + score.ToString(), new Vector2(10, 10), Color.Tomato);
            spriteBatch.DrawString(font, "Player 2: " + score2.ToString(), new Vector2(640 - 150, 10), Color.Tomato);
            for (int i = 0; i < tank.hp; i++)
                spriteBatch.Draw(spritesheet, new Vector2(10, 50 + i * 16), new Rectangle(100, 1, 14, 12), Color.White);
            for (int i = 0; i < tank2.hp; i++)
                spriteBatch.Draw(spritesheet, new Vector2(640-24, 50 + i * 16), new Rectangle(100, 1, 14, 12), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
