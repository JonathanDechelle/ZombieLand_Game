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
    class Cartouche
    {
       
        bool Invisible;
        List<Eclat> Eclats;
       
        public bool invisible
        {
            set { Invisible = value; }
        }

        public List<Eclat> eclats { get { return Eclats; } set { Eclats = value; } }

        public Cartouche(Vector2 Position, SpriteEffects flip)
        {
            Eclats = new List<Eclat>();

            for (int X = 0; X < 5; X++)
            {
                Eclats.Add(new Eclat(Position, flip));
            }
        }

        public void Update()
        {
            //Deplacement
            for (int X = 0; X < Eclats.Count(); X++)
            {
                Eclats[X].Update();

                switch (X)
                {
                    case 0: Eclats[X].position = new Vector2(Eclats[X].position.X, Eclats[X].position.Y- 0.75f); break;
                    case 1: Eclats[X].position = new Vector2(Eclats[X].position.X, Eclats[X].position.Y - 1.5f); break;
                    case 3: Eclats[X].position = new Vector2(Eclats[X].position.X, Eclats[X].position.Y + 0.75f); break;
                    case 4: Eclats[X].position = new Vector2(Eclats[X].position.X, Eclats[X].position.Y + 1.5f); break;
                }

                if (Eclats[X].Flip == SpriteEffects.None) { if (Eclats[X].position.X > Eclats[X].eclatXMax) Eclats.RemoveAt(X); }
                else if (Eclats[X].Flip == SpriteEffects.FlipHorizontally) if (Eclats[X].position.X < Eclats[X].eclatXMax) Eclats.RemoveAt(X);
            }
            
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
           // if (!Invisible)
            for (int X = 0; X < Eclats.Count(); X++)
            {
                Eclats[X].Draw(g);
            }

           // g.Draw(Ressources.Test, RecB, Color.Red);
        }
    }

    class Eclat
    {
        Vector2 Position;
        Rectangle RecC;
        SpriteEffects flip;
        float EclatXMax;

        public float eclatXMax { get { return EclatXMax; } }

        public Vector2 position
        {
            get { return Position; }
            set { Position = value; } 
        }

        public SpriteEffects Flip { get { return flip; } }

        public Rectangle recC
        {
            get { return RecC; }
        }

         public Eclat(Vector2 Position, SpriteEffects flip)
        {
            Position.Y -= 50;
            this.Position = Position;
            this.flip = flip;

            if (flip == SpriteEffects.None) { this.Position.X += 30; EclatXMax = Position.X += 150; }
            else { this.Position.X -= 30; EclatXMax = Position.X -= 150; }
        }

         public void Update()
         {
             //Deplacement
             if (flip == SpriteEffects.None)
             {
                 Position.X += 3f; RecC = new Rectangle((int)Position.X + 60, (int)Position.Y + 50, 15, 15);
             }
             else
             {
                 Position.X -= 3f; RecC = new Rectangle((int)Position.X, (int)Position.Y + 50, 15, 15);
             }
         }

         public void Draw(SpriteBatch g)
         {
             g.Draw(Ressources.Eclat, Position, Color.White);
         }

    }

}
