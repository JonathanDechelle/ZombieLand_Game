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
    class FireBall
    {
        Vector2 Position;
        Animation FBAnimation;
        AnimationPlayer FBPLayer;
        SpriteEffects flip;
        bool Invisible;
        Rectangle RecFB;

        public Vector2 position
        {
            get { return Position; }
        }

        public bool invisible
        {
            set { Invisible = value; }
        }

        public Rectangle recFB
        {
            get { return RecFB; }
        }

        public FireBall(Vector2 Position,SpriteEffects flip)
        {
            this.Position = Position;
            this.Position.Y -= 10;
            FBAnimation = new Animation(Ressources.FireBall, 60, 0.2f, 1, true);
            FBPLayer = new AnimationPlayer();
            FBPLayer.PlayAnimation(FBAnimation);
            this.flip = flip;

            if (flip == SpriteEffects.None) this.Position.X += 50;
            else this.Position.X -= 50;
        }

        public void Update()
        {
            RecFB = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);

            //Deplacement
            if (flip == SpriteEffects.None) Position.X += 3.5f; else Position.X -= 3.5f;
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
            if(!Invisible)
            FBPLayer.Draw(gametime, g, Position, flip);
        }
    }
}
