using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Perso_Speciaux
{
    public class Kirby
    {
        Animation Nothing, Running, Jumping,Aspiration,AspirationContinue;
        Animation NothingA,RunningA,Lauching,AirLunch,Transforming;
        Animation NothingT,WalkingT,AttackT;
        AnimationPlayer APJoueur,APAir;

        Vector2 Position,PositionAir;
        Rectangle RecPerso,RecAir;
        SpriteEffects flip,flipAir;
        BarreDeVie Vie;

        bool Hurt, Deplacement, Jump,Attack, Launch,Respire,RespireContinue,EnnemiIn,Tir,Transform,Transformation;
        bool Gravity,JumpOne;

        Vector2 Speed;
      
        public Vector2 position { get { return Position; } set { Position = value; } }

        public Rectangle recPerso { get { return RecPerso; } set { RecPerso = value; } }

        public SpriteEffects Flip { get { return flip; } }

        public bool hurt { get { return Hurt; } set { Hurt = value; } }

        public BarreDeVie vie { get { return Vie; } }

        public bool attack { get { return Attack; } }

        public bool jump { get { return Jump; } }

        public Rectangle recAir { get { return RecAir; } }

        public bool tir { get { return Tir; } set { Tir = value; } }

        public bool launch { get { return Launch; } set { Launch = value; } }

        public bool respire { get { return Respire; } }

        public bool ennemiIn { get { return EnnemiIn; } set { EnnemiIn = value; } }

        public bool transform { get { return Transform; } }

        public Kirby(Vector2 Position)
        {
            this.Position = Position;
        }

        public void Load()
        {
            Nothing = new Animation(Ressource.NothingK, 80, 0.4f, 1, true);
            Running = new Animation(Ressource.RunningK, 80, 0.1f, 1, true);
            Jumping = new Animation(Ressource.JumpingK, 80, 0.1f, 1, false);
            Aspiration = new Animation(Ressource.AspirationK, 80, 0.1f, 1, true);
            AspirationContinue = new Animation(Ressource.AspirationContinueK, 80, 0.1f, 1, true);
            NothingA = new Animation(Ressource.NothingKA, 80, 0.4f, 1, true);
            RunningA = new Animation(Ressource.RunningKA, 80, 0.1f, 1, true);
            Lauching = new Animation(Ressource.LauchingK, 80, 0.3f, 1, false);
            AirLunch = new Animation(Ressource.AirLunchK, 80, 0.1f, 1, false);
            NothingT = new Animation(Ressource.NothingKT, 80, 0.4f,1, true);
            WalkingT = new Animation(Ressource.WalkingKT, 80, 0.1f, 1, true);
            AttackT = new Animation(Ressource.AttackKT, 90, 0.1f, 1f, true);
            Transforming = new Animation(Ressource.TransformingK, 80, 0.1f, 1, true);
            APJoueur = new AnimationPlayer();
            APAir = new AnimationPlayer();
            Vie = new BarreDeVie(new Vector2(Position.X, Position.Y), 100);
        }

        public void Update(List<OBJCollisionable> Obj)
        {
            RecPerso = new Rectangle((int)Position.X - 40, (int)Position.Y - 80, 80, 80);

            //Collision avec les obstacles
            if (Obj != null) foreach (OBJCollisionable O in Obj) O.Update(this);

            Vie.Update(this);

            Deplacement = false;

            Position += Speed;

            #region Controle
            //Peut se déplacer si pas entrain de respirer
            if (!Respire || !Transformation)
            {
                if (KeyboardHelper.KeyHold(Keys.Up) || KeyboardHelper.KeyHold(Keys.W)) { Position.Y -= 2; RecPerso.Y -= 4; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Down) || KeyboardHelper.KeyHold(Keys.S)) { Position.Y += 2; RecPerso.Y += 4; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Right) || KeyboardHelper.KeyHold(Keys.D)) { Position.X += 2; RecPerso.X += 4; flip = SpriteEffects.None; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Left) || KeyboardHelper.KeyHold(Keys.A)) { Position.X -= 2; RecPerso.X -= 4; flip = SpriteEffects.FlipHorizontally; Deplacement = true; }

                //Deplacement de l'air
                if (EnnemiIn) { if (flipAir == SpriteEffects.None) PositionAir.X+=3; else PositionAir.X-=3; }
            }
            #endregion

            #region GRAVITÉ Apres les saut et roulades
            if (Gravity)
            {
                float i = 1;
                Speed.Y += 0.15f * i;
                if (Speed.Y >= 3) { Speed.Y=0; Gravity = false; JumpOne = false; }
            }
#endregion

            #region Animation Selon les touche

            if (Deplacement) // si en deplacement
            {
                if (!Transform) // si n'est pas transformé
                {
                    if (!EnnemiIn) //Si l'ennemi n'est pas avalé
                    {
                        if (Respire) { if (RespireContinue) APJoueur.PlayAnimation(AspirationContinue); else APJoueur.PlayAnimation(Aspiration); }

                        else if (Jump) APJoueur.PlayAnimation(Jumping);

                        else APJoueur.PlayAnimation(Running);
                    }
                    else // Si l'ennemi est avalé
                    {
                        if (Transformation) APJoueur.PlayAnimation(Transforming);

                        else if (Launch) { APJoueur.PlayAnimation(Lauching); APAir.PlayAnimation(AirLunch); }

                        else APJoueur.PlayAnimation(RunningA);
                    }
                }
                else //Si transformé
                {
                    if (Attack) APJoueur.PlayAnimation(AttackT);
                    else if (Launch) { APJoueur.PlayAnimation(Lauching); APAir.PlayAnimation(AirLunch); }
                    else  APJoueur.PlayAnimation(WalkingT);
                }
            }
            else
            {
                if (!Transform)//si pas transformé
                {
                    if (!EnnemiIn)//Si l'ennemi n'est pas avalé
                    {
                        if (Respire) { if (RespireContinue) APJoueur.PlayAnimation(AspirationContinue); else APJoueur.PlayAnimation(Aspiration); }

                        else if (Jump) APJoueur.PlayAnimation(Jumping);

                        else APJoueur.PlayAnimation(Nothing);
                    }

                    else // si l'ennemi est avalé
                    {
                        if (Transformation) APJoueur.PlayAnimation(Transforming);

                        else if (Launch) { APJoueur.PlayAnimation(Lauching); APAir.PlayAnimation(AirLunch); }

                        else APJoueur.PlayAnimation(NothingA);
                    }
                }

                else // si transformé
                {
                    if (Attack) APJoueur.PlayAnimation(AttackT);
                    else if (Launch) { APJoueur.PlayAnimation(Lauching); APAir.PlayAnimation(AirLunch); }
                    else APJoueur.PlayAnimation(NothingT);
                }
            }
#endregion

            //Saut Et Attack
            #region Saut/Attack
            if (KeyboardHelper.KeyHold(Keys.Space))
            {
                if (!EnnemiIn) Jump = true;
                else Transformation = true;

                if (Transform) Attack = true;

                if (!JumpOne && !Transform && !Transformation)
                {
                    //Saut avec gravité
                    Gravity = true; Position.Y -= 2;
                    Speed.Y = -3;
                    JumpOne = true;
                }
            }
#endregion

           // if (KeyboardHelper.KeyPressed(Keys.I)) { if (EnnemiIn)EnnemiIn = false; else EnnemiIn = true; }

            // Activation de l'absorption et Tir
            #region Absorption et tir 

            //Absorption
            if (KeyboardHelper.KeyHold(Keys.R) && !EnnemiIn)
            {
                Respire = true;
                Jump = false;
            }
            else
            {
                if(KeyboardHelper.KeyPressed(Keys.R))
                {
                      //Si a avalé un ennemi
                    if (EnnemiIn || Transform)
                    {
                        Launch = true;

                        //Regarde le jet d'air une fois
                        if (!Tir)
                        {
                            Tir = true;
                            if (flip == SpriteEffects.None) PositionAir = new Vector2(Position.X + 40, Position.Y);
                            else PositionAir = new Vector2(Position.X - 40, Position.Y);
                            flipAir = flip;
                        }
                    }
                }

                //Relachement de la respiration
                Respire = false;
                RespireContinue = false;
            }
            #endregion

            #region Deplacement du rectangle de l'air
            if (Tir)
            {
                RecAir = new Rectangle((int)PositionAir.X, (int)PositionAir.Y, 50, 50);

                //Deplacement
                if (flipAir == SpriteEffects.None) PositionAir.X += 3f; else PositionAir.X -= 3f;
            }
            #endregion

            // Arret des animation
            #region Arret
            if (APJoueur.Animation == Jumping && APJoueur.FrameIndex == 10) Jump = false;
            if (APJoueur.Animation == Aspiration && APJoueur.FrameIndex == 2) RespireContinue = true;
            if (APJoueur.Animation == Lauching && APJoueur.FrameIndex == 2) { Launch = false; EnnemiIn = false; APAir.PlayAnimation(null); Tir = false; Transform = false; Transformation = false; }
            if (APJoueur.Animation == Transforming && APJoueur.FrameIndex == 4) { Transform = true; Transformation = false; }
            if (APJoueur.Animation == AttackT && APJoueur.FrameIndex == 8) Attack = false;
            #endregion
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
           // g.Draw(Ressource.ImagePerso, RecAir, Color.Black);
            APJoueur.Draw(gametime, g, Position, flip);
            APAir.Draw(gametime, g, PositionAir, flipAir);
            Vie.Draw(g);
        }
    }
}
