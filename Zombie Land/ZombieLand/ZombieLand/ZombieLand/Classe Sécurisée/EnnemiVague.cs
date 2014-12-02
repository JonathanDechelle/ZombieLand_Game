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
    class EnnemiVague
    {
        Vector2 Position,PosTexte;
        int NbEnnemiRestant, NumeroVague, CompteurZombie, CompteurZombieCR, CompteurZombieF, CompteurBossZombie, CompteurBossZombieCR,
            CompteurBossZombieF;

        int[] nbEnnemieParVagues,nbEnnemieCRParVagues,nbEnnemieFParVagues,nbEnnemieBossZParVagues,nbEnnemieBossCRParVagues,
              nbEnnemieBossFParVagues;
       
        bool VagueInitialisé,Visible,DISPO;
        Color Clignotement;
        float ChangementEntreVague,VitesseApparition;
        string Texte;

        #region Get SET
        public int nbEnnemiRestant { get { return NbEnnemiRestant; } set { NbEnnemiRestant = value; } }

        public float vitesseApparition { get { return VitesseApparition; } set { VitesseApparition = value; } }

        public int[] NbEnnemieParVagues { get { return nbEnnemieParVagues; } }

        public int[] NbEnnemieCRParVagues { get { return nbEnnemieCRParVagues; } }

        public int[] NbEnnemieFParVagues { get { return nbEnnemieFParVagues; } }

        public int[] NbEnnemieBossZParVagues { get { return nbEnnemieBossZParVagues; } }

        public int[] NbEnnemieBossCRParVagues { get { return nbEnnemieBossCRParVagues; } }

        public int[] NbEnnemieBossFParVagues { get { return nbEnnemieBossFParVagues; } }

        public int numeroVague { get { return NumeroVague; } }

        public int compteurZombie { get { return CompteurZombie; } set { CompteurZombie = value; } }

        public int compteurZombieCR { get { return CompteurZombieCR; } set { CompteurZombieCR = value; } }

        public int compteurZombieF { get { return CompteurZombieF; } set { CompteurZombieF = value; } }

        public int compteurBossZombie { get { return CompteurBossZombie; } set { CompteurBossZombie = value; } }

        public int compteurBossZombieCR { get { return CompteurBossZombieCR; } set { CompteurBossZombieCR = value; } }

        public int compteurBossZombieF { get { return CompteurBossZombieF; } set { CompteurBossZombieF = value; } }

        #endregion

        public EnnemiVague()
        {
            nbEnnemieParVagues = new int[100];
            nbEnnemieCRParVagues = new int[100];
            nbEnnemieFParVagues = new int[100];
            nbEnnemieBossZParVagues = new int[100];
            nbEnnemieBossCRParVagues = new int[100];
            nbEnnemieBossFParVagues = new int[100];

            //Zombie Par vague
            nbEnnemieParVagues[0] = 5;
            nbEnnemieParVagues[1] = 7;
            nbEnnemieParVagues[2] = 10;
            nbEnnemieParVagues[3] = 15;
            nbEnnemieParVagues[4] = 25;
            nbEnnemieParVagues[5] = 30;
            nbEnnemieParVagues[6] = 35;
            nbEnnemieParVagues[7] = 40;
            nbEnnemieParVagues[8] = 45;
            nbEnnemieParVagues[9] = 50;
            nbEnnemieParVagues[10] = 55;
            nbEnnemieParVagues[11] = 60;
            nbEnnemieParVagues[12] = 65;
            nbEnnemieParVagues[13] = 70;
            nbEnnemieParVagues[14] = 150;
            nbEnnemieParVagues[15] = 170;
            nbEnnemieParVagues[16] = 190;
            nbEnnemieParVagues[17] = 200;
            nbEnnemieParVagues[18] = 210;
            nbEnnemieParVagues[19] = 215;
            nbEnnemieParVagues[20] = 230;


            //Zombie Chaise Roulante Par vague
            nbEnnemieCRParVagues[0] = 0;
            nbEnnemieCRParVagues[1] = 0;
            nbEnnemieCRParVagues[2] = 0;
            nbEnnemieCRParVagues[3] = 3;
            nbEnnemieCRParVagues[4] = 6;
            nbEnnemieCRParVagues[5] = 9;
            nbEnnemieCRParVagues[6] = 12;
            nbEnnemieCRParVagues[7] = 15;
            nbEnnemieCRParVagues[8] = 17;
            nbEnnemieCRParVagues[9] = 19;
            nbEnnemieCRParVagues[10] = 21;
            nbEnnemieCRParVagues[11] = 23;
            nbEnnemieCRParVagues[12] = 25;
            nbEnnemieCRParVagues[13] = 30;
            nbEnnemieCRParVagues[14] = 40;
            nbEnnemieCRParVagues[15] = 45;
            nbEnnemieCRParVagues[16] = 45;
            nbEnnemieCRParVagues[17] = 50;
            nbEnnemieCRParVagues[18] = 55;
            nbEnnemieCRParVagues[19] = 55;
            nbEnnemieCRParVagues[20] = 60;

            //Zombie fat Par Vague
            nbEnnemieFParVagues[0] = 0;
            nbEnnemieFParVagues[1] = 0;
            nbEnnemieFParVagues[2] = 0;
            nbEnnemieFParVagues[3] = 0;
            nbEnnemieFParVagues[4] = 0;
            nbEnnemieFParVagues[5] = 0;
            nbEnnemieFParVagues[6] = 1;
            nbEnnemieFParVagues[7] = 2;
            nbEnnemieFParVagues[8] = 4;
            nbEnnemieFParVagues[9] = 6;
            nbEnnemieFParVagues[10] = 8;
            nbEnnemieFParVagues[11] = 10;
            nbEnnemieFParVagues[12] = 12;
            nbEnnemieFParVagues[13] = 14;
            nbEnnemieFParVagues[14] = 18;
            nbEnnemieFParVagues[15] = 20;
            nbEnnemieFParVagues[16] = 22;
            nbEnnemieFParVagues[17] = 23;
            nbEnnemieFParVagues[18] = 25;
            nbEnnemieFParVagues[19] = 27;
            nbEnnemieFParVagues[20] = 29;

            //BossZOmbieParVague
            nbEnnemieBossZParVagues[0] = 0;
            nbEnnemieBossZParVagues[1] = 0;
            nbEnnemieBossZParVagues[2] = 1;
            nbEnnemieBossZParVagues[3] = 0;
            nbEnnemieBossZParVagues[4] = 0;
            nbEnnemieBossZParVagues[5] = 0;
            nbEnnemieBossZParVagues[6] = 0;
            nbEnnemieBossZParVagues[7] = 0;
            nbEnnemieBossZParVagues[8] = 0;
            nbEnnemieBossZParVagues[9] = 0;
            nbEnnemieBossZParVagues[10] =1;
            nbEnnemieBossZParVagues[11] =2;
            nbEnnemieBossZParVagues[12] =2;
            nbEnnemieBossZParVagues[13] =2;
            nbEnnemieBossZParVagues[14] = 2;
            nbEnnemieBossZParVagues[15] = 2;
            nbEnnemieBossZParVagues[16] = 3;
            nbEnnemieBossZParVagues[17] = 3;
            nbEnnemieBossZParVagues[18] = 3;
            nbEnnemieBossZParVagues[19] = 3;
            nbEnnemieBossZParVagues[20] = 3;

            //BossZOmbieCRParVague
            nbEnnemieBossCRParVagues[0] = 0;
            nbEnnemieBossCRParVagues[1] = 0;
            nbEnnemieBossCRParVagues[2] = 0;
            nbEnnemieBossCRParVagues[3] = 0;
            nbEnnemieBossCRParVagues[4] = 0;
            nbEnnemieBossCRParVagues[5] = 1;
            nbEnnemieBossCRParVagues[6] = 0;
            nbEnnemieBossCRParVagues[7] = 0;
            nbEnnemieBossCRParVagues[8] = 0;
            nbEnnemieBossCRParVagues[9] = 0;
            nbEnnemieBossCRParVagues[10] = 1;
            nbEnnemieBossCRParVagues[11] = 2;
            nbEnnemieBossCRParVagues[12] = 2;
            nbEnnemieBossCRParVagues[13] = 2;
            nbEnnemieBossCRParVagues[14] = 2;
            nbEnnemieBossCRParVagues[15] = 3;
            nbEnnemieBossCRParVagues[16] = 3;
            nbEnnemieBossCRParVagues[17] = 3;
            nbEnnemieBossCRParVagues[18] = 3;
            nbEnnemieBossCRParVagues[19] = 3;
            nbEnnemieBossCRParVagues[20] = 3;

            //BossZOmbieFParVague
            nbEnnemieBossFParVagues[0] = 0;
            nbEnnemieBossFParVagues[1] = 0;
            nbEnnemieBossFParVagues[2] = 0;
            nbEnnemieBossFParVagues[3] = 0;
            nbEnnemieBossFParVagues[4] = 0;
            nbEnnemieBossFParVagues[5] = 0;
            nbEnnemieBossFParVagues[6] = 0;
            nbEnnemieBossFParVagues[7] = 0;
            nbEnnemieBossFParVagues[8] = 1;
            nbEnnemieBossFParVagues[9] = 0;
            nbEnnemieBossFParVagues[10] = 1;
            nbEnnemieBossFParVagues[11] = 2;
            nbEnnemieBossFParVagues[12] = 2;
            nbEnnemieBossFParVagues[13] = 2;
            nbEnnemieBossFParVagues[14] = 2;
            nbEnnemieBossFParVagues[15] = 2;
            nbEnnemieBossFParVagues[16] = 3;
            nbEnnemieBossFParVagues[17] = 3;
            nbEnnemieBossFParVagues[18] = 3;
            nbEnnemieBossFParVagues[19] = 3;
            nbEnnemieBossFParVagues[20] = 3;

            Clignotement = Color.Red;
            Texte = "Zombies Restants";
            Visible = true;
            DISPO = true;

            VitesseApparition = 1.05f;
        }

        public void Update(Vector2 PosCamera)
        {
            Position = PosCamera;
            PosTexte = PosCamera;

            NumeroVague=GestionExterne.numeroVague;

            if (!VagueInitialisé)
            {
                NbEnnemiRestant = nbEnnemieParVagues[NumeroVague] + nbEnnemieCRParVagues[NumeroVague] + nbEnnemieFParVagues[NumeroVague] 
                                 +nbEnnemieBossZParVagues[NumeroVague]+nbEnnemieBossCRParVagues[NumeroVague]+nbEnnemieBossFParVagues[NumeroVague];
                VagueInitialisé = true; 
                if(VitesseApparition > 0.05) VitesseApparition -= 0.05f;
                CompteurZombie = 0; CompteurZombieCR = 0; CompteurZombieF = 0; CompteurBossZombie = 0; CompteurBossZombieCR = 0; CompteurBossZombieF = 0;
            }

           

           // Si EnnemiVague Non Dispo
            if (nbEnnemieParVagues[NumeroVague] == 0) { Texte = "Prochaine mis a jour"; DISPO = false; Clignotement.G -= 4; }
            
            if (NbEnnemiRestant == 0 && DISPO)
            {
                Clignotement.G -= 4;
                ChangementEntreVague += 0.005f;

                if (ChangementEntreVague > 2.5)
                {
                    Visible = true;
                    Texte = "Zombies Restants";
                    NumeroVague++;
                    GestionExterne.numeroVague++;
                    VagueInitialisé = false;
                    ChangementEntreVague = 0;
                }
                else if (ChangementEntreVague > 1)
                {
                    Visible = false;
                    Texte = "Changement de Vague";
                    PosTexte.X -= 50;
                }
               
            }
        }

        public void Draw(SpriteBatch g)
        {
            //écriture
            g.DrawString(Ressources.TexteItalic, Texte, new Vector2(PosTexte.X + 120, PosTexte.Y - 200), Clignotement);

            if (Visible && DISPO)
            {
                g.DrawString(Ressources.TexteItalic, "Vague", new Vector2(Position.X + 120, Position.Y - 220), Clignotement);
                g.DrawString(Ressources.TexteItalic, (NumeroVague + 1).ToString(), new Vector2(Position.X + 200, Position.Y - 220), Color.LightGreen);
                g.DrawString(Ressources.TexteItalic, NbEnnemiRestant.ToString(), new Vector2(Position.X + 330, Position.Y - 200), Color.LightGreen);
            }

        }
    }
}
