using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZombieLand
{
    class ZLPlayer
    {
        Animation LFSkating, LFArming, LFCharging, GSkating, GCharging, GArming, GShooting,SSkating,SCharging,SShooting,ScieSkating,ScieArming,LSkating,
                  LShooting;
        Animation Zombie1Attack, Zombie1Win, Zombie1PreAttack;
        Animation ZombieCRAttack,ZombieCRWin,ZombieCRPreAttack;
        Animation ZombieFAttack, ZombieFWin, ZombieFPreAttack;
        Animation AnimBeforeAttacked;
        AnimationPlayer APJoueur;
        BarreDeVie Vie;

        Vector2 Position;
        Rectangle RecTuto,RecPerso;
        SpriteEffects flip;

        List<FireBall> FireBalls;
        List<Laser> Lasers;
        List<Ball> Balles;
        List<Cartouche> Cartouches;

        bool Tuto,Hurt,Disable,ChargeurPleinG,ShotgunCharge;
        float CompteurPreAttack, CompteurAttack;
        int NbBalle,NbFleche,NbBalleS,NumArmement,HurtLevel;
        double NbLaser;

        /// <summary>
        ///  GET SET 
        /// </summary>
        /// 
        #region Sécurisé
        public Vector2 position { get { return Position; } set { Position = value; } }

        public List<FireBall> fireBalls { get { return FireBalls; } }

        public List<Ball> balles { get { return Balles; } }

        public List<Cartouche> cartouches { get { return Cartouches; } }

        public Rectangle recPerso {get { return RecPerso; } }

        public bool hurt { get { return Hurt; } set { Hurt = value; } }

        public bool disable { set { Disable = value; } }

        public BarreDeVie vie { get { return Vie; } }

        public int numArmement { get { return NumArmement; } }

        public int nbBalle { get { return NbBalle; } }

        public int nbFleche { get { return NbFleche; } }

        public int nbBalleS { get { return NbBalleS; } }

        public int hurtLevel { get { return HurtLevel; } }

        public AnimationPlayer apJoueur { get { return APJoueur; } }

        public Animation scieArming { get { return ScieArming; } }

        public SpriteEffects Flip { get { return flip; } }

        public List<Laser> lasers { get { return Lasers; } set { Lasers = value; } }

        public double nbLaser { get { return NbLaser; } set { NbLaser = value; } }
        #endregion

        public ZLPlayer(Vector2 Position,bool Tuto,bool BarreVie)
        {
            this.Position = Position;
            if (Tuto) { this.Tuto = Tuto; RecTuto = new Rectangle(90, 160, 270, 250); }
            if (BarreVie) Vie = new BarreDeVie(Position,100);
         }

        public void Load(int ArmeDeDepart)
        {
            //Numero d'arme
            NumArmement = ArmeDeDepart;

            //Animation Joueur
            LFSkating = new Animation(Ressources.LFSkating, 80, 0.4f, 1, true);
            LFArming = new Animation(Ressources.LFArming, 80, 0.4f, 1, true);
            LFCharging = new Animation(Ressources.LFCharging, 80, 0.1f, 1, true);
            GSkating = new Animation(Ressources.GSkating, 80, 0.4f, 1, true);
            GCharging = new Animation(Ressources.GCharging, 80, 0.15f, 1, true);
            GArming = new Animation(Ressources.GArming, 80, 0.4f, 1, true);
            GShooting = new Animation(Ressources.GShooting, 80, 0.1f, 1, true);
            SSkating = new Animation(Ressources.SSkating, 80, 0.4f, 1, true);
            SCharging = new Animation(Ressources.SCharging, 80, 0.1f, 1, true);
            SShooting = new Animation(Ressources.SShooting, 80, 0.1f, 1, true);
            ScieSkating = new Animation(Ressources.ScieSkating, 80, 0.4f, 1, true);
            ScieArming = new Animation(Ressources.ScieArming, 80, 0.01f,1, true);
            LSkating = new Animation(Ressources.LSkating, 80, 0.4f, 1, true);
            LShooting = new Animation(Ressources.LAttack, 80, 0.1f, 1, true);
            
            //Animation Zombie
            Zombie1Attack = new Animation(Ressources.Zombie1Attack, 100, 0.4f, 1, true);
            Zombie1Win = new Animation(Ressources.Zombie1Win, 150, 0.15f, 1, false);
            Zombie1PreAttack = new Animation(Ressources.Zombie1PreAttack, 80, 1f, 1, true);
            ZombieCRAttack = new Animation(Ressources.ZombieCRAttack, 80, 0.4f, 1, true);
            ZombieCRWin = new Animation(Ressources.ZombieCRWin, 100, 0.20f, 1, false);
            ZombieCRPreAttack = new Animation(Ressources.ZombieCRPreAttack, 80, 0.3f, 1, true);
            ZombieFAttack = new Animation(Ressources.ZombieFAttack, 80, 0.4f, 1, true);
            ZombieFWin = new Animation(Ressources.ZombieFWin, 150, 0.20f, 1, false);
            ZombieFPreAttack = new Animation(Ressources.ZombieFPreAttack, 80, 0.3f, 1, true);


            APJoueur = new AnimationPlayer();

            //Regarde si avait chargée avant de changer d'arme
            if (ArmeDeDepart == 0) if (NbBalle > 0) APJoueur.PlayAnimation(GCharging); else APJoueur.PlayAnimation(GSkating);
            else if (ArmeDeDepart == 1) if (NbFleche > 0) APJoueur.PlayAnimation(LFCharging); else APJoueur.PlayAnimation(LFSkating);
            else if (ArmeDeDepart == 2) { if (NbBalleS > 0) { ShotgunCharge = true; APJoueur.PlayAnimation(SSkating); } else APJoueur.PlayAnimation(SSkating); }
            else if (ArmeDeDepart == 3) APJoueur.PlayAnimation(ScieSkating);
            else if (ArmeDeDepart == 4) APJoueur.PlayAnimation(LSkating);

            //Animation de départ
            AnimBeforeAttacked = APJoueur.Animation;

            FireBalls = new List<FireBall>();
            Balles = new List<Ball>();
            Cartouches = new List<Cartouche>();
            Lasers = new List<Laser>();
        }

        public void Update(GameTime gametime,List<Zombie>Zombies,List<ZombieCR>ZombiesCR,List<ZombieF>ZombiesF,
                            List<BossZombie>BossZombies,List<BossZombieCR>BossZombiesCR,List<BossZombieF>BossZombiesF,
                            List<ObjCollisionable>Obj,HUD Hud)
        {
            RecPerso = new Rectangle((int)Position.X - 40, (int)Position.Y - 80, 80, 80);

            //Gestion de la vie
            if (Vie != null) Vie.Update(this);

            if (!Disable)
            {
                #region Controle
                if (KeyboardHelper.KeyHold(Keys.Up) || KeyboardHelper.KeyHold(Keys.W)) { Position.Y -= 2; RecPerso.Y -= 4; }
                if (KeyboardHelper.KeyHold(Keys.Down) || KeyboardHelper.KeyHold(Keys.S)) { Position.Y += 2; RecPerso.Y += 4; }
                if (KeyboardHelper.KeyHold(Keys.Right) || KeyboardHelper.KeyHold(Keys.D)) { Position.X += 2; RecPerso.X += 4; flip = SpriteEffects.None; }
                if (KeyboardHelper.KeyHold(Keys.Left) || KeyboardHelper.KeyHold(Keys.A)) { Position.X -= 2; RecPerso.X -= 4; flip = SpriteEffects.FlipHorizontally; }
                #endregion
            }

            //Collision avec les obstacles
             if (Obj != null) foreach (ObjCollisionable O in Obj) O.Update(this);
             
            #region Animation Selon les Touches

             //Chargement du laser automatique
             if (Hud != null) { if (Hud.laser > 0 && NbLaser < Hud.laser) { NbLaser++; } }

             //Chargement de l'arme
             #region Chargement
             if (KeyboardHelper.KeyPressed(Keys.R) && !Hurt)
             {

                 if (APJoueur.Animation == LFSkating) //Une seule charge a la fois
                 {
                     if (Hud != null) { if (Hud.fleche > 0) { APJoueur.PlayAnimation(LFCharging); NbFleche++; Hud.fleche--; } }
                     else { APJoueur.PlayAnimation(LFCharging); NbFleche++; }
                 }

                 else if (APJoueur.Animation == GSkating || APJoueur.Animation == GArming) //Droit a Plusieurs Charge
                 {
                     #region GUN
                     if (Hud != null)
                     {
                         if (NbBalle == 0) ChargeurPleinG = false;

                         if (Hud.balle > 0 && !ChargeurPleinG)
                         {
                             if (GestionExterne.nivGun == 1)
                             {
                                 if (Hud.balle < 5)
                                 {
                                     NbBalle += Hud.balle; Hud.balle -= Hud.balle;
                                 }
                                 else
                                 {
                                     Hud.balle -= 5; NbBalle += 5;
                                 }
                             }
                             else if (GestionExterne.nivGun == 2)
                             {
                                 if (Hud.balle < 10)
                                 {
                                     NbBalle += Hud.balle; Hud.balle -= Hud.balle;
                                 }
                                 else
                                 {
                                     Hud.balle -= 10; NbBalle += 10;
                                 }
                             }

                             else if (GestionExterne.nivGun == 3)
                             {
                                 if (Hud.balle < 15)
                                 {
                                     NbBalle += Hud.balle; Hud.balle -= Hud.balle;
                                 }
                                 else
                                 {
                                     Hud.balle -= 15; NbBalle += 15;
                                 }
                             }
                             APJoueur.PlayAnimation(GCharging); ChargeurPleinG = true;
                         }
                     }
                     else { APJoueur.PlayAnimation(GCharging); NbBalle++; }

                     #endregion
                 }

                 else if (APJoueur.Animation == SSkating || APJoueur.Animation == SCharging) //Droit a Plusieurs Charge
                 {
                     #region Shotgun
                     if (Hud != null)
                     {
                         if (GestionExterne.nivShotgun == 1)
                         {
                             if (Hud.balleShotgun > 0 && NbBalleS < 8)
                             {
                                 Hud.balleShotgun--; APJoueur.PlayAnimation(SCharging); NbBalleS++;
                                 ShotgunCharge = true;
                             }
                         }
                         else if (GestionExterne.nivShotgun == 2)
                         {
                             if (Hud.balleShotgun > 0 && NbBalleS < 12)
                             {
                                 Hud.balleShotgun--; APJoueur.PlayAnimation(SCharging); NbBalleS++;
                                 ShotgunCharge = true;
                             }
                         }
                     }
                     else { APJoueur.PlayAnimation(SCharging); NbBalleS++; ShotgunCharge = true; }
                     #endregion
                 }
             }
             #endregion

             //Tir de l'arme
             #region Tir
             else if (KeyboardHelper.KeyPressed(Keys.Space))
             {
                 if (APJoueur.Animation == LFArming)
                 { NbFleche--; FireBalls.Add(new FireBall(Position, flip)); APJoueur.PlayAnimation(LFSkating); }

                 else if (APJoueur.Animation == GArming)
                 { NbBalle--; Balles.Add(new Ball(Position, flip)); Ressources.FireGun.Play(0.2f, 1f, 0f); APJoueur.PlayAnimation(GShooting); }

                 else if (APJoueur.Animation == SSkating && ShotgunCharge)
                 {
                     NbBalleS--; if (NbBalleS == 0) ShotgunCharge = false;
                     Cartouches.Add(new Cartouche(Position, flip));
                     Ressources.FireGun.Play(0.2f, 0, 0f); APJoueur.PlayAnimation(SShooting);
                 }
             }
             #endregion

             //Maintiens du tir(Pour la scie et Laser)
             #region Scie et Laser
             if (KeyboardHelper.KeyHold(Keys.Space))
             {
                 //Si non blessé
                 if (!Hurt)
                 {
                     if (APJoueur.Animation == ScieSkating) APJoueur.PlayAnimation(ScieArming);
                     else if (APJoueur.Animation == LSkating)
                     {
                         if (Hud != null)
                         {
                             if (Hud.laser > 0)
                             {
                                 if (Lasers.Count == 0) Lasers.Add(new Laser(Position, flip)); APJoueur.PlayAnimation(LShooting); NbLaser -= 0.1f; Hud.laser -= 0.1f;
                             }
                             else Hud.laser = 0;
                         }
                         else
                         {
                             if (Lasers.Count == 0) Lasers.Add(new Laser(Position, flip));
                             APJoueur.PlayAnimation(LShooting);
                         }
                     }
                     else if (APJoueur.Animation == LShooting)
                     {
                         if (Hud != null)
                         {
                             if (Hud.laser >= 0)
                             {
                                 NbLaser -= 0.1f; Hud.laser -= 0.1f;
                             }
                             else Hud.laser = 0;
                         }
                     }
                 }
                 //Si Blessé
                 else
                 {
                     if (Lasers.Count == 1) Lasers.RemoveAt(0);
                 }
             }
             else if (!Hurt)
             {
                 if (AnimBeforeAttacked == ScieSkating) APJoueur.PlayAnimation(ScieSkating);
                 else if (AnimBeforeAttacked == LSkating) { APJoueur.PlayAnimation(LSkating); if (Lasers.Count == 1)Lasers.RemoveAt(0);}
             }
             else
             {
                 if (Lasers.Count == 1) Lasers.RemoveAt(0);
             }
             #endregion

             //Animation Lorsque blesse par un zombie ET tue le zombie
             #region Blessé
             if (Hurt)
            {
                if (KeyboardHelper.KeyPressed(Keys.Space))
                {
                    if (APJoueur.Animation == Zombie1Attack) APJoueur.PlayAnimation(Zombie1Win);
                    else if (APJoueur.Animation == ZombieCRAttack) APJoueur.PlayAnimation(ZombieCRWin);
                    else if (APJoueur.Animation == ZombieFAttack) APJoueur.PlayAnimation(ZombieFWin);

                    Hurt = false;
                 }
            }
             #endregion

             //Fin Animation Win sur les zombies (retour à animation de depart)
             #region Victoire sur Zombies
             if (APJoueur.Animation == Zombie1Win)
            {
                if (APJoueur.FrameIndex == 1)
                {
                    CompteurAttack += (float)gametime.ElapsedGameTime.TotalMilliseconds;
                    if (CompteurAttack > 150) { Ressources.CoupDePoing.Play(0.05f,1f,0); CompteurAttack = 0; }
                }
             
                else if (APJoueur.FrameIndex == 4)
                {
                    RemiseNormal();
                }
            }

             else if (APJoueur.Animation == ZombieCRWin)
             {
                 if (APJoueur.FrameIndex == 5)
                     RemiseNormal();
             }

             else if (APJoueur.Animation == ZombieFWin)
             {
                 if (APJoueur.FrameIndex == 5)
                     RemiseNormal();
             }
             #endregion 

             //Animation des Armes Armées
             #region Arme Chargée
             if (APJoueur.Animation == LFCharging && APJoueur.FrameIndex == 7)
                APJoueur.PlayAnimation(LFArming);

            else if (APJoueur.Animation == GCharging && APJoueur.FrameIndex == 2)
                APJoueur.PlayAnimation(GArming);

            else if (APJoueur.Animation == SCharging && APJoueur.FrameIndex == 2)
                APJoueur.PlayAnimation(SSkating);

             #endregion

             //Arret de l'animation du tir (sur certaines armes uniquement)
             #region Arret Animation tir
             if (APJoueur.Animation == GShooting && APJoueur.FrameIndex == 1)
            { if (NbBalle == 0) { APJoueur.PlayAnimation(GSkating); ChargeurPleinG = false; } else APJoueur.PlayAnimation(GArming); }
            else if (APJoueur.Animation == SShooting && APJoueur.FrameIndex == 7)
                APJoueur.PlayAnimation(SSkating);
             #endregion

            #endregion

            #region Contrainte tutoriel
             //check de contrainte(si dans hors du rectangle)
            if (Tuto)
            {
                if (RecPerso.X + 80 > RecTuto.X + RecTuto.Width) Position.X -= 2;
                if (RecPerso.X < RecTuto.X) Position.X += 2;
                if (RecPerso.Y + 80 > RecTuto.Y + RecTuto.Height) Position.Y -= 2;
                if (RecPerso.Y < RecTuto.Y) Position.Y += 2;
            }
            #endregion

            #region FireBall
            //Update de FireBall 
            for (int F = 0; F < FireBalls.Count; F++)
            {
                FireBalls[F].Update();
                if (FireBalls[F].position.X > 2100 || FireBalls[F].position.X < -200) FireBalls.RemoveAt(F);
            }
            #endregion

            #region Balles
            //Update de Balles 
            for (int B = 0; B < Balles.Count; B++)
            {
                Balles[B].Update();
                if (Balles[B].position.X > 2100 || Balles[B].position.X < -200) Balles.RemoveAt(B);
            }
            #endregion

            #region Cartouche
            for (int C = 0; C < Cartouches.Count; C++)
            {
                Cartouches[C].Update();
                if (Cartouches[C].eclats.Count()==0) Cartouches.RemoveAt(C);
            }
            #endregion

            #region Laser
            for (int L = 0; L < Lasers.Count; L++)
            {
                Lasers[L].Update(this);
            }
            #endregion

            #region Zombies
            //Update Zombie
            if (Zombies != null)
            {
                for (int Z = 0; Z < Zombies.Count; Z++)
                {
                    if (Zombies[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 1;
                        APJoueur.PlayAnimation(Zombie1PreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        Zombies[Z].disable = true;
                        if (CompteurPreAttack > 0.5f)
                        {
                            CompteurPreAttack = 0; Zombies.RemoveAt(Z);
                            APJoueur.PlayAnimation(Zombie1Attack); Disable = false;
                        }
                    }
                }
            }

            if (APJoueur.Animation == Zombie1Attack)
            {
                //CompteurAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                //if (CompteurAttack > 0.9f) { Ressources.ZombieAttack.Play(0.5f, 0f, 0f); CompteurAttack = 0; }
            }
            #endregion

            #region ZombiesCR
            //Update Zombie
            if (ZombiesCR != null)
            {
                for (int Z = 0; Z < ZombiesCR.Count; Z++)
                {
                    if (ZombiesCR[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 2;
                        APJoueur.PlayAnimation(ZombieCRPreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        ZombiesCR[Z].disable = true;
                        if (CompteurPreAttack > 0.6f)
                        {
                            CompteurPreAttack = 0; ZombiesCR.RemoveAt(Z);
                            APJoueur.PlayAnimation(ZombieCRAttack); Disable = false;
                        }
                    }
                }
            }
            #endregion

            #region ZombiesF
            //Update Zombie
            if (ZombiesF != null)
            {
                for (int Z = 0; Z < ZombiesF.Count; Z++)
                {
                    if (ZombiesF[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 3;
                        APJoueur.PlayAnimation(ZombieFPreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        ZombiesF[Z].disable = true;
                        if (CompteurPreAttack > 0.6f)
                        {
                            CompteurPreAttack = 0; ZombiesF.RemoveAt(Z);
                            APJoueur.PlayAnimation(ZombieFAttack); Disable = false;
                        }
                    }
                }
            }
            #endregion

            #region BossZombies
            //Update Zombie
            if (BossZombies != null)
            {
                for (int Z = 0; Z < BossZombies.Count; Z++)
                {
                    if (BossZombies[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 3;
                        APJoueur.PlayAnimation(Zombie1PreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        BossZombies[Z].disable = true;
                        if (CompteurPreAttack > 0.5f)
                        {
                            CompteurPreAttack = 0; BossZombies.RemoveAt(Z);
                            APJoueur.PlayAnimation(Zombie1Attack); Disable = false;
                        }
                    }
                }
            }
            #endregion

            #region BossZombiesCR
            //Update Zombie
            if (BossZombiesCR != null)
            {
                for (int Z = 0; Z < BossZombiesCR.Count; Z++)
                {
                    if (BossZombiesCR[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 3;
                        APJoueur.PlayAnimation(ZombieCRPreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        BossZombiesCR[Z].disable = true;
                        if (CompteurPreAttack > 0.5f)
                        {
                            CompteurPreAttack = 0; BossZombiesCR.RemoveAt(Z);
                            APJoueur.PlayAnimation(ZombieCRAttack); Disable = false;
                        }
                    }
                }
            }
            #endregion

            #region BossZombiesF
            //Update Zombie
            if (BossZombiesF != null)
            {
                for (int Z = 0; Z < BossZombiesF.Count; Z++)
                {
                    if (BossZombiesF[Z].attack)
                    {
                        Hurt = true;
                        HurtLevel = 3;
                        APJoueur.PlayAnimation(ZombieFPreAttack);
                        CompteurPreAttack += (float)gametime.ElapsedGameTime.TotalSeconds;
                        BossZombiesF[Z].disable = true;
                        if (CompteurPreAttack > 0.5f)
                        {
                            CompteurPreAttack = 0; BossZombiesF.RemoveAt(Z);
                            APJoueur.PlayAnimation(ZombieFAttack); Disable = false;
                        }
                    }
                }
            }
            #endregion
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
            APJoueur.Draw(gametime, g, Position, flip);
            if(Vie!=null) Vie.Draw(g);
            foreach (FireBall F in FireBalls) F.Draw(gametime, g);
            foreach (Ball B in Balles) B.Draw(gametime, g);
            foreach (Cartouche C in Cartouches) C.Draw(gametime, g);
            foreach (Laser L in Lasers) L.Draw(gametime, g);
        }

        public void RemiseNormal()
        {
            //Regarde si avait charger avant
            if (NbBalle > 0) { if (AnimBeforeAttacked == GSkating) APJoueur.PlayAnimation(GCharging); else APJoueur.PlayAnimation(AnimBeforeAttacked); }
            else if (nbFleche > 0) { if (AnimBeforeAttacked == LFSkating) APJoueur.PlayAnimation(LFCharging); else APJoueur.PlayAnimation(AnimBeforeAttacked); }
            else if (NbBalleS > 0) { if (AnimBeforeAttacked == SSkating)APJoueur.PlayAnimation(SCharging); else APJoueur.PlayAnimation(AnimBeforeAttacked); }
            else APJoueur.PlayAnimation(AnimBeforeAttacked);
        }
    }
}
