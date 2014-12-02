using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGameLibrairy;
using Microsoft.Xna.Framework;

namespace ZombieLand
{
    class Ball
    {
        Vector2 Position;
        SpriteEffects flip;
        bool Invisible;
        Rectangle RecB;

        public Vector2 position
        {
            get { return Position; }
        }

        public bool invisible
        {
            set { Invisible = value; }
        }

        public Rectangle recB
        {
            get { return RecB; }
        }

        public Ball(Vector2 Position, SpriteEffects flip)
        {
            Position.Y -= 50;
            this.Position = Position;
            this.flip = flip;

            if (flip == SpriteEffects.None) this.Position.X += 30;
            else this.Position.X -= 30;
        }

        public void Update()
        {
            //Deplacement
            if (flip == SpriteEffects.None)
            {
                Position.X += 3f; RecB = new Rectangle((int)Position.X + 60, (int)Position.Y + 50, 15, 15);
            }
            else
            {
                Position.X -= 3f; RecB = new Rectangle((int)Position.X,(int)Position.Y + 50, 15, 15);
            }
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
            if (!Invisible)
                g.Draw(Ressources.Ball, Position, Color.White);

           // g.Draw(Ressources.Test, RecB, Color.Red);
        }
    }
}

