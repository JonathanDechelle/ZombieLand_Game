using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perso_Speciaux
{
    public class BarreDeVie
    {
        Vector2 Position, PosBackSpace;
        Rectangle RecVisible;
        float VieReele;
        bool Invisible, Tuer;
        float CompteurNoTouch;

        public bool tuer { get { return Tuer; } }

        public float vieReele { get { return VieReele; } set { VieReele = value; } }

        public bool invisible { get { return Invisible; } set { Invisible = value; } }

        public BarreDeVie(Vector2 Position, float VieReele)
        {
            this.Position = Position;
            this.VieReele = VieReele;
            RecVisible = new Rectangle((int)Position.X, (int)Position.Y, 100, 10);
        }

        public void Update(Ninja Joueur)
        {
            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Joueur.hurt)
            { VieReele -= 20f; CompteurNoTouch = 0; Invisible = false; Joueur.hurt = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) Invisible = true; } }

            //Position 
            Position = new Vector2(Joueur.position.X - RecVisible.Width / 2, Joueur.position.Y-10- 8 * (RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele;

            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width == 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Update(Kirby Joueur)
        {
            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Joueur.hurt)
            { VieReele -= 20f; CompteurNoTouch = 0; Invisible = false; Joueur.hurt = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) Invisible = true; } }

            //Position 
            Position = new Vector2(Joueur.position.X - RecVisible.Width / 2, Joueur.position.Y - 10 - 8 * (RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele;

            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width == 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Draw(SpriteBatch g)
        {
            if (!Invisible)
                g.Draw(Ressource.BarreVie, Position, RecVisible, Color.White);

        }
    }
}
