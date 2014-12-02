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
    class GestionMagasin
    {
        Vector2 PositionCurseur=new Vector2(100,200);
        Vector2 PositionIndicateur;
        Vector2 [] PositionCadreItem = new Vector2[9];
        TexteMagasin[] Texte = new TexteMagasin[100];
        List<ImageItem> Images = new List<ImageItem>();
        Color ColorSelect,ColorTexteAchat,ColorMap=Color.White;
        bool Achat,Acheté,PageInnaccessible;
        float TimerDecoloration;
        string TexteAchat;
        Animation Perso1,Perso2,Perso3;
        AnimationPlayer PPlayer;
        

        public GestionMagasin()
        {

            //Animation
            Perso1 = new Animation(Ressources.GSkating, 80, 0.4f, 1, true);
            Perso2 = new Animation(Perso_Speciaux.Ressource.Nothing, 80, 0.4f, 1, true);
            Perso3 =  new Animation(Perso_Speciaux.Ressource.AspirationK, 80, 0.1f, 1, true);
            PPlayer = new AnimationPlayer();

            // Texte du magasin
            #region Texte
            Texte[0] = new TexteMagasin(new Vector2(160, 200), 1, "Balle à l'unité");
            Texte[1] = new TexteMagasin(new Vector2(160, 280), 20, "Balle X15");
            Texte[2] = new TexteMagasin(new Vector2(160, 360), 75, "Balle X50");
            Texte[3] = new TexteMagasin(new Vector2(400, 200), 30, "Flèche à l'unité");
            Texte[4] = new TexteMagasin(new Vector2(400, 280), 80, "Flèche X15");
            Texte[5] = new TexteMagasin(new Vector2(400, 360), 150, "Flèche X50");

            if (!GestionExterne.haveShotGun)
                Texte[6] = new TexteMagasin(new Vector2(640, 200), 300, "Shotgun");
            else
            {
                Texte[6] = new TexteMagasin(new Vector2(640, 200), 3, "Cartouche");
                Texte[7] = new TexteMagasin(new Vector2(640, 280), 25, "Car. X15");
                Texte[8] = new TexteMagasin(new Vector2(640, 360), 90, "Car. X50");
            }

            Texte[9] = new TexteMagasin(new Vector2(1060, 200), 5000, "Scie");
            Texte[10] = new TexteMagasin(new Vector2(1060, 280), 2500, "Fusil Niveau 2");
            Texte[11] = new TexteMagasin(new Vector2(1060, 360), 5200, "Fusil Niveau 3");
            Texte[12] = new TexteMagasin(new Vector2(1300, 200), 3500, "ShotGun Niv. 2");
            Texte[13] = new TexteMagasin(new Vector2(1300, 280), 8000, "Tenchu");
            Texte[14] = new TexteMagasin(new Vector2(1300, 360), 0, "Jack");
            Texte[15] = new TexteMagasin(new Vector2(1540, 200), 0, "Niveau 1");
            Texte[16] = new TexteMagasin(new Vector2(1540, 280), 7000, "Niveau 2");
            if (GestionExterne.haveRayGun) Texte[17] = new TexteMagasin(new Vector2(1540, 360), 850, "Recharge");
            else if(GestionExterne.haveScie) Texte[17] = new TexteMagasin(new Vector2(1540, 360), 8500, "RayGun");
            #endregion

            //Cadre du magasin
            #region Cadre
            PositionCadreItem[0] = new Vector2(100, 200);
            PositionCadreItem[1] = new Vector2(340, 200);
            PositionCadreItem[2] = new Vector2(580, 200);
            PositionCadreItem[3] = new Vector2(100, 280);
            PositionCadreItem[4] = new Vector2(340, 280);
            PositionCadreItem[5] = new Vector2(580, 280);
            PositionCadreItem[6] = new Vector2(100, 360);
            PositionCadreItem[7] = new Vector2(340, 360);
            PositionCadreItem[8] = new Vector2(580, 360);
            #endregion


            TexteAchat = "VOUS N'AVEZ PAS ASSEZ D'ARGENT";

            //Images Descriptives<
            #region Image
            //Balle
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(110, 210, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));
            Images.Add(new ImageItem(Ressources.boite15G, new Rectangle(100, 280, Ressources.boite15G.Width, Ressources.boite15G.Height)));
            Images.Add(new ImageItem(Ressources.boite50G, new Rectangle(100, 360, Ressources.boite50G.Width, Ressources.boite50G.Height)));

            //Fleche
            Images.Add(new ImageItem(Ressources.FireBall, new Rectangle(330, 200, 70, 70), new Rectangle(110, 0, 70, 70)));
            Images.Add(new ImageItem(Ressources.boite15S, new Rectangle(340, 280, Ressources.boite15S.Width, Ressources.boite15S.Height)));
            Images.Add(new ImageItem(Ressources.boite50S, new Rectangle(340, 360, Ressources.boite50S.Width, Ressources.boite50S.Height)));
            Images.Add(new ImageItem(Ressources.FireBall, new Rectangle(337, 298, 50, 50), new Rectangle(110, 0, 70, 70)));
            Images.Add(new ImageItem(Ressources.FireBall, new Rectangle(337, 378, 50, 50), new Rectangle(110, 0, 70, 70)));
            Images.Add(new ImageItem(Ressources.ShotgunItem, new Rectangle(575, 195, (int)(Ressources.ShotgunItem.Width * 1.5), (int)(Ressources.ShotgunItem.Height * 1.5))));

            if (GestionExterne.haveShotGun)
            {
                //Shotgun
                Images[8].texture = Ressources.BalleS; Images[8].recImage = new Rectangle(585, 205, Ressources.Ball.Width * 3, Ressources.Ball.Height * 3);
                Images.Add(new ImageItem(Ressources.Boite15Sh, new Rectangle(580, 280, Ressources.Boite15Sh.Width, Ressources.Boite15Sh.Height)));
                Images.Add(new ImageItem(Ressources.Boite50Sh, new Rectangle(580, 360, Ressources.Boite50Sh.Width, Ressources.Boite50Sh.Height)));
            }

            //Scie
            Images.Add(new ImageItem(Ressources.Scie, new Rectangle(998, 205,(int)(Ressources.Scie.Width*1.3),(int)(Ressources.Scie.Height*1.3))));

            //Upgrade Balle
            Images.Add(new ImageItem(Ressources.boite15S, new Rectangle(998, 280, Ressources.boite15S.Width, Ressources.boite15S.Height)));
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(1000, 285, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(1010, 285, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));
            Images.Add(new ImageItem(Ressources.boite15S, new Rectangle(998, 360, Ressources.boite15S.Width, Ressources.boite15S.Height)));
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(1000, 365, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(1006, 365, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));
            Images.Add(new ImageItem(Ressources.Ball, new Rectangle(1012, 365, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2)));

            //Upgrade Shotgun
            Images.Add(new ImageItem(Ressources.boite15S, new Rectangle(1240, 200, Ressources.boite15S.Width, Ressources.boite15S.Height)));
            Images.Add(new ImageItem(Ressources.BalleS, new Rectangle(1240, 205, Ressources.BalleS.Width * 4, Ressources.BalleS.Height * 4)));
            Images.Add(new ImageItem(Ressources.BalleS, new Rectangle(1250, 205, Ressources.BalleS.Width*4, Ressources.BalleS.Height*4)));

            //PersoSpeciauxNinja
            Images.Add(new ImageItem(Perso_Speciaux.Ressource.ImagePerso, new Rectangle(1245, 285, 40, 40)));
            //PersoNormal
            Images.Add(new ImageItem(Ressources.ImagePerso, new Rectangle(1230, 352, 65, 65)));

            //Niveau1
            Images.Add(new ImageItem(Ressources.Niveau1, new Rectangle(1485, 205, 40, 40)));
            //Niveau2
            Images.Add(new ImageItem(Ressources.Niveau2B, new Rectangle(1485, 285, 40, 40)));

            //Laser
            if(GestionExterne.haveScie) Images.Add(new ImageItem(Ressources.RayGun, new Rectangle(1480, 360, 50, 50)));
            #endregion

        }

        public void Update()
        {
            //Animation
            if (GestionExterne.numeroPerso == 0) PPlayer.PlayAnimation(Perso1);
            else if (GestionExterne.numeroPerso == 1) PPlayer.PlayAnimation(Perso2);
            else PPlayer.PlayAnimation(Perso3);

            //Deplacement du curseur
            #region Deplacement
            if (KeyboardHelper.KeyPressed(Keys.D) || KeyboardHelper.KeyPressed(Keys.Right))
            {
                if (PositionCurseur.X < 500) PositionCurseur.X += 240;
                else if (!GestionExterne.haveShotGun) PageInnaccessible = true;
                else if (Images[0].recImage.X > -790)//Limite supérieur
                {
                    //Deplacement des objets pour les pages suivantes
                    PositionCurseur.X = 100;

                    foreach (TexteMagasin T in Texte) if (T != null) T.position = new Vector2(T.position.X - 900, T.position.Y);

                    foreach (ImageItem I in Images) I.recImage = new Rectangle(I.recImage.X - 900, I.recImage.Y, I.recImage.Width, I.recImage.Height);
                }
            }
            else if (KeyboardHelper.KeyPressed(Keys.A) || KeyboardHelper.KeyPressed(Keys.Left))
            {
               
                if (PositionCurseur.X > 100)    PositionCurseur.X -= 240;
                else if (Images[0].recImage.X<110)//Limite BAS
                {
                    foreach (TexteMagasin T in Texte) if (T != null) T.position = new Vector2(T.position.X + 900, T.position.Y);

                    foreach (ImageItem I in Images) I.recImage = new Rectangle(I.recImage.X + 900, I.recImage.Y, I.recImage.Width, I.recImage.Height);
                }

                //Debloque les pages(Pour revenir a ceux deja dispo)
                PageInnaccessible = false;
            }
            else if (KeyboardHelper.KeyPressed(Keys.Up) || KeyboardHelper.KeyPressed(Keys.W)) 
            {
                if (PositionCurseur.Y > 200) PositionCurseur.Y -= 80;
            }
            else if (KeyboardHelper.KeyPressed(Keys.Down) || KeyboardHelper.KeyPressed(Keys.S))
            {
                if (PositionCurseur.Y < 360) PositionCurseur.Y += 80;
            }
            #endregion

            //Deplacement indicateur
            #region Deplacement
            //Dependant de la page où il se situe
            if (Images[0].recImage.Location == new Point(110, 210))
            {
                if (PositionCurseur.X == 100) PositionIndicateur = new Vector2(50, 450);
                else if (PositionCurseur.X == 340) PositionIndicateur = new Vector2(70, 450);
                else if (PositionCurseur.X == 580) PositionIndicateur = new Vector2(90, 450);
            }
            else
            {
                if (PositionCurseur.X == 100) PositionIndicateur = new Vector2(110, 450);
                else if (PositionCurseur.X == 340) PositionIndicateur = new Vector2(130, 450);
                else if (PositionCurseur.X == 580) PositionIndicateur = new Vector2(150, 450);
            }
            #endregion

            //Update des Texte
            #region Update
            foreach (TexteMagasin T in Texte)
            {
                if (T != null)
                {
                    T.Update();
                    if (PositionCurseur.X + 60 == T.position.X && PositionCurseur.Y == T.position.Y)
                        if (T.color == Color.Black) { ColorSelect = Color.Green; T.color = Color.GreenYellow; }
                        else ColorSelect = Color.Red;
                }
            }
            #endregion

            //Demande d'achat
            if (ColorSelect == Color.Green) { Achat = true; if (!Acheté)ColorTexteAchat = Color.Black; } else Achat = false;

            //Texte d'achat
            if (Achat) TexteAchat = "POUR ACHETER APPUYER SUR ENTER"; else TexteAchat = "VOUS N'AVEZ PAS ASSEZ D'ARGENT";

            //Item non-disponoble selon certaine condition
            #region Speciaux
            if (!GestionExterne.haveScie && GestionExterne.haveShotGun)
            {
                if (Images[0].recImage.X == -790 && Images[0].recImage.Y == 210)
                {
                    if (PositionCurseur.Y == 360 && PositionCurseur.X == 580) TexteAchat = "NON-DISPONIBLE";
                }
            }
            else if (!GestionExterne.haveShotGun)
            {
                if (PositionCurseur.Y == 280 && PositionCurseur.X == 580 ||
                   PositionCurseur.Y == 360 && PositionCurseur.X == 580) TexteAchat = "NON-DISPONIBLE";
            }
            else PageInnaccessible = false;

            //Non dispo
            if (PageInnaccessible) TexteAchat = "DÉBLOQUER SOUS CERTAINES CONDITIONS";
           
            #endregion

            //Achat
            #region Achat des items
            if (Achat && KeyboardHelper.KeyPressed(Keys.Enter))
            {
                //Achat dans la premiere colonne
                #region Colonne 1
                if (PositionCurseur.X == 100)
                {
                    //---Page1 ---Balles
                    if (Images[0].recImage.Location == new Point(110, 210))
                    {
                        if (PositionCurseur.Y == 200) { GestionExterne.nbBalleGun++; GestionExterne.argent--; }
                        else if (PositionCurseur.Y == 280) { GestionExterne.nbBalleGun += 15; GestionExterne.argent -= 20; }
                        else if (PositionCurseur.Y == 360) { GestionExterne.nbBalleGun += 50; GestionExterne.argent -= 75; }
                    }

                    //---Page 2--Scie Upgrade
                    else if(Images[0].recImage.Location==new Point(-790,210))
                    {
                        if (PositionCurseur.Y == 200)
                        {
                            if (!GestionExterne.haveScie)
                            {
                                GestionExterne.haveScie = true; GestionExterne.argent -= 5000; Texte[9].prix = 0;
                                Texte[17] = new TexteMagasin(new Vector2(640, 360), 8500, "RayGun");
                                Images.Add(new ImageItem(Ressources.RayGun, new Rectangle(580, 360, 50, 50)));
                            }

                        }
                        else if (PositionCurseur.Y == 280)
                        {
                            if (GestionExterne.nivGun < 2) { GestionExterne.nivGun = 2; GestionExterne.argent -= 2500; Texte[10].prix = 0; }
                        }
                        else if (PositionCurseur.Y == 360)
                        {
                            if (GestionExterne.nivGun < 3) { GestionExterne.nivGun = 3; GestionExterne.argent -= 5200; Texte[10].prix = 0; Texte[11].prix = 0; }
                        }
                    }
                }
                #endregion

                //Achat dans la 2ieme colonne
                #region Colonne 2
                else if (PositionCurseur.X == 340) 
                {
                    //PAge 1 --Fleche
                    if (Images[0].recImage.Location == new Point(110, 210))
                    {
                        if (PositionCurseur.Y == 200) { GestionExterne.nbFleche++; GestionExterne.argent -= 30; }
                        else if (PositionCurseur.Y == 280) { GestionExterne.nbFleche += 15; GestionExterne.argent -= 80; }
                        else if (PositionCurseur.Y == 360) { GestionExterne.nbFleche += 50; GestionExterne.argent -= 150; }
                    }

                     //Page2 Upgrade,Perso Spéciaux
                    else if (Images[0].recImage.Location == new Point(-790, 210))
                    {
                        if (PositionCurseur.Y == 200 && GestionExterne.nivShotgun < 2) { GestionExterne.nivShotgun = 2; GestionExterne.argent -= 3500; Texte[12].prix = 0; }
                        else if (PositionCurseur.Y == 280 && GestionExterne.numeroPerso != 1) { GestionExterne.argent -= 8000; GestionExterne.numeroPerso = 1; }
                        else if (PositionCurseur.Y == 360) { GestionExterne.numeroPerso = 0; }
                    }
                }
                #endregion

                //Achat dans la 3ieme colonne
                #region Colonne 3
                else if (PositionCurseur.X == 580)
                {
                    if (!GestionExterne.haveShotGun)
                    {
                        //Page 1 Shotgun
                        if (PositionCurseur.Y == 200)
                        {
                            Texte[6].prix = 3; Texte[6].texte = "Cartouche";
                            GestionExterne.argent -= 300; GestionExterne.haveShotGun = true;
                            Texte[7] = new TexteMagasin(new Vector2(640, 280), 25, "Car. X15");
                            Texte[8] = new TexteMagasin(new Vector2(640, 360), 90, "Car. X50");

                            Images[8].texture = Ressources.BalleS; Images[8].recImage = new Rectangle(585, 205, Ressources.Ball.Width * 3, Ressources.Ball.Height * 3);
                            Images.Add(new ImageItem(Ressources.Boite15Sh, new Rectangle(580, 280, Ressources.Boite15Sh.Width, Ressources.Boite15Sh.Height)));
                            Images.Add(new ImageItem(Ressources.Boite50Sh, new Rectangle(580, 360, Ressources.Boite50Sh.Width, Ressources.Boite50Sh.Height)));
                        }
                    }
                    else
                    {
                        if (Images[0].recImage.Location == new Point(110, 210))
                        {
                            if (PositionCurseur.Y == 200) { GestionExterne.argent -= 3; GestionExterne.nbBalleShotgun++; }
                            else if (PositionCurseur.Y == 280) { GestionExterne.argent -= 25; GestionExterne.nbBalleShotgun += 15; }
                            else if (PositionCurseur.Y == 360) { GestionExterne.argent -= 90; GestionExterne.nbBalleShotgun += 50; }
                        }
                            //PAge 2-- Map
                        else if (Images[0].recImage.Location == new Point(-790, 210))
                        {
                            if (PositionCurseur.Y == 200) { GestionExterne.numMap = 0; ColorMap = Color.White; }
                            else if (PositionCurseur.Y == 280 && GestionExterne.numMap != 1) { GestionExterne.numMap = 1; GestionExterne.argent -= 7000; ColorMap = Color.White; }
                            else if (PositionCurseur.Y == 360 && GestionExterne.haveScie)
                            {
                                if (!GestionExterne.haveRayGun) { GestionExterne.haveRayGun = true; GestionExterne.nbLaser = 100; GestionExterne.argent -= 8500; Texte[17].prix = 850; Texte[17].texte = "Recharge"; }
                                else if (GestionExterne.nbLaser < 100) { GestionExterne.nbLaser = 100; GestionExterne.argent -= 850; }
                            }
                        }
                    }
                }
                #endregion

                Acheté = true;
                ColorTexteAchat.A = 255; ColorTexteAchat.G = 0;
            }
            #endregion

            //Clignotement lors de l'achat
            #region CHACHING
            if (Acheté)
            {
                ColorTexteAchat.A -= 5; ColorTexteAchat.G -= 5;
                TimerDecoloration += 0.10f;
                if (TimerDecoloration > 1) { TimerDecoloration = 0; ColorTexteAchat = Color.Black; Acheté = false; }
            }
            #endregion

            //Disparition de la map
            if (ColorMap.A > 0) { ColorMap.A --; ColorMap.G --; ColorMap.B --; ColorMap.R --;}
            
        }
      
        public void Draw(SpriteBatch g,GameTime gametime)
        {
            //Map
            if (GestionExterne.numMap == 0) g.Draw(Ressources.Niveau1, new Rectangle(640, 105, 100, 80), ColorMap);
            else if (GestionExterne.numMap == 1) g.Draw(Ressources.Niveau2B, new Rectangle(640, 105, 100, 80), ColorMap);

            //Animation
            PPlayer.Draw(gametime, g, new Vector2(125, 185), SpriteEffects.None);

            //PAge
            g.DrawString(Ressources.TexteNormal, "PAGE", new Vector2(0, 450), Color.White);
            g.Draw(Ressources.Test, new Rectangle(50, 450, 65, 25), Color.GreenYellow);
            g.Draw(Ressources.Test, new Rectangle(115, 450, 60, 25), Color.Yellow);

            //Argent
            g.Draw(Ressources.Argent, new Rectangle(100, 70, Ressources.Argent.Width / 2, Ressources.Argent.Height / 2), Color.White);
            g.DrawString(Ressources.TexteItalic, GestionExterne.argent.ToString(), new Vector2(150, 80), Color.White);

            //Balle 
            g.Draw(Ressources.Ball, new Rectangle(230, 73, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2), Color.White);
            g.DrawString(Ressources.TexteItalic, GestionExterne.nbBalleGun.ToString(), new Vector2(270, 80), Color.White);

            //Fleche
            g.Draw(Ressources.FireBall, new Rectangle(350, 65, 70, 70),
                   new Rectangle(110, 0, 70, 70), Color.White);
            g.DrawString(Ressources.TexteItalic, GestionExterne.nbFleche.ToString(), new Vector2(420, 80), Color.White);

            //Balle Shotgun
            if (GestionExterne.haveShotGun)
            {
                g.Draw(Ressources.BalleS, new Rectangle(510, 80, Ressources.Ball.Width * 2, Ressources.Ball.Height * 2), Color.White);
                g.DrawString(Ressources.TexteItalic, GestionExterne.nbBalleShotgun.ToString(), new Vector2(550, 80), Color.White);
            }
       
           
            //Encadrement Sous les images d'item
            for (int X = 0; X < PositionCadreItem.Length; X++)
            { g.Draw(Ressources.Encadrement3, new Rectangle((int)PositionCadreItem[X].X, (int)PositionCadreItem[X].Y, 50, 50), Color.LightGray); }
          
            //Encadrement de sélection
            g.Draw(Ressources.Encadrement3, new Rectangle((int)PositionCurseur.X, (int)PositionCurseur.Y, 50, 50), ColorSelect);

            //Dessin Indicateur
            g.Draw(Ressources.Test, new Rectangle((int)PositionIndicateur.X, (int)PositionIndicateur.Y, 25, 25),Color.Red);

            //Dessin des item et descriptif       
            foreach (ImageItem I in Images) I.Draw(g);

            //dessin des texte
            foreach (TexteMagasin T in Texte) { if (T != null) T.Draw(g); }

            //dessin de la mention d'achat
                if (Achat || Acheté) g.DrawString(Ressources.TexteItalic, TexteAchat, new Vector2(200, 140), ColorTexteAchat);
                else g.DrawString(Ressources.TexteItalic, TexteAchat, new Vector2(200, 140), Color.DarkRed);
            
        }

        class TexteMagasin
        {
            Vector2 Position;
            int Prix;
            Color Color;
            string Texte;

            public Vector2 position { get { return Position; } set { Position = value; } }

            public Color color { get { return Color; } set { Color = value; } }

            public string texte { get { return Texte; } set { Texte = value; } }

            public int prix { get { return Prix; } set { Prix = value; } }

            public TexteMagasin(Vector2 Position,int Prix,string Texte)
            {
                this.Position = Position;
                Color = Color.Black;
                this.Prix = Prix;
                this.Texte = Texte;
            }

            public void Update()
            {
                if (GestionExterne.argent < Prix) Color = Color.DarkRed;
          
                else { Color = Color.Black;}
            }

            public void Draw(SpriteBatch g)
            {
                g.DrawString(Ressources.TexteItalic, Texte, Position, Color);
                g.DrawString(Ressources.TexteItalic, "PRIX: " + Prix, new Vector2(Position.X, Position.Y + 20), Color);
            }
        }

        class ImageItem
        {
            Texture2D Texture;
            Rectangle RecImage,RecAnim;

            public Rectangle recImage { get { return RecImage; } set { RecImage = value; } }

            public Texture2D texture { get { return Texture; } set { Texture = value; } }

            public ImageItem(Texture2D Image,Rectangle recIma)
            {
                Texture = Image;
                RecImage = recIma;
            }

            public ImageItem(Texture2D Image, Rectangle recIma,Rectangle RecAnim)
            {
                Texture = Image;
                RecImage = recIma;
                this.RecAnim = RecAnim;
            }

            public void Update()
            {
                
            }

            public void Draw(SpriteBatch g)
            {
                if (RecAnim == new Rectangle())
                    g.Draw(Texture, RecImage, Color.White);
                else
                    g.Draw(Texture, RecImage, RecAnim, Color.White);
            }
        }
    }
}
