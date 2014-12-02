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
    class Zombie
    {
        AnimationPlayer APZ;
        Animation Walking,Dead;
        Vector2 Position;
        Rectangle RecZombie,recPerso,recColl;
        bool Killed,Attack,Remove,Disable,AttackFinish,
            SoundOneTime;
        Color DeadColor;
        float CompteurDead;
        int nbTouche,Argent;
        //Vitesse;
        SpriteEffects flip;

        public Vector2 position { get { return Position; } set { Position = value; } }

        public bool attack { get { return Attack; } }

        public bool remove { get { return Remove; } }

        public bool disable { set { Disable = value; } }

        public bool attackfinished { get { return AttackFinish; } set { AttackFinish = value; } }

        public Rectangle recZombie { get { return RecZombie; } }

        public int argent { get { return Argent;} }

        public SpriteEffects Flip { get { return flip; } }


        public Zombie(Vector2 Position)
        {
            this.Position = Position;
            RecZombie = new Rectangle((int)Position.X+40, (int)Position.Y, 80, 20);
            APZ = new AnimationPlayer();
            Walking = new Animation(Ressources.Zombie1, 80, 1f, 1, true);
            Dead = new Animation(Ressources.Zombie1Dead, 80, 0.05f, 1, false);
            DeadColor = Color.White;
            Argent = 5;
        }

        public void Update(ZLPlayer Joueur,List<ObjCollisionable>ObjCollisionnable)
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
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.25f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.25f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.25f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else Position.X -= 0.25f;
                }
                #endregion
            }

            //Decalement du Rectangle de collsion en conséquence du sens de l'ennemi
            if (recPerso.X > RecZombie.X) { flip = SpriteEffects.FlipHorizontally; RecZombie.X -= 50; }
            else { flip = SpriteEffects.None; RecZombie.X += 50; }

            #region Gestion Des Objects Collisionnable
            if(ObjCollisionnable!=null)
            foreach (ObjCollisionable O in ObjCollisionnable)
                 O.Update(this,Joueur.position);
            #endregion

            #region Animation Selon l'environnement
            if (Killed) APZ.PlayAnimation(Dead);
            else if (recPerso.Intersects(RecZombie))
            {
                //Si touche a scie
                if (Joueur.apJoueur.Animation == Joueur.scieArming && Joueur.Flip==flip) Killed = true;

                else
                {
                    APZ.PlayAnimation(null); Attack = true; Joueur.disable = true;
                    if (flip == SpriteEffects.FlipHorizontally) Position.X += 25;
                    else Position.X -= 25;
                }
            }

            else if (!Attack) APZ.PlayAnimation(Walking);
            #endregion

            #region Si touche à FireBall
            if (Joueur.fireBalls != null)
            {
                for (int F = 0; F < Joueur.fireBalls.Count; F++)
                {
                    if (Joueur.fireBalls[F].recFB.Intersects(RecZombie)) { Killed = true; }
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
                    if (nbTouche == 2) Killed = true;
                }
            }
            #endregion

            #region Si Touche à Cartouche
            if (Joueur.cartouches != null)
            {
                for (int C = 0; C < Joueur.cartouches.Count; C++)
                {
                    for (int c = 0; c < Joueur.cartouches[C].eclats.Count();c++)
                        if (Joueur.cartouches[C].eclats[c].recC.Intersects(RecZombie))
                        { nbTouche++; Joueur.cartouches[C].eclats.RemoveAt(c); }
                    if (nbTouche >= 1) { RecZombie = new Rectangle(-100, -100, 10, 10); ; Killed = true; }
                }
            }
            #endregion

            #region Si Touche à Laser
            if (Joueur != null)
            {
                    for (int L = 0; L < Joueur.lasers.Count; L++)
                    {
                        recColl = Joueur.lasers[L].recL;
                        if (Joueur.lasers[L].recL.Intersects(RecZombie)) { Killed = true; }
                    }
            }
            #endregion

            #region Clignotement Si tuer
            if (Killed)
            {
                CompteurDead ++; if (CompteurDead == 200) Remove = true;
                DeadColor.A -= 10;
                DeadColor.B -= 10;
                DeadColor.G -= 10;
                DeadColor.R -= 10;

            }
            #endregion

            if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieDie.Play(0.1f,0.25f,0f); }
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
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.25f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.25f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.25f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else Position.X -= 0.25f;
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
            if (Killed) APZ.PlayAnimation(Dead);
            else if (recPerso.Intersects(RecZombie))
            {
                //Si touche pendant attack
                if (Joueur.attack) Killed = true;

                else
                {
                    Remove = true;
                    Joueur.hurt = true;
                }
            }

            else if (!Attack) APZ.PlayAnimation(Walking);
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

            if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieDie.Play(0.1f, 0.25f, 0f); }
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
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else if (Joueur.position.X < Position.X) Position.X -= 0.25f;

                    if (Joueur.position.Y > Position.Y) Position.Y += 0.25f;
                    else if (Joueur.position.Y < Position.Y) Position.Y -= 0.25f;
                }
                else
                {
                    if (Joueur.position.X > Position.X) Position.X += 0.25f;
                    else Position.X -= 0.25f;
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
            if (Killed) APZ.PlayAnimation(Dead);
            else if (recPerso.Intersects(RecZombie) && !Joueur.respire)
            {
                //Si touche pendant attack
                if (Joueur.attack ||Joueur.jump) Killed = true;

                else
                {
                    if (flip == SpriteEffects.None) Position.X += 30;
                    else Position.X -= 30;

                    if (Joueur.ennemiIn) Joueur.ennemiIn = false;
                     Joueur.hurt = true;
                }
            }

            else if (!Attack) APZ.PlayAnimation(Walking);
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

            #region si touche à l'air 
                if (RecZombie.Intersects(Joueur.recAir) && Joueur.tir) Killed = true;
            #endregion 

            #region Si kirby Respire
                    if (Joueur.respire && (Math.Abs(Joueur.position.X - Position.X) < 100) && !Killed && !Joueur.transform)
                    {
                        if (!Joueur.ennemiIn)
                        {
                            if (Joueur.Flip == flip)
                            {
                                ///en X
                                if (Position.X > Joueur.position.X) Position.X -= 2;
                                else Position.X += 2;

                                //en Y
                                if (Position.Y > Joueur.position.Y) Position.Y -= 2;
                                else Position.Y += 2;
                            }

                            if (RecZombie.Intersects(Joueur.recPerso)) { Remove = true; Joueur.ennemiIn = true; }
                        }
                    }
      #endregion

                if (Killed && !SoundOneTime) { SoundOneTime = true; Ressources.ZombieDie.Play(0.1f, 0.25f, 0f); }
        }

        public void Draw(GameTime gametime,SpriteBatch g)
        {
            //g.Draw(Ressources.Test, recPerso, Color.Blue);
           // g.Draw(Ressources.Test, RecZombie, Color.Red);
           // g.Draw(Ressources.Test, recColl, Color.Blue);
            APZ.Draw(gametime,g,Position,flip,DeadColor);
        }
    }
}
