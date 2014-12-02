using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGameLibrairy;

namespace ZombieLand
{
    class ObjCollisionable
    {
        Rectangle RecObj,RecPerso,RecZombie;

        public Rectangle recObj { get { return RecObj; } }

        public ObjCollisionable(Rectangle NewRec)
        {
            RecObj = new Rectangle((int)NewRec.X, (int)NewRec.Y, NewRec.Width, NewRec.Height);
        }

        //Interaction joueur->Object Collisionnable
        public void Update(ZLPlayer Joueur)
        {
            RecPerso = new Rectangle(Joueur.recPerso.X,Joueur.recPerso.Y,Joueur.recPerso.Width,Joueur.recPerso.Height);

            if (RecPerso.isOnBottomOf(RecObj)) Joueur.position = new Vector2(Joueur.position.X, Joueur.position.Y + 2);
            else if (RecPerso.isOnTopOf(RecObj)) Joueur.position = new Vector2(Joueur.position.X, Joueur.position.Y - 2);
            else if (RecPerso.isOnRightOf(RecObj)) Joueur.position = new Vector2(Joueur.position.X + 2, Joueur.position.Y);
            else if (RecPerso.isOnLeftOf(RecObj)) Joueur.position = new Vector2(Joueur.position.X - 2, Joueur.position.Y);

            else if (RecPerso.Intersects(RecObj))
            {
                if (Joueur.position.X > RecObj.X) Joueur.position = new Vector2(Joueur.position.X + 2, Joueur.position.Y);
                else Joueur.position = new Vector2(Joueur.position.X - 2, Joueur.position.Y);
            }
          
        }

        //Interaction Zombie->Object Collisionnable
        public void Update(Zombie Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }

            //if (RecZombie.Intersects(RecObj))
            //{
            //    if (Zombie.position.X > RecObj.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y);
            //    else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y);

            //    if (Zombie.position.Y > RecObj.Y) Zombie.position = new Vector2(Zombie.position.X, Zombie.position.Y + 2);
            //    else Zombie.position = new Vector2(Zombie.position.X, Zombie.position.Y - 2);
            //}

        }

        //Interaction Zombie->Object Collisionnable
        public void Update(ZombieCR Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }

            //if (RecZombie.Intersects(RecObj))
            //{
            //    if (Zombie.position.X > RecObj.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y);
            //    else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y);

            //    if (Zombie.position.Y > RecObj.Y) Zombie.position = new Vector2(Zombie.position.X, Zombie.position.Y + 2);
            //    else Zombie.position = new Vector2(Zombie.position.X, Zombie.position.Y - 2);
            //}

        }

        //Interaction ZombieF->Object Collisionnable
        public void Update(ZombieF Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }

        }

        //Interaction Zombie->Object Collisionnable
        public void Update(BossZombie Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }
        }

        //Interaction Zombie->Object Collisionnable
        public void Update(BossZombieCR Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }
        }

        //Interaction Zombie->Object Collisionnable
        public void Update(BossZombieF Zombie, Vector2 JoueurPosition)
        {
            if (Zombie.Flip == SpriteEffects.None) RecZombie = new Rectangle(Zombie.recZombie.X - 80, Zombie.recZombie.Y - 80, 80, 80);
            else RecZombie = new Rectangle(Zombie.recZombie.X, Zombie.recZombie.Y - 80, 80, 80);

            if (RecZombie.isOnBottomOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 2);
            }
            else if (RecZombie.isOnTopOf(RecObj))
            {
                if (Zombie.position.X < JoueurPosition.X) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 2);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 2);
            }
            else if (RecZombie.isOnRightOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X + 2, Zombie.position.Y - 4);
            }

            else if (RecZombie.isOnLeftOf(RecObj))
            {
                if (Zombie.position.Y < JoueurPosition.Y + RecObj.Height) Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y + 4);
                else Zombie.position = new Vector2(Zombie.position.X - 2, Zombie.position.Y - 4);
            }
        }

        public void Draw(SpriteBatch g)
        {
           g.Draw(Ressources.Test, RecObj, Color.Red);
           g.Draw(Ressources.Test, RecZombie, Color.Blue);
        }
    }
}
