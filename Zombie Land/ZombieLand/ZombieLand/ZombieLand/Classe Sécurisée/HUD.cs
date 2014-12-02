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
    class HUD
    {
        int Argent,Balle,Fleche,BalleShotgun;
        float  Laser;
        Vector2 Position;
        int Arme;
        bool PersoSpeciaux;
        ImageChargeur Chargeur=new ImageChargeur();

        public int argent { get { return Argent; } set { Argent = value; } }

        public int balle { get { return Balle; } set { Balle = value; } }

        public int fleche { get { return Fleche; } set { Fleche = value; } }

        public int balleShotgun { get { return BalleShotgun; } set { BalleShotgun = value; } }

        public float laser { get { return Laser; } set { Laser = value; } }


        public void Update(Vector2 PosCamera,ZLPlayer Joueur)
        {
            Position = PosCamera;
            Arme = Joueur.numArmement;
            Chargeur.Update(Position,Arme,Joueur.nbBalle,Joueur.nbFleche,Joueur.nbBalleS,Joueur.nbLaser);
        }

        public void Update(Vector2 PosCamera, Perso_Speciaux.Ninja Joueur)
        {
            Position = PosCamera;
            PersoSpeciaux = true;
        }

        public void Update(Vector2 PosCamera, Perso_Speciaux.Kirby Joueur)
        {
            Position = PosCamera;
            PersoSpeciaux = true;
        }

        public void Draw(SpriteBatch g)
        {
            //Argent
            g.Draw(Ressources.Argent, new Rectangle((int)Position.X - 340, (int)Position.Y - 220, Ressources.Argent.Width / 3, Ressources.Argent.Height / 3), Color.LightGreen);
            g.DrawString(Ressources.TexteItalic, Argent.ToString(), new Vector2(Position.X - 300, Position.Y - 220), Color.LightGreen);

            //Balle
            if (!PersoSpeciaux)
            {
                if (Arme == 0)
                {
                    Chargeur.Draw(g);
                    g.DrawString(Ressources.TexteItalic, Balle.ToString(), new Vector2(Position.X - 300, Position.Y - 190), Color.OrangeRed);
                }
                else if (Arme == 1)
                {
                    Chargeur.Draw(g);
                    g.DrawString(Ressources.TexteItalic, Fleche.ToString(), new Vector2(Position.X - 300, Position.Y - 190), Color.OrangeRed);
                }

                else if (Arme == 2)
                {
                    Chargeur.Draw(g);
                    g.DrawString(Ressources.TexteItalic, BalleShotgun.ToString(), new Vector2(Position.X - 300, Position.Y - 190), Color.OrangeRed);
                }

                else if (Arme == 4)
                {
                    Chargeur.Draw(g);
                    g.DrawString(Ressources.TexteItalic, Convert.ToInt32(Laser).ToString(), new Vector2(Position.X - 300, Position.Y - 190), Color.OrangeRed);
                }
            }
        }
    }

    class ImageChargeur
    {
        Texture2D BalleDuChargeur=Ressources.Ball;
        Vector2[] Position;
        Color[] CouleurBalle;

        public void Update(Vector2 Position,int NumArmement, int NbBalle,int NbFleche,int NbBalleS,double NbLaser)
        {
            if (NumArmement == 0)
            {
                #region Arme Gun
                //Dessin selon l'armement
                BalleDuChargeur = Ressources.Ball;

                //Grosseur de tableau
                {
                    if (GestionExterne.nivGun == 1) { this.Position = new Vector2[5]; CouleurBalle = new Color[5]; }
                    else if (GestionExterne.nivGun == 2) { this.Position = new Vector2[10]; CouleurBalle = new Color[10]; }
                    else if (GestionExterne.nivGun == 3) { this.Position = new Vector2[15]; CouleurBalle = new Color[15]; }
                }

                //Couleur des balles

                switch (NbBalle)
                {
                    #region COuleur balle
                    case 0: for (int X = 0; X < 5; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 2 || GestionExterne.nivGun==3) for (int X = 5; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 1: CouleurBalle[0] = Color.White; for (int X = 1; X < 5; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 2 ||GestionExterne.nivGun==3) for (int X = 5; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 2: CouleurBalle[0] = Color.White; CouleurBalle[1] = Color.White;
                        for (int X = 2; X < 5; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 2 || GestionExterne.nivGun==3) for (int X = 5; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 3: for (int X = 0; X < 3; X++) CouleurBalle[X] = Color.White; CouleurBalle[3] = Color.Gray; CouleurBalle[4] = Color.Gray;
                        if (GestionExterne.nivGun == 2 ||GestionExterne.nivGun==3) for (int X = 5; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 4: for (int X = 0; X < 4; X++) CouleurBalle[X] = Color.White; CouleurBalle[4] = Color.Gray;
                        if (GestionExterne.nivGun == 2 ||GestionExterne.nivGun==3) for (int X = 4; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 5: for (int X = 0; X < 5; X++) CouleurBalle[X] = Color.White;
                        if (GestionExterne.nivGun == 2 ||GestionExterne.nivGun==3) for (int X = 5; X < 10; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 6: for (int X = 0; X < 6; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[6] = Color.Gray;
                        CouleurBalle[7] = Color.Gray;
                        CouleurBalle[8] = Color.Gray;
                        CouleurBalle[9] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 7: for (int X = 0; X < 7; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[7] = Color.Gray;
                        CouleurBalle[8] = Color.Gray;
                        CouleurBalle[9] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 8: for (int X = 0; X < 8; X++) CouleurBalle[X] = Color.White; CouleurBalle[8] = Color.Gray; CouleurBalle[9] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 9: for (int X = 0; X < 9; X++) CouleurBalle[X] = Color.White; CouleurBalle[9] = Color.Gray;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 10: for (int X = 0; X < 10; X++) CouleurBalle[X] = Color.White;
                        if (GestionExterne.nivGun == 3) for (int X = 10; X < 15; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 11: for (int X = 0; X < 11; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[14] = Color.Gray; CouleurBalle[13] = Color.Gray; CouleurBalle[12]=Color.Gray;CouleurBalle[11]=Color.Gray;
                        break;
                    case 12: for (int X = 0; X < 12; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[14] = Color.Gray; CouleurBalle[13]=Color.Gray;CouleurBalle[12]=Color.Gray;
                        break;
                    case 13: for (int X = 0; X < 13; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[14] = Color.Gray;CouleurBalle[13]=Color.Gray;
                        break;
                    case 14: for (int X = 0; X < 14; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[14] = Color.Gray;
                        break;
                    case 15: for (int X = 0; X < 15; X++) CouleurBalle[X] = Color.White;
                        break;
                    #endregion
                }

                //Repositionnement des balles
                #region Position
                this.Position[0] = new Vector2(Position.X - 335, Position.Y - 185);
                this.Position[1] = new Vector2(Position.X - 330, Position.Y - 185);
                this.Position[2] = new Vector2(Position.X - 325, Position.Y - 185);
                this.Position[3] = new Vector2(Position.X - 320, Position.Y - 185);
                this.Position[4] = new Vector2(Position.X - 315, Position.Y - 185);

                if (GestionExterne.nivGun == 2 || GestionExterne.nivGun==3)
                {
                    this.Position[5] = new Vector2(Position.X - 335, Position.Y - 175);
                    this.Position[6] = new Vector2(Position.X - 330, Position.Y - 175);
                    this.Position[7] = new Vector2(Position.X - 325, Position.Y - 175);
                    this.Position[8] = new Vector2(Position.X - 320, Position.Y - 175);
                    this.Position[9] = new Vector2(Position.X - 315, Position.Y - 175);
                }

                if (GestionExterne.nivGun == 3)
                {
                    this.Position[10] = new Vector2(Position.X - 335, Position.Y - 165);
                    this.Position[11] = new Vector2(Position.X - 330, Position.Y - 165);
                    this.Position[12] = new Vector2(Position.X - 325, Position.Y - 165);
                    this.Position[13] = new Vector2(Position.X - 320, Position.Y - 165);
                    this.Position[14] = new Vector2(Position.X - 315, Position.Y - 165);
                }
                #endregion

                #endregion
            }
            else if (NumArmement == 1)
            {
                #region LF
                //Texture
                BalleDuChargeur = Ressources.FireBall;

                //Position
                this.Position = new Vector2[1]; this.Position[0] = new Vector2(Position.X - 340, (int)Position.Y - 190);

                //Color 
                CouleurBalle = new Color[1]; if (NbFleche > 0) CouleurBalle[0] = Color.White; else CouleurBalle[0] = Color.Gray;
                #endregion
            }

            else if (NumArmement == 2)
            {
                #region Arme ShotGun
                //Dessin selon l'armement
                BalleDuChargeur = Ressources.BalleS;

                //Grosseur de tableau
                {
                    if (GestionExterne.nivShotgun == 1) { this.Position = new Vector2[8]; CouleurBalle = new Color[8]; }
                    else if (GestionExterne.nivShotgun == 2) { this.Position = new Vector2[12]; CouleurBalle = new Color[12]; }
                }

                //Couleur des balles
                switch (NbBalleS)
                {
                    #region COuleur balle
                    case 0: for (int X = 0; X < 8; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 1: CouleurBalle[0] = Color.White; for (int X = 1; X < 8; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 2: CouleurBalle[0] = Color.White; CouleurBalle[1] = Color.White;
                        for (int X = 2; X < 8; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 3: CouleurBalle[0] = Color.White; CouleurBalle[1] = Color.White; CouleurBalle[2] = Color.White;
                        for (int X = 3; X < 8; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 4: CouleurBalle[0] = Color.White; CouleurBalle[1] = Color.White; CouleurBalle[2] = Color.White;
                        CouleurBalle[3] = Color.White;
                        for (int X = 4; X < 8; X++) CouleurBalle[X] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 5: for (int X = 0; X < 5; X++) CouleurBalle[X] = Color.White; CouleurBalle[5] = Color.Gray; CouleurBalle[6] = Color.Gray; CouleurBalle[7] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 6: for (int X = 0; X < 6; X++) CouleurBalle[X] = Color.White; CouleurBalle[6] = Color.Gray; CouleurBalle[7] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 7: for (int X = 0; X < 7; X++) CouleurBalle[X] = Color.White; CouleurBalle[7] = Color.Gray;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;

                    case 8: for (int X = 0; X < 8; X++) CouleurBalle[X] = Color.White;
                        if (GestionExterne.nivShotgun == 2) for (int X = 8; X < 12; X++) CouleurBalle[X] = Color.Gray;
                        break;
                    case 9: for (int X = 0; X < 9; X++) CouleurBalle[X] = Color.White;
                        CouleurBalle[9] = Color.Gray;
                        CouleurBalle[10] = Color.Gray;
                        CouleurBalle[11] = Color.Gray;
                        break;
                    case 10: for (int X = 0; X < 10; X++) CouleurBalle[X] = Color.White; CouleurBalle[10] = Color.Gray; CouleurBalle[11] = Color.Gray;
                        break;
                    case 11: for (int X = 0; X < 11; X++) CouleurBalle[X] = Color.White; CouleurBalle[11] = Color.Gray;
                        break;
                    case 12: for (int X = 0; X < 12; X++) CouleurBalle[X] = Color.White;
                        break;
                    #endregion
                }

                //Repositionnement des balles
                #region Position
                this.Position[0] = new Vector2(Position.X - 335, Position.Y - 185);
                this.Position[1] = new Vector2(Position.X - 330, Position.Y - 185);
                this.Position[2] = new Vector2(Position.X - 325, Position.Y - 185);
                this.Position[3] = new Vector2(Position.X - 320, Position.Y - 185);
                this.Position[4] = new Vector2(Position.X - 335, Position.Y - 175);
                this.Position[5] = new Vector2(Position.X - 330, Position.Y - 175);
                this.Position[6] = new Vector2(Position.X - 325, Position.Y - 175);
                this.Position[7] = new Vector2(Position.X - 320, Position.Y - 175);

                if (GestionExterne.nivShotgun == 2)
                {
                    this.Position[8] = new Vector2(Position.X - 335, Position.Y - 165);
                    this.Position[9] = new Vector2(Position.X - 330, Position.Y - 165);
                    this.Position[10] = new Vector2(Position.X - 325, Position.Y - 165);
                    this.Position[11] = new Vector2(Position.X - 320, Position.Y - 165);
                }
                #endregion

                #endregion
            }

            else if (NumArmement == 4)
            {
                #region RayGun
                //Texture
                BalleDuChargeur = Ressources.Laser;

                //Position
                this.Position = new Vector2[1]; this.Position[0] = new Vector2(Position.X - 340, (int)Position.Y - 245);

                //Color 
                CouleurBalle = new Color[1]; if (NbLaser >= 0.99) CouleurBalle[0] = Color.White; else CouleurBalle[0] = Color.Gray;
                #endregion
            }

        }

        public void Draw(SpriteBatch g)
        {
            if (Position != null)
            {
                if (BalleDuChargeur == Ressources.Ball)
                    for (int X = 0; X < Position.Length; X++) g.Draw(BalleDuChargeur, Position[X], CouleurBalle[X]);

                else if (BalleDuChargeur == Ressources.FireBall)
                    g.Draw(Ressources.FireBall, new Rectangle((int)Position[0].X, (int)Position[0].Y, 30, 30), new Rectangle(240, 0, 50, 50), CouleurBalle[0]);

                else if (BalleDuChargeur == Ressources.BalleS)
                    for (int X = 0; X < Position.Length; X++) g.Draw(BalleDuChargeur, Position[X], CouleurBalle[X]);
                
                else if(BalleDuChargeur==Ressources.Laser)
                    g.Draw(Ressources.Laser, new Rectangle((int)Position[0].X, (int)Position[0].Y, 30, 150), new Rectangle(0, 0, 50, 150), CouleurBalle[0]);

            }
        }
    }
}
