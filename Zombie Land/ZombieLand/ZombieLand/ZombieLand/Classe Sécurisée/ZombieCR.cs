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
    class ZombieCR
    {
        AnimationPlayer APZ,APR;
        Animation Walking, Dead,Roue;
        Vector2 Position;
        Rectangle RecZombie, recPerso;
        bool Killed, Attack, Remove, Disable, AttackFinish,
            SoundOneTime;
        Color DeadColor;
        float CompteurDead;
        int nbTouche, Argent;
        //Vitesse;
        SpriteEffects flip;

        public Vector2 position { get { return Position; } set { Position = value; } }

        public bool attack { get { return Attack; } }

        public bool remove { get { return Remove; } }

        public bool disable { set { Disable = value; } }

        public bool attackfinished { get { return AttackFinish; } set { AttackFinish = value; } }

        public Rectangle recZombie { get { return RecZombie; } }

        public int argent { get { return Argent; } }

        public SpriteEffects Flip { get { return flip; } }

        public ZombieCR(Vector2 Position)
        {
            this.Position = Position;
            RecZombie = new Rectangle((int)Position.X + 40, (int)Position.Y, 80, 20);
            APZ = new AnimationPlayer();
            APR = new AnimationPlayer();
            Walking = new Animation(Ressources.ZombieCR, 80, 0.5f, 1, true);
            Roue = new Animation(Ressources.RoueTournante, 30, 0.1f, 1, true);
            Dead = new Animation(Ressources.ZombieCRDead, 80, 0.1f, 1, false);
            DeadColor = Color.White;
            Argent = 10;
        }

        public void Update(ZLPlayer Joueur, List<ObjCollisionable> ObjCollisionnable)
        {
            recPerso = new Rectangle(Joueur.recPerso.X + 40, Joueur.recPerso.Y + 80, Joueur.recPerso.Width, 20);

            if (!Disable && !Killed)
            {
                RecZombie.X = (int)Position.X;
                RecZombie.Y = (int)Position.Y;

                #region deplacement
                //gere les Y a une certaine distance du personnage
                if (Math.Abs(Position.X - Joueur.position.X) < 200)
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.75f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.75f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.75f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else Position.X -= 0.75f;
                }
                #endregion
            }

            //Decalement du Rectangle de collsion en conséquence du sens de l'ennemi
            if (recPerso.X > RecZombie.X) { flip = SpriteEffects.FlipHorizontally; RecZombie.X -= 50; }
            else { flip = SpriteEffects.None; RecZombie.X += 50; }

            #region Gestion Des Objects Collisionnable
            if (ObjCollisionnable != null)
                foreach (ObjCollisionable O in ObjCollisionnable)
                    O.Update(this, Joueur.position);
            #endregion

            #region Animation Selon l'environnement
            if (Killed) { APZ.PlayAnimation(Dead); APR.PlayAnimation(null); }
            else if (recPerso.Intersects(RecZombie))
            {
                   //Si touche a scie
                if (Joueur.apJoueur.Animation == Joueur.scieArming && Joueur.Flip == flip) Killed = true;

                else
                {
                    APZ.PlayAnimation(null); APR.PlayAnimation(null);
                    Attack = true; Joueur.disable = true;
                    if (flip == SpriteEffects.FlipHorizontally) Position.X += 25;
                    else Position.X -= 25;
                }
            }

            else if (!Attack) { APZ.PlayAnimation(Walking); APR.PlayAnimation(Roue); }
            #endregion

            #region Si touche à FireBall
            if (Joueur.fireBalls != null)
            {
                for (int F = 0; F < Joueur.fireBalls.Count; F++)
                {
                    if (Joueur.fireBalls[F].recFB.Intersects(RecZombie)) { nbTouche++; }

                    if (nbTouche == 3) Killed = true;
                }
            }
            #endregion

            #region Si Touche à Ball
            if (Joueur.balles != null)
            {
                for (int B = 0; B < Joueur.balles.Count; B++)
                {
                    if (Joueur.balles[B].recB.Intersects(RecZombie))
                    { nbTouche++; Joueur.balles.RemoveAt(B); }
                    if (nbTouche == 3) Killed = true;
                }
            }
            #endregion

            #region Si Touche à Cartouche
            if (Joueur.cartouches != null)
            {
                for (int C = 0; C < Joueur.cartouches.Count; C++)
                {
                    for (int c = 0; c < Joueur.cartouches[C].eclats.Count(); c++)
                        if (Joueur.cartouches[C].eclats[c].recC.Intersects(RecZombie))
                        { nbTouche++; Joueur.cartouches[C].eclats.RemoveAt(c); }
                    if (nbTouche >= 6) { RecZombie = new Rectangle(-100, -100, 10, 10); ; Killed = true; }
                }
            }
            #endregion

            #region Si Touche à Laser
            if (Joueur != null)
            {
                for (int L = 0; L < Joueur.lasers.Count; L++)
                {
                    if (Joueur.lasers[L].recL.Intersects(RecZombie)) { Killed = true; }
                }
            }
            #endregion

            #region Clignotement Si tuer
            if (Killed)
            {
                CompteurDead++; if (CompteurDead == 200) Remove = true;
                DeadColor.A -= 10;
                DeadColor.B -= 10;
                DeadColor.G -= 10;
                DeadColor.R -= 10;

            }
            #endregion
            
            if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieCRDie.Play(0.1f, 0.5f, 0f); }
        }

        public void Update(Perso_Speciaux.Ninja Joueur, List<ObjCollisionable> ObjCollisionnable)
        {
            recPerso = new Rectangle(Joueur.recPerso.X + 40, Joueur.recPerso.Y + 80, Joueur.recPerso.Width, 20);

            if (!Disable && !Killed)
            {
                RecZombie.X = (int)Position.X;
                RecZombie.Y = (int)Position.Y;

                #region deplacement
                //gere les Y a une certaine distance du personnage
                if (Math.Abs(Position.X - Joueur.position.X) < 200)
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.75f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.75f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.75f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else Position.X -= 0.75f;
                }
                #endregion
            }

            //Decalement du Rectangle de collsion en conséquence du sens de l'ennemi
            if (recPerso.X > RecZombie.X) { flip = SpriteEffects.FlipHorizontally; RecZombie.X -= 50; }
            else { flip = SpriteEffects.None; RecZombie.X += 50; }

            #region Gestion Des Objects Collisionnable
            if (ObjCollisionnable != null)
                foreach (ObjCollisionable O in ObjCollisionnable)
                    O.Update(this, Joueur.position);
            #endregion

            #region Animation Selon l'environnement
            if (Killed) { APZ.PlayAnimation(Dead); APR.PlayAnimation(null); }
            else if (recPerso.Intersects(RecZombie))
            {
                //Si touche a scie
                if (Joueur.attack) Killed = true;

                else
                {
                    Remove = true;
                    Joueur.hurt = true;
                }
                
            }

            else if (!Attack) { APZ.PlayAnimation(Walking); APR.PlayAnimation(Roue); }
            #endregion


            #region Clignotement Si tuer
            if (Killed)
            {
                CompteurDead++; if (CompteurDead == 200) Remove = true;
                DeadColor.A -= 10;
                DeadColor.B -= 10;
                DeadColor.G -= 10;
                DeadColor.R -= 10;

            }
            #endregion

            if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieCRDie.Play(0.1f, 0.5f, 0f); }
        }

        public void Update(Perso_Speciaux.Kirby Joueur, List<ObjCollisionable> ObjCollisionnable)
        {
            recPerso = new Rectangle(Joueur.recPerso.X + 40, Joueur.recPerso.Y + 80, Joueur.recPerso.Width, 20);

            if (!Disable && !Killed)
            {
                RecZombie.X = (int)Position.X;
                RecZombie.Y = (int)Position.Y;

                #region deplacement
                //gere les Y a une certaine distance du personnage
                if (Math.Abs(Position.X - Joueur.position.X) < 200)
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.75f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.75f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.75f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.75f;
                    else Position.X -= 0.75f;
                }
                #endregion
            }

            //Decalement du Rectangle de collsion en conséquence du sens de l'ennemi
            if (recPerso.X > RecZombie.X) { flip = SpriteEffects.FlipHorizontally; RecZombie.X -= 50; }
            else { flip = SpriteEffects.None; RecZombie.X += 50; }

            #region Gestion Des Objects Collisionnable
            if (ObjCollisionnable != null)
                foreach (ObjCollisionable O in ObjCollisionnable)
                    O.Update(this, Joueur.position);
            #endregion

            #region Animation Selon l'environnement
            if (Killed) { APZ.PlayAnimation(Dead); APR.PlayAnimation(null); }
            else if (recPerso.Intersects(RecZombie))
            {
                //Si touche a scie
                if (Joueur.attack ||Joueur.jump) Killed = true;

                else
                {
                    Remove = true;
                    Joueur.hurt = true;
                }

            }

            else if (!Attack) { APZ.PlayAnimation(Walking); APR.PlayAnimation(Roue); }
            #endregion

            #region si touche à l'air
            if (RecZombie.Intersects(Joueur.recAir) && Joueur.tir) Killed = true;
            #endregion 

            #region Clignotement Si tuer
            if (Killed)
            {
                CompteurDead++; if (CompteurDead == 200) Remove = true;
                DeadColor.A -= 10;
                DeadColor.B -= 10;
                DeadColor.G -= 10;
                DeadColor.R -= 10;

            }
            #endregion

            if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieCRDie.Play(0.1f, 0.5f, 0f); }
        }


        public void Draw(GameTime gametime, SpriteBatch g)
        {
            //g.Draw(Ressources.Test, recPerso, Color.Blue);
            //g.Draw(Ressources.Test, RecZombie, Color.Red);
            APZ.Draw(gametime, g, Position, flip, DeadColor);

            ///Roue
            if (flip == SpriteEffects.None)
                APR.Draw(gametime, g, new Vector2(Position.X + 10, Position.Y- 5 ), flip);
            else
                APR.Draw(gametime, g, new Vector2(Position.X - 10, Position.Y- 5), flip);
        }
    }
}
