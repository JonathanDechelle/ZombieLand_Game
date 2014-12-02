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
    class GestionExterne
    {
        static int NbBalleGun;
        static int NbFleche;
        static int Argent;
        static bool HaveShotGun;
        static bool HaveScie;
        static bool HaveRayGun;
        static int NbBalleShotGun;
        static int NumeroVague;
        static int NivGun;
        static int NivShotgun;
        static int NumSong;
        static int NumeroPerso;
        static int NumMap;
        static float NbLaser;

        static public int nbBalleGun { get { return NbBalleGun; } set { NbBalleGun = value; } }
        static public int nbFleche { get { return NbFleche; } set { NbFleche = value; } }
        static public int argent { get { return Argent; } set { Argent = value; } }
        static public bool haveShotGun { get { return HaveShotGun; } set { HaveShotGun = value; } }
        static public int nbBalleShotgun { get { return NbBalleShotGun; } set { NbBalleShotGun = value; } }
        static public bool haveScie { get { return HaveScie; } set { HaveScie = value; } }
        static public int numeroVague { get { return NumeroVague; } set { NumeroVague = value; } }
        static public int nivGun { get { return NivGun; } set { NivGun = value; } }
        static public int nivShotgun { get { return NivShotgun; } set { NivShotgun = value; } }
        static public int numSong { get { return NumSong; } set { NumSong = value; } }
        static public int numeroPerso { get { return NumeroPerso; } set { NumeroPerso = value; } }
        static public int numMap { get { return NumMap; } set { NumMap = value; } }
        static public bool haveRayGun { get { return HaveRayGun; } set { HaveRayGun = value; } }
        static public float nbLaser { get { return NbLaser; } set{NbLaser=value;} }
    }
}
