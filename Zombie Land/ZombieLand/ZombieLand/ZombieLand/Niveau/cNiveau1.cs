using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Perso_Speciaux;

namespace ZombieLand
{
    class cNiveau1:GameScreen
    {
        ZLPlayer Joueur;
        Ninja Joueur2;
        Kirby Joueur3;
        Camera camera;
        List<ObjCollisionable> Obj;
        List<OBJCollisionable> Obj2;
        List<Zombie> Zombies;
        List<ZombieCR> ZombiesCR;
        List<ZombieF> ZombiesF;
        List<BossZombie> BossZombies;
        List<BossZombieCR> BossZombiesCR;
        List<BossZombieF> BossZombiesF;
        HUD Hud;
        EnnemiVague EV;
        float TimerApparition, TimerApparition2, TimerApparition3, TimerApparition4, TimerApparition5,TimerApparition6;
        Vector2[] ZombiesAppartition;
        Random rApparition;
        MusicPlayer MP;

        public cNiveau1(IServiceProvider serviceProvider, GraphicsDeviceManager graphics)
            : base(serviceProvider, graphics)
        {
            Joueur = new ZLPlayer(new Vector2(100, 200), false, true);
            Joueur2 = new Ninja(new Vector2(100, 200));
            Joueur3 = new Kirby(new Vector2(100, 200));
            rApparition = new Random();
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            Hud = new HUD();
            EV = new EnnemiVague();

            Hud.balle = GestionExterne.nbBalleGun;
            Hud.argent = GestionExterne.argent;
            Hud.fleche = GestionExterne.nbFleche;
            Hud.balleShotgun = GestionExterne.nbBalleShotgun;
            Hud.laser = GestionExterne.nbLaser;

            MP = new MusicPlayer(new Song[]{Ressources.PSong, Ressources.FSong
            ,Ressources.BPSong,Ressources.MGSong, Ressources.RESong});

            MP.Load(GestionExterne.numSong);

            Obj = new List<ObjCollisionable>();
            Obj2 = new List<OBJCollisionable>();

            Zombies = new List<Zombie>();
            ZombiesCR = new List<ZombieCR>();
            ZombiesF = new List<ZombieF>();
            BossZombies = new List<BossZombie>();
            BossZombiesCR = new List<BossZombieCR>();
            BossZombiesF = new List<BossZombieF>();
            ZombiesAppartition = new Vector2[10] { new Vector2(100,50),new Vector2(100,500),new Vector2(800, 50), new Vector2(800, 500), 
                                                  new Vector2(400, 500),new Vector2(400, 50),new Vector2(1200,50),new Vector2(1200,500),
                                                  new Vector2(1800, 50), new Vector2(1800, 500) };
            #region Objects Collisionables
            Obj.Add(new ObjCollisionable(new Rectangle(190, 20, 20, 20)));
            Obj.Add(new ObjCollisionable(new Rectangle(360, 245, 13, 10)));
            Obj.Add(new ObjCollisionable(new Rectangle(360, 330, 1, 20)));
            Obj.Add(new ObjCollisionable(new Rectangle(500, 0, 1, 130)));
            Obj.Add(new ObjCollisionable(new Rectangle(500, 300, 1, 10)));
            Obj.Add(new ObjCollisionable(new Rectangle(500, 430, 1, 10)));
            Obj.Add(new ObjCollisionable(new Rectangle(610, 210, 5, 10)));
            Obj.Add(new ObjCollisionable(new Rectangle(1090, 70, 1, 1)));
            Obj.Add(new ObjCollisionable(new Rectangle(1090, 330, 1, 1)));
            Obj.Add(new ObjCollisionable(new Rectangle(1330, 235, 70, 10)));
            Obj.Add(new ObjCollisionable(new Rectangle(1560, 210, 1, 1)));
            Obj.Add(new ObjCollisionable(new Rectangle(1540, 250, 1, 20)));
            #endregion

            foreach (ObjCollisionable O in Obj) Obj2.Add(new OBJCollisionable(O.recObj));
        }

        public override void Load()
        {
            if(KeyboardHelper.KeyHold(Keys.K)) GestionExterne.numeroPerso = 2;

            if (GestionExterne.numeroPerso == 0) Joueur.Load(0);
            else if (GestionExterne.numeroPerso == 1) Joueur2.Load();
            else Joueur3.Load();
        }

        public override void Update(GameTime gameTime)
        {
            if (GestionExterne.numeroPerso == 0) Joueur.Update(gameTime, Zombies, ZombiesCR, ZombiesF, BossZombies, BossZombiesCR, BossZombiesF, Obj, Hud);
            else if (GestionExterne.numeroPerso == 1) Joueur2.Update(Obj2);
            else Joueur3.Update(Obj2);

            //Song MusicPlayer
            MP.Update(gameTime);
         
            //Apparition des Zombies
            #region Apparition
            if (EV.compteurZombie < EV.NbEnnemieParVagues[EV.numeroVague])
            {
                TimerApparition += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition > EV.vitesseApparition)
                { Zombies.Add(new Zombie(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition = 0; EV.compteurZombie++; }
            }

            if (EV.compteurZombieCR < EV.NbEnnemieCRParVagues[EV.numeroVague])
            {
                TimerApparition2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition2 > EV.vitesseApparition)
                { ZombiesCR.Add(new ZombieCR(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition2 = 0; EV.compteurZombieCR++; }
            }

            if (EV.compteurZombieF < EV.NbEnnemieFParVagues[EV.numeroVague])
            {
                TimerApparition3 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition3 > EV.vitesseApparition)
                { ZombiesF.Add(new ZombieF(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition3 = 0; EV.compteurZombieF++; }
            }

            if (EV.compteurBossZombie< EV.NbEnnemieBossZParVagues[EV.numeroVague])
            {
                TimerApparition4 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition4 > EV.vitesseApparition)
                { BossZombies.Add(new BossZombie(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition4 = 0; EV.compteurBossZombie++; }
            }

            if (EV.compteurBossZombieCR < EV.NbEnnemieBossCRParVagues[EV.numeroVague])
            {
                TimerApparition5 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition5 > EV.vitesseApparition)
                { BossZombiesCR.Add(new BossZombieCR(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition5 = 0; EV.compteurBossZombieCR++; }
            }
            if (EV.compteurBossZombieF < EV.NbEnnemieBossFParVagues[EV.numeroVague])
            {
                TimerApparition6 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (TimerApparition6 > EV.vitesseApparition)
                { BossZombiesF.Add(new BossZombieF(ZombiesAppartition[rApparition.Next(ZombiesAppartition.Length)])); TimerApparition6 = 0; EV.compteurBossZombieF++; }
            }
            #endregion

            //Update des Zombies
            #region Zombie Update
            for (int Z = 0; Z < Zombies.Count; Z++)
            {
                if (GestionExterne.numeroPerso == 0) Zombies[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) Zombies[Z].Update(Joueur2, Obj);
                else Zombies[Z].Update(Joueur3, Obj);

                if (Zombies[Z].remove) { Hud.argent += Zombies[Z].argent; Zombies.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (Zombies[Z].attack && !Zombies[Z].attackfinished) { EV.nbEnnemiRestant--; Zombies[Z].attackfinished = true; }
            }
            #endregion

            //Update des ZombiesCR
            #region ZombieCR Update
            for (int Z = 0; Z < ZombiesCR.Count; Z++)
            {
                if(GestionExterne.numeroPerso==0)  ZombiesCR[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) ZombiesCR[Z].Update(Joueur2, Obj);
                else ZombiesCR[Z].Update(Joueur3, Obj);

                if (ZombiesCR[Z].remove) { Hud.argent += ZombiesCR[Z].argent; ZombiesCR.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (ZombiesCR[Z].attack && !ZombiesCR[Z].attackfinished) { EV.nbEnnemiRestant--; ZombiesCR[Z].attackfinished = true; }
            }
            #endregion

            //Update des ZombiesCR
            #region ZombieF Update
            for (int Z = 0; Z < ZombiesF.Count; Z++)
            {
                if(GestionExterne.numeroPerso==0) ZombiesF[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) ZombiesF[Z].Update(Joueur2, Obj);
                else ZombiesF[Z].Update(Joueur3, Obj);

                if (ZombiesF[Z].remove) { Hud.argent += ZombiesF[Z].argent; ZombiesF.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (ZombiesF[Z].attack && !ZombiesF[Z].attackfinished) { EV.nbEnnemiRestant--; ZombiesF[Z].attackfinished = true; }
            }
            #endregion

            //Update des BossZombies
            #region BossZombie Update
            for (int Z = 0; Z < BossZombies.Count; Z++)
            {
                if(GestionExterne.numeroPerso==0)  BossZombies[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) BossZombies[Z].Update(Joueur2, Obj);
                else BossZombies[Z].Update(Joueur3, Obj);

                if (BossZombies[Z].remove) { Hud.argent += BossZombies[Z].argent; BossZombies.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (BossZombies[Z].attack && !BossZombies[Z].attackfinished) { EV.nbEnnemiRestant--; BossZombies[Z].attackfinished = true; }
            }
            #endregion

            //Update des BossZombiesCR
            #region BossZombieCR Update
            for (int Z = 0; Z < BossZombiesCR.Count; Z++)
            {
                if(GestionExterne.numeroPerso==0) BossZombiesCR[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) BossZombiesCR[Z].Update(Joueur2, Obj);
                else BossZombiesCR[Z].Update(Joueur3, Obj);

                if (BossZombiesCR[Z].remove) { Hud.argent += BossZombiesCR[Z].argent; BossZombiesCR.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (BossZombiesCR[Z].attack && !BossZombiesCR[Z].attackfinished) { EV.nbEnnemiRestant--; BossZombiesCR[Z].attackfinished = true; }
            }
            #endregion

            //Update des BossZombiesF
            #region BossZombieF Update
            for (int Z = 0; Z < BossZombiesF.Count; Z++)
            {
                if (GestionExterne.numeroPerso == 0) BossZombiesF[Z].Update(Joueur, Obj);
                else if (GestionExterne.numeroPerso == 1) BossZombiesF[Z].Update(Joueur2, Obj);
                else BossZombiesF[Z].Update(Joueur3, Obj);

                if (BossZombiesF[Z].remove) { Hud.argent += BossZombiesF[Z].argent; BossZombiesF.RemoveAt(Z); EV.nbEnnemiRestant--; }
                else if (BossZombiesF[Z].attack && !BossZombiesF[Z].attackfinished) { EV.nbEnnemiRestant--; BossZombiesF[Z].attackfinished = true; }
            }
            #endregion

            //Limite Caméra
            #region Camera
            if (GestionExterne.numeroPerso == 0)
            {
                if (Joueur.position.X < 400) camera.Update(new Vector2(400, 250));
                else if (Joueur.position.X > 1500) camera.Update(new Vector2(1500, 250));
                else camera.Update(new Vector2(Joueur.position.X, 250));
            }
            else if (GestionExterne.numeroPerso == 1)
            {
                if (Joueur2.position.X < 400) camera.Update(new Vector2(400, 250));
                else if (Joueur2.position.X > 1500) camera.Update(new Vector2(1500, 250));
                else camera.Update(new Vector2(Joueur2.position.X, 250));
            }
            else
            {
                if (Joueur3.position.X < 400) camera.Update(new Vector2(400, 250));
                else if (Joueur3.position.X > 1500) camera.Update(new Vector2(1500, 250));
                else camera.Update(new Vector2(Joueur3.position.X, 250));
            }
            #endregion

            //update du HUD 
            if(GestionExterne.numeroPerso==0) Hud.Update(new Vector2(camera.X, camera.Y), Joueur);
            else if(GestionExterne.numeroPerso==1) Hud.Update(new Vector2(camera.X, camera.Y), Joueur2);
            else Hud.Update(new Vector2(camera.X, camera.Y), Joueur3);

            GestionExterne.argent = Hud.argent;
            GestionExterne.nbBalleGun = Hud.balle;
            GestionExterne.nbFleche = Hud.fleche;
            GestionExterne.nbBalleShotgun = Hud.balleShotgun;
            GestionExterne.nbLaser = Hud.laser;

            //Update ENNEMI VAGUE
            EV.Update(new Vector2(camera.X, camera.Y));

            //Limite Joueur
            #region Modification de la position Joueur
            if (GestionExterne.numeroPerso == 0)
            {
                //En X
                if (Joueur.position.X < 70) Joueur.position = new Vector2(70, Joueur.position.Y);
                else if (Joueur.position.X > 1860) Joueur.position = new Vector2(1860, Joueur.position.Y);
                ////En Y
                if (Joueur.position.Y < 60) Joueur.position = new Vector2(Joueur.position.X, 60);
                else if (Joueur.position.Y > 500) Joueur.position = new Vector2(Joueur.position.X, 500);
            }
            else if (GestionExterne.numeroPerso == 1)
            {
                if (Joueur2.position.X < 70) Joueur2.position = new Vector2(70, Joueur2.position.Y);
                else if (Joueur2.position.X > 1860) Joueur2.position = new Vector2(1860, Joueur2.position.Y);

                if (Joueur2.position.Y < 60) Joueur2.position = new Vector2(Joueur2.position.X, 60);
                else if (Joueur2.position.Y > 500) Joueur2.position = new Vector2(Joueur2.position.X, 500);
            }
            else
            {
                if (Joueur3.position.X < 70) Joueur3.position = new Vector2(70, Joueur3.position.Y);
                else if (Joueur3.position.X > 1860) Joueur3.position = new Vector2(1860, Joueur3.position.Y);

                if (Joueur3.position.Y < 60) Joueur3.position = new Vector2(Joueur3.position.X, 60);
                else if (Joueur3.position.Y > 500) Joueur3.position = new Vector2(Joueur3.position.X, 500);
            }
            #endregion

            //Changement darme
            #region Switch d'arme
            if (GestionExterne.numeroPerso == 0)
            {
                if (KeyboardHelper.KeyPressed(Keys.L))
                {
                    if (GestionExterne.haveRayGun)
                    {
                        if (Joueur.numArmement == 0)
                            Joueur.Load(1);
                        else if (Joueur.numArmement == 1)
                            Joueur.Load(2);
                        else if (Joueur.numArmement == 2)
                            Joueur.Load(3);
                        else if (Joueur.numArmement == 3)
                            Joueur.Load(4);
                        else
                            Joueur.Load(0);
                    }

                    else if (GestionExterne.haveScie)
                    {
                        if (Joueur.numArmement == 0)
                            Joueur.Load(1);
                        else if (Joueur.numArmement == 1)
                            Joueur.Load(2);
                        else if (Joueur.numArmement == 2)
                            Joueur.Load(3);
                        else
                            Joueur.Load(0);
                    }

                    else if (GestionExterne.haveShotGun)
                    {
                        if (Joueur.numArmement == 0)
                            Joueur.Load(1);
                        else if (Joueur.numArmement == 1)
                            Joueur.Load(2);
                        else if (Joueur.numArmement == 2)
                            Joueur.Load(0);
                    }
                    else
                    {
                        if (Joueur.numArmement == 0)
                            Joueur.Load(1);
                        else Joueur.Load(0);
                    }
                }
            }
            #endregion

            //Retour au menu
            if (KeyboardHelper.KeyPressed(Keys.Back))
            { RemoveScreen(this); AddScreen(new cMainMenu(serviceProvider, GraphicsDeviceManager)); }

            //Si Joueur Meurt retour au menu
            if (GestionExterne.numeroPerso == 0) { if (Joueur.vie.tuer) { RemoveScreen(this); AddScreen(new cMainMenu(serviceProvider, GraphicsDeviceManager)); } }
            else if (GestionExterne.numeroPerso == 1) { if (Joueur2.vie.tuer) { RemoveScreen(this); AddScreen(new cMainMenu(serviceProvider, GraphicsDeviceManager)); } }
            else  { if (Joueur3.vie.tuer) { RemoveScreen(this); AddScreen(new cMainMenu(serviceProvider, GraphicsDeviceManager)); } }
        }

        public override void Draw(GameTime gametime, SpriteBatch g)
        {
            g.End();
            g.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, camera.Transform);
            g.Draw(Ressources.Niveau1, new Vector2(), Color.White);

            //foreach (ObjCollisionable O in Obj) O.Draw(g);

            //AnimationPlayer(Zombie)
            foreach (Zombie Z in Zombies) Z.Draw(gametime, g);

            //AnimationPlayer(ZombieCR)
            foreach (ZombieCR Z in ZombiesCR) Z.Draw(gametime, g);

            //AnimationPlayer(ZombieCR)
            foreach (ZombieF Z in ZombiesF) Z.Draw(gametime, g);

            //AnimationPlayer(BossZombie)
            foreach (BossZombie Z in BossZombies) Z.Draw(gametime, g);

            //AnimationPlayer(BossZombie)
            foreach (BossZombieCR Z in BossZombiesCR) Z.Draw(gametime, g);

            //AnimationPlayer(BossZombie)
            foreach (BossZombieF Z in BossZombiesF) Z.Draw(gametime, g);

            if (GestionExterne.numeroPerso == 0) Joueur.Draw(gametime, g);
            else if (GestionExterne.numeroPerso == 1) Joueur2.Draw(gametime, g);
            else Joueur3.Draw(gametime, g);
          
            //Dessin des Dessins Avant Plan
            g.Draw(Ressources.Niv1Poubelle, new Vector2(55, 350), Color.White);
            g.Draw(Ressources.Niv1Flag, new Vector2(535, 38), Color.White);
            g.Draw(Ressources.Niv1Pilier, new Vector2(305, 100), Color.White);
            g.Draw(Ressources.Niv1Pont1, new Vector2(770, -12), Color.White);
            g.Draw(Ressources.Niv1Pont2, new Vector2(793, 307), Color.White);
            g.Draw(Ressources.Niv1MiniFlag1, new Vector2(1030, -10), Color.White);
            g.Draw(Ressources.Niv1MiniFlag2, new Vector2(1047, 235), Color.White);
            g.Draw(Ressources.Niv1Arbre, new Vector2(1240, 42), Color.White);
            g.Draw(Ressources.Niv1Maison, new Vector2(1170, 350), Color.White);

         
            //Dessin HUd et Nombre d'ennemis restants
            Hud.Draw(g);
            EV.Draw(g);
        }
    }
}
