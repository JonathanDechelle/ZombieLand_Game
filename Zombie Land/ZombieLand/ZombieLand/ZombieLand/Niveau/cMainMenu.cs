using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ZombieLand
{
    class cMainMenu:GameScreen
    {
        Animation Fire,ZombieWalking;
        AnimationPlayer FirePlayer;
        Vector2 PositionFeu;
        bool TutoOn,inMagasin,ChoixVague;
        Rectangle RecTuto;
        List<Zombie> Zombies;

        List<FireBall> FireBalls;
        Color ColorF=Color.White;
        ZLPlayer Player;
        GestionMagasin Magasin;

        public cMainMenu(IServiceProvider serviceProvider, GraphicsDeviceManager graphics)
            :base(serviceProvider, graphics)
        {
            Player = new ZLPlayer(new  Vector2(158,300),true,false);
            FirePlayer = new AnimationPlayer();
            Fire = new Animation(Ressources.Fire, 75, 0.5f, 1, true);
            ZombieWalking = new Animation(Ressources.Zombie1, 80, 1.5f, 1, true);
            PositionFeu = new Vector2(440, 125);
            RecTuto = new Rectangle(90, 160, 270, 250);
            Zombies = new List<Zombie>();
            FireBalls = new List<FireBall>();
            FireBalls.Add(new FireBall(new Vector2(-100,280),SpriteEffects.None));
            Zombies.Add(new Zombie(new Vector2(255, 300)));
            Zombies.Add(new Zombie(new Vector2(235, 360)));
            Zombies.Add(new Zombie(new Vector2(265, 240)));

            Magasin = new GestionMagasin();

            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            if (MediaPlayer.Queue.ActiveSong != Ressources.CHSong) MediaPlayer.Play(Ressources.CHSong);
        }

        public override void Load()
        {
            Player.Load(1);
            // GestionExterne.argent = 1000;
            GestionExterne.numeroVague = 5;
        }

        public override void Update(GameTime gameTime)
        {
          
                FirePlayer.PlayAnimation(Fire);

                if (TutoOn)
                {
                     #region Tuto Actif
     
                    Player.Update(gameTime, Zombies, null, null, null, null,null,null, null);

                    //Changement darme
                    #region Switch d'arme
                    if (KeyboardHelper.KeyPressed(Keys.L))
                    {
                        if (GestionExterne.haveRayGun)
                        {
                            if (Player.numArmement == 0)
                                Player.Load(1);
                            else if (Player.numArmement == 1)
                                Player.Load(2);
                            else if (Player.numArmement == 2)
                                Player.Load(3);
                            else if (Player.numArmement == 3)
                                Player.Load(4);
                            else
                                Player.Load(0);
                        }
                            
                        else if (GestionExterne.haveScie)
                        {
                            if (Player.numArmement == 0)
                                Player.Load(1);
                            else if (Player.numArmement == 1)
                                Player.Load(2);
                            else if (Player.numArmement == 2)
                                Player.Load(3);
                            else
                                Player.Load(0);
                        }
                        else if (GestionExterne.haveShotGun)
                        {
                            if (Player.numArmement == 0)
                                Player.Load(1);
                            else if (Player.numArmement == 1)
                                Player.Load(2);
                            else if (Player.numArmement == 2)
                                Player.Load(0);
                        }
                        else
                        {
                            if (Player.numArmement == 0)
                                Player.Load(1);
                            else Player.Load(0);
                        }
                    }
                    #endregion

                    #region Zombie Update
                    for (int Z = 0; Z < Zombies.Count; Z++)
                    {
                        Zombies[Z].Update(Player, null);
                        if (Zombies[Z].remove) Zombies.RemoveAt(Z);
                    }
                    #endregion

                    //Couleur du feu
                    ColorF = Color.LightGreen;

                    //Retour au menu
                    if (KeyboardHelper.KeyPressed(Keys.Back))
                    { TutoOn = false; ColorF = Color.White; }

                    //Mettre plus de zombies
                    if (KeyboardHelper.KeyPressed(Keys.Z))
                    {
                        #region Rajout de zombie
                       // Si la premiere es deja la
                        if ((int)(Zombies.Count % 2) == 0)
                        {
                            Zombies.Add(new Zombie(new Vector2(215, 400)));
                            Zombies.Add(new Zombie(new Vector2(235, 400)));
                            Zombies.Add(new Zombie(new Vector2(265, 400)));
                        }
                        else
                        {
                            Zombies.Add(new Zombie(new Vector2(255, 300)));
                            Zombies.Add(new Zombie(new Vector2(235, 360)));
                            Zombies.Add(new Zombie(new Vector2(265, 240)));
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region TutoOff
                    if (!inMagasin)
                    {
                        //Déplacement de l'animation du feu (selection des options entre jouer magasin et tutorial)
                        #region Position du feu
                        if (KeyboardHelper.KeyPressed(Keys.Down) || KeyboardHelper.KeyPressed(Keys.S))
                        {
                            if (PositionFeu == new Vector2(440, 125))
                                PositionFeu = new Vector2(440, 225);
                            else
                                if (PositionFeu == new Vector2(440, 225) && !ChoixVague)
                                    PositionFeu = new Vector2(440, 325);
                                else
                                    PositionFeu = new Vector2(440, 125);
                        }

                        if (KeyboardHelper.KeyPressed(Keys.Up) || KeyboardHelper.KeyPressed(Keys.W))
                        {
                            if (PositionFeu == new Vector2(440, 125) && !ChoixVague)
                                PositionFeu = new Vector2(440, 325);
                            else
                                if (PositionFeu == new Vector2(440, 225))
                                    PositionFeu = new Vector2(440, 125);
                                else
                                    PositionFeu = new Vector2(440, 225);
                        }
                        #endregion


                        //Selection de L'option 
                        #region Selection
                        if (KeyboardHelper.KeyPressed(Keys.Enter))
                        {
                            if (PositionFeu == new Vector2(440, 125))
                            {
                                if (ChoixVague)
                                {
                                    RemoveScreen(this);
                                    if(GestionExterne.numMap==1)
                                    AddScreen(new cNiveau2 (serviceProvider, GraphicsDeviceManager));
                                    else if(GestionExterne.numMap==0)
                                    AddScreen(new cNiveau1(serviceProvider, GraphicsDeviceManager));
                                }
                                ChoixVague = true;
                                ColorF = Color.LightGreen;
                            }
                            else if (PositionFeu == new Vector2(440, 325))
                            {
                                TutoOn = true;
                            }
                            else if (PositionFeu == new Vector2(440, 225))
                            {
                                if (!ChoixVague) inMagasin = true;
                                else
                                {
                                    GestionExterne.numeroVague = 0; RemoveScreen(this);
                                    AddScreen(new cNiveau1(serviceProvider, GraphicsDeviceManager));
                                }
                            }
                                
                        }
                        #endregion
                    }
                    else
                    {
                        Magasin.Update();
                    }

                    if (KeyboardHelper.KeyPressed(Keys.Back)) { ColorF = Color.White; inMagasin = false; ChoixVague = false; }
                    #endregion
                }
        }

        public override void Draw(GameTime gametime, SpriteBatch g)
        {
            g.Draw(Ressources.MainPage, new Rectangle(0, 0, 800, 500), Color.White);

            //Gestion Systeme
            #region Couleur Systeme
            g.Draw(Ressources.Test, new Rectangle(0, 0, 100, 100), Color.DarkRed);
            g.Draw(Ressources.Test, new Rectangle(0, 400, 100, 100), Color.DarkRed);
            g.Draw(Ressources.Test, new Rectangle(700, 0, 100, 100), Color.DarkRed);
            g.Draw(Ressources.Test, new Rectangle(700, 400, 100, 100), Color.DarkRed);
            g.Draw(Ressources.Test, new Rectangle(25, 25, 750, 450), Color.Green);
            g.Draw(Ressources.Test, new Rectangle(50, 50, 700, 400), Color.CornflowerBlue);
            g.Draw(Ressources.Test, new Rectangle(0, 0, 50, 50), Color.Green);
            g.Draw(Ressources.Test, new Rectangle(0, 450, 50, 50), Color.Green);
            g.Draw(Ressources.Test, new Rectangle(750, 0, 50, 50), Color.Green);
            g.Draw(Ressources.Test, new Rectangle(750, 450, 50, 50), Color.Green);
            #endregion

            //Avec Image Ressource
            if (!inMagasin)
            {
                #region Hors magasin
                //encadrement
                g.Draw(Ressources.Encadrement, new Rectangle(75, 50, 300, 100), Color.Green);
                g.Draw(Ressources.Encadrement3, new Rectangle(90, 160, 270, 250), Color.DarkRed);
                g.Draw(Ressources.Encadrement2, new Rectangle(400, 260, 250, 100), Color.DarkRed);

            if (!TutoOn)
            {
                g.Draw(Ressources.Encadrement2, new Rectangle(400, 60, 250, 100), Color.DarkRed);
                g.Draw(Ressources.Encadrement2, new Rectangle(400, 160, 250, 100), Color.DarkRed);

            }
            else g.Draw(Ressources.Encadrement3, new Rectangle(370, 70, 360, 190), Color.SandyBrown);

            //Texte
            g.DrawString(Ressources.TexteItalic, "ZOMBIE LAND", new Vector2(150, 87), Color.White);

            if (!TutoOn)
            {
                if (!ChoixVague)
                {
                    g.DrawString(Ressources.TexteItalic, "Commencer", new Vector2(460, 100), Color.White);
                    g.DrawString(Ressources.TexteItalic, "Magasin", new Vector2(480, 200), Color.White);
                }
                else
                {
                    g.DrawString(Ressources.TexteItalic, "VAGUES " + (GestionExterne.numeroVague + 1), new Vector2(470, 145), Color.Red);
                    g.DrawString(Ressources.TexteNormal, "CONTINUER", new Vector2(470, 100), Color.White);
                    g.DrawString(Ressources.TexteNormal, "RECOMMENCER", new Vector2(455, 200), Color.White);
                }
            }
            else
            {
                ///Texte tuto
                g.DrawString(Ressources.TexteNormal, "R -> CHARGER (1 fois ou plusieurs \n              fois en fonction de l'arme)", new Vector2(390, 90), Color.DarkRed);
                g.DrawString(Ressources.TexteNormal, "ESPACE -> TIRER/SE DEBATTRE ", new Vector2(390, 150), Color.DarkRed);
                g.DrawString(Ressources.TexteNormal, "WASD ET FLECHE -> DIRECTION", new Vector2(390, 185), Color.DarkRed);
                g.DrawString(Ressources.TexteNormal, "L -> CHANGEMENT D'ARME", new Vector2(390, 215), Color.DarkRed);
                g.DrawString(Ressources.TexteNormal, "BACKSPACE -> QUITTER TUTORIEL.. Z -> NOUVELLE HORDE ZOMBIES ", new Vector2(100, 450), Color.White);
            }

               g.DrawString(Ressources.TexteItalic, "Tutorial", new Vector2(480, 300), Color.White);

                //AnimationPLayer(feu)
                FirePlayer.Draw(gametime, g, PositionFeu, SpriteEffects.None, ColorF);
                FirePlayer.Draw(gametime, g, new Vector2(PositionFeu.X + 165, PositionFeu.Y - 5), SpriteEffects.None, ColorF);

                //AnimationPlayer(Zombie)
                foreach (Zombie Z in Zombies) Z.Draw(gametime, g);

                Player.Draw(gametime, g);
                #endregion
            }
            else
            {
                Magasin.Draw(g,gametime);
            }

            if (inMagasin) g.DrawString(Ressources.TexteNormal, "BACKSPACE -> RETOURNER AU MENU ", new Vector2(200, 450), Color.White);
        }
    }
}
