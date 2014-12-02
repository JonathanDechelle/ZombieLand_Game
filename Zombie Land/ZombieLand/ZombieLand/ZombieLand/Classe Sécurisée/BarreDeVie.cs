using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ZombieLand
{
    class BarreDeVie
    {
        
        Vector2 Position,PosBackSpace;
        Rectangle RecVisible;
        float VieReele;
        bool Invisible,Tuer,NoHuman;
        float CompteurNoTouch;

        public bool tuer { get { return Tuer; } }

        public float vieReele { get { return VieReele; } set { VieReele = value; } }

        public bool invisible { get { return Invisible; } set { Invisible = value; } }

        public BarreDeVie(Vector2 Position,float VieReele)
        {
            this.Position = Position;
            this.VieReele = VieReele;
            RecVisible = new Rectangle((int)Position.X, (int)Position.Y, 100,10);
        }

        public void Update(ZLPlayer Joueur)
        {
            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Joueur.hurt)
            { if (Joueur.hurtLevel == 1)VieReele -= 0.10f; else if (Joueur.hurtLevel == 2)VieReele -= 0.15f; else if (Joueur.hurtLevel == 3)VieReele -= 0.20f; CompteurNoTouch = 0; Invisible = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) Invisible = true; } }

            //Position 
            Position = new Vector2(Joueur.position.X - RecVisible.Width/2 , Joueur.position.Y - 8*(RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele; 
            
            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width == 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Update(BossZombie Zombie)
        {
            NoHuman = true;

            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Zombie.hurt)
            { VieReele -= 0.35f; CompteurNoTouch = 0; Invisible = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) { Invisible = true; CompteurNoTouch = 0; } } }

            //Position 
            Position = new Vector2(Zombie.position.X - RecVisible.Width / 2, Zombie.position.Y-40 - 8 * (RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele;

            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width <= 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Update(BossZombieCR Zombie)
        {
            NoHuman = true;

            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Zombie.hurt)
            { VieReele -= 0.35f; CompteurNoTouch = 0; Invisible = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) { Invisible = true; CompteurNoTouch = 0; } } }

            //Position 
            Position = new Vector2(Zombie.position.X - RecVisible.Width / 2, Zombie.position.Y - 40 - 8 * (RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele;

            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width <= 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Update(BossZombieF Zombie)
        {
            NoHuman = true;

            ///Pour que la vie descendre beaucoup moins vite et pour donner un peu plus de facilité VieReele est gérée
            if (Zombie.hurt)
            { VieReele -= 0.35f; CompteurNoTouch = 0; Invisible = false; }

            //Compteur pour faire disparaitre Barre de vie si non touché (ne fait pas le traitement si deja invisible)
            else { if (!Invisible) { CompteurNoTouch++; if (CompteurNoTouch == 120) { Invisible = true; CompteurNoTouch = 0; } } }

            //Position 
            Position = new Vector2(Zombie.position.X - RecVisible.Width / 2, Zombie.position.Y - 80 - 8 * (RecVisible.Height));

            //Changement de grosseur du rectangle
            RecVisible.Width = (int)VieReele;

            //Si grosseur=0 C'EST LA MORT
            if (RecVisible.Width <= 0) Tuer = true;

            //Position TexteBackspace
            if (Position.X < 800) PosBackSpace = new Vector2(350, 450);
            else if (Position.X < 1600) PosBackSpace = new Vector2(1200, 20);
        }

        public void Draw(SpriteBatch g)
        {
            if (!Invisible)
            {
                g.Draw(Ressources.BarreVie, Position, RecVisible, Color.White);

                if(!NoHuman)
                g.DrawString(Ressources.TexteItalic, "BACKSPACE POUR REVENIR AU MENU", PosBackSpace, Color.White);
            }
        }
    }
}
