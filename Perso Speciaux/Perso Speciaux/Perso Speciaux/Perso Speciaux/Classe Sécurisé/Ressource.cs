using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Perso_Speciaux
{
    public class Ressource
    {
        //Ninja
        public static Texture2D Nothing, Running,Jumping,JumpingAttack,Degaine,RunningSword,NothingSword,Attack1,Attack2,Attack3,ImagePerso;

        //Kirby
        public static Texture2D NothingK,RunningK,JumpingK,AspirationK,AspirationContinueK,NothingKA,RunningKA,JumpingKA,LauchingK,AirLunchK,
                                NothingKT,WalkingKT,TransformingK,AttackKT;

        //Son Ninja
        public static SoundEffect BruitEpee;

        //HealthBar
        public static Texture2D BarreVie;
             
        public static void Load(ContentManager Content)
        {
            AttackKT = Content.Load<Texture2D>("KirbyAttackT");
            TransformingK = Content.Load<Texture2D>("KirbyTransforming");
            WalkingKT = Content.Load<Texture2D>("KirbyWalkingT");
            NothingKT = Content.Load<Texture2D>("KirbyNothingT");
            AirLunchK = Content.Load<Texture2D>("AirLunch");
            LauchingK = Content.Load<Texture2D>("KirbyLaunching");
            JumpingKA = Content.Load<Texture2D>("KirbyJumpingA");
            RunningKA = Content.Load<Texture2D>("KirbyRunningA");
            NothingKA = Content.Load<Texture2D>("KirbyNothingA");
            AspirationContinueK = Content.Load<Texture2D>("KirbyAspirationContinue");
            AspirationK = Content.Load<Texture2D>("KirbyAspiration");
            JumpingK = Content.Load<Texture2D>("KirbyJumping");
            RunningK = Content.Load<Texture2D>("KirbyRunning");
            NothingK = Content.Load<Texture2D>("KirbyNothing");
            ImagePerso = Content.Load<Texture2D>("ImageNinjaPerso");
            BarreVie = Content.Load<Texture2D>("HealtBar");
            BruitEpee = Content.Load<SoundEffect>("SwordSwing");
            Nothing = Content.Load<Texture2D>("NinjaNothing");
            Running = Content.Load<Texture2D>("NinjaRunning");
            Jumping = Content.Load<Texture2D>("NinjaJump");
            JumpingAttack = Content.Load<Texture2D>("NinjaJumpAttack");
            Degaine = Content.Load<Texture2D>("NinjaDegaine");
            RunningSword = Content.Load<Texture2D>("NinjaRunningSword");
            NothingSword = Content.Load<Texture2D>("NinjaNothingSword");
            Attack1 = Content.Load<Texture2D>("NinjaAttack1");
            Attack2 = Content.Load<Texture2D>("NinjaAttack2");
            Attack3 = Content.Load<Texture2D>("NinjaAttack3");
        }
    }
}
