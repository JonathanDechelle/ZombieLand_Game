using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyGameLibrairy;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieLand
{
    class Laser
    {
        Vector2 Position;
        Animation LAnimation;
        AnimationPlayer LPLayer;
        SpriteEffects flip;
        bool Invisible;
        Rectangle RecL;

        public Vector2 position
        {
            get { return Position; }
        }

        public bool invisible
        {
            set { Invisible = value; }
        }

        public Rectangle recL
        {
            get { return RecL; }
        }

            public Laser(Vector2 Position,SpriteEffects flip)
        {
            this.Position = Position;
            this.Position.Y += 3;
            LAnimation = new Animation(Ressources.Laser, 350, 0.2f, 1, true);
            LPLayer = new AnimationPlayer();
            LPLayer.PlayAnimation(LAnimation);
            this.flip = flip;

            if (flip == SpriteEffects.None) this.Position.X += 172;
            else this.Position.X -= 172;
        }

        public void Update(ZLPlayer Joueur)
        {
            RecL = new Rectangle((int)Position.X -100, (int)Position.Y, 435, 50);

            //Deplacement par rapport au perso
            flip = Joueur.Flip;
            Position = Joueur.position;
            Position.Y += 3;
            if (flip == SpriteEffects.None) this.Position.X += 172;
            else this.Position.X -= 172;
            
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
            if(!Invisible)
            LPLayer.Draw(gametime, g, Position, flip);
        }
    }
}
