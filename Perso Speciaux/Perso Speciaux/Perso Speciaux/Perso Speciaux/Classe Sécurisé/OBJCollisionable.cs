using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGameLibrairy;

namespace Perso_Speciaux
{
    public class OBJCollisionable
    {
         Rectangle RecObj,RecPerso;

        public Rectangle recObj { get { return RecObj; } }

        public OBJCollisionable(Rectangle NewRec)
        {
            RecObj = new Rectangle((int)NewRec.X, (int)NewRec.Y, NewRec.Width, NewRec.Height);
        }

        //Interaction joueur->Object Collisionnable
        public void Update(Ninja Joueur)
        {
            RecPerso = new Rectangle(Joueur.recPerso.X, Joueur.recPerso.Y, Joueur.recPerso.Width, Joueur.recPerso.Height);

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

        public void Update(Kirby Joueur)
        {
            RecPerso = new Rectangle(Joueur.recPerso.X, Joueur.recPerso.Y, Joueur.recPerso.Width, Joueur.recPerso.Height);

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

    }
}
