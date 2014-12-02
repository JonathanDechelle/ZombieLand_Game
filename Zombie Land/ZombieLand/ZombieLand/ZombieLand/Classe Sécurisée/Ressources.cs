using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ZombieLand
{
    class Ressources
    {
        //Ressource Texte
        public static SpriteFont TexteItalic, TexteNormal;

        //Ressource ImageNiveau
        public static Texture2D MainPage, Encadrement, Encadrement2, Encadrement3, TitreJeu, Niveau1, Niv1Poubelle,
                                Niv1Flag, Niv1Pilier, Niv1Pont1, Niv1Pont2, Niv1MiniFlag1, Niv1MiniFlag2, Niv1Arbre,
                                Niv1Maison, Niveau2, Niv2Ecorce, Niv2Feuillage1, Niv2Feuillage2, Niveau2B, CoconNiv2B,
                                Colonne1Niv2B, Colonne2Niv2B;
                                

        //Ressource Ennemi
        public static Texture2D GrandMa, Zombie1, Zombie1Dead, Zombie1Attack, Zombie1PreAttack, Zombie1Win, ZombieCR,
                                RoueTournante, ZombieCRPreAttack, ZombieCRAttack, ZombieCRWin, ZombieCRDead,ZombieF,ZombieFDead,
                                ZombieFAttack,ZombieFWin,ZombieFPreAttack;

        //Ressource Item
        public static Texture2D FireBall, Ball, BalleS, Test, Argent, ShotgunItem, boite15G, boite50G, boite15S, boite50S, Eclat,
                                Boite15Sh, Boite50Sh,Scie,RayGun;


        //Ressource Effet
        public static Texture2D Explosion,Fire,Laser;

        //HealthBar
        public static Texture2D BarreVie;

        //Ressource Personnage
        public static Texture2D LFSkating, LFArming, LFCharging, GSkating, GCharging, GArming, GShooting, SSkating, SCharging,
                                SShooting,ScieSkating,ScieArming,ImagePerso,LSkating,LAttack;
                                

        //sound and effect
        public static SoundEffect FireGun,ZombieDie,ZombieCRDie,ZombieAttack,CoupDePoing;

        //Song
        public static Song CHSong, RESong,MSong,PSong,FSong,BPSong,MGSong;

        public static void Load(ContentManager Content)
        {
            RayGun = Content.Load<Texture2D>("RayGunItem");
            Laser = Content.Load<Texture2D>("AnimeLaser");
            LSkating = Content.Load<Texture2D>("LSkating");
            LAttack = Content.Load<Texture2D>("LAttack");
            Colonne2Niv2B = Content.Load<Texture2D>("Colonne2");
            Colonne1Niv2B = Content.Load<Texture2D>("Colonne1");
            CoconNiv2B = Content.Load<Texture2D>("Cocon");
            Niveau2B = Content.Load<Texture2D>("Map Kel-Trlesh");
            Niv2Feuillage2 = Content.Load<Texture2D>("Feullage2");
            Niv2Feuillage1 = Content.Load<Texture2D>("Feullage1");
            Niv2Ecorce = Content.Load<Texture2D>("Ecorce");
            ImagePerso = Content.Load<Texture2D>("ImagePerso");
            FSong = Content.Load<Song>("Frontier Song");
            BPSong = Content.Load<Song>("Beethoven Prophecy Song");
            MGSong = Content.Load<Song>("MGS Song");
            Niveau2 = Content.Load<Texture2D>("MapZomgoat Portal");
            MSong = Content.Load<Song>("Matrix Song");
            PSong = Content.Load<Song>("Prodigy Song");
            ZombieFPreAttack = Content.Load<Texture2D>("ZombieFPreAttack");
            ZombieFWin = Content.Load<Texture2D>("ZombieFWin");
            ZombieFAttack=Content.Load<Texture2D>("ZombieFAttack");
            ZombieFDead = Content.Load<Texture2D>("ZombieFDead");
            ZombieF = Content.Load<Texture2D>("ZombieF");
            ScieArming = Content.Load<Texture2D>("AttackScie");
            ScieSkating = Content.Load<Texture2D>("WalkingScie");
            Scie = Content.Load<Texture2D>("Scie");
            ZombieCRDie= Content.Load<SoundEffect>("Die2");
            ZombieCRDead = Content.Load<Texture2D>("ZombieCRKilled");
            ZombieCRWin = Content.Load<Texture2D>("ZombieCRWin");
            ZombieCRAttack = Content.Load<Texture2D>("ZombieCRAttack");
            ZombieCRPreAttack = Content.Load<Texture2D>("ZombieCrPreAttack");
            RoueTournante = Content.Load<Texture2D>("RoueTournante");
            ZombieCR=Content.Load<Texture2D>("ZombieCR");
            CoupDePoing = Content.Load<SoundEffect>("CoupDePoing");
            CHSong = Content.Load<Song>("ChainsawSong");
            RESong = Content.Load<Song>("ResidentEvil Song");
            ZombieAttack = Content.Load<SoundEffect>("Attack1");
            Boite15Sh = Content.Load<Texture2D>("boite15Sh");
            Boite50Sh = Content.Load<Texture2D>("boite50Sh");
            BalleS = Content.Load<Texture2D>("BalleS");
            Eclat = Content.Load<Texture2D>("Eclat");
            SShooting = Content.Load<Texture2D>("SShooting");
            SCharging = Content.Load<Texture2D>("SCharging");
            SSkating = Content.Load<Texture2D>("SSKating");
            boite15S = Content.Load<Texture2D>("boite15S");
            boite50S = Content.Load<Texture2D>("boite50S");
            boite15G = Content.Load<Texture2D>("Boite15");
            boite50G = Content.Load<Texture2D>("Boite50");
            ShotgunItem = Content.Load<Texture2D>("ShotgunItem");
            ZombieDie = Content.Load<SoundEffect>("Die1");
            FireGun = Content.Load<SoundEffect>("FireHit");
            Argent = Content.Load<Texture2D>("ZArgent");
            BarreVie = Content.Load<Texture2D>("HealtBar");
            Niv1MiniFlag1 = Content.Load<Texture2D>("Niv1MiniFlag1");
            Niv1MiniFlag2 = Content.Load<Texture2D>("Niv1MiniFlag2");
            Niv1Arbre = Content.Load<Texture2D>("Niv1Arbre");
            Niv1Maison = Content.Load<Texture2D>("Niv1Maison");
            Niv1Pont1 = Content.Load<Texture2D>("Niv1Pont1");
            Niv1Pont2 = Content.Load<Texture2D>("Niv1Pont2");
            Niv1Pilier = Content.Load<Texture2D>("Niv1Pilier");
            Niv1Flag = Content.Load<Texture2D>("Niv1Flag");
            Niv1Poubelle = Content.Load<Texture2D>("Niv1Poubelle");
            Ball = Content.Load<Texture2D>("Ball");
            GShooting = Content.Load<Texture2D>("GShooting");
            GArming = Content.Load<Texture2D>("GArming");
            GCharging = Content.Load<Texture2D>("GCharging");
            GSkating = Content.Load<Texture2D>("GSkating");
            Niveau1 = Content.Load<Texture2D>("Niveau1(1)");
            TitreJeu = Content.Load<Texture2D>("TitreIntro");
            Zombie1Win = Content.Load<Texture2D>("Zombie1Win");
            Zombie1PreAttack = Content.Load<Texture2D>("Zombie1PreAttack");
            Zombie1Attack = Content.Load<Texture2D>("Zombie1Attack");
            Zombie1Dead = Content.Load<Texture2D>("Zombie1Dead");
            Zombie1 = Content.Load<Texture2D>("Zombie1");
            LFCharging = Content.Load<Texture2D>("LFCharging");
            LFArming = Content.Load<Texture2D>("LFArming");
            Fire = Content.Load<Texture2D>("FireAnim");
            Encadrement3 = Content.Load<Texture2D>("Encadrement3");
            Encadrement2 = Content.Load<Texture2D>("Encadrement2");
            Encadrement = Content.Load<Texture2D>("Encadrement");
            MainPage = Content.Load<Texture2D>("MainPage");
            TexteNormal = Content.Load<SpriteFont>("Normal");
            TexteItalic = Content.Load<SpriteFont>("Italic");
            Test = Content.Load<Texture2D>("Test");
            GrandMa = Content.Load<Texture2D>("GrandMa");
            FireBall = Content.Load<Texture2D>("FireBall");
            Explosion = Content.Load<Texture2D>("Explosion");
            LFSkating = Content.Load<Texture2D>("LFSkating");
        }
    }
}
