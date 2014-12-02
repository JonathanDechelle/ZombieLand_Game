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
    public class Ninja
    {
        Animation Nothing, Running,Jumping,JumpingAttack,Degaine,RunningSword,NothingSword,Attack1,Attack2,Attack3;
        AnimationPlayer APJoueur;

        Vector2 Position;
        Rectangle RecPerso;
        SpriteEffects flip;
        BarreDeVie Vie;

        bool Hurt,Deplacement,Jump,Attack,PrepSword,HaveSword;
        int NumCombo;

        public Vector2 position { get { return Position; } set { Position = value; } }

        public Rectangle recPerso { get { return RecPerso; } set { RecPerso = value; } }

        public bool attack { get { return Attack; } }

        public SpriteEffects Flip { get { return flip; } }

        public bool hurt { get { return Hurt; } set { Hurt = value; } }

        public BarreDeVie vie { get { return Vie; } }

        public Ninja(Vector2 Position)
        {
            this.Position = Position;
        }

        public void Load()
        {
            Nothing = new Animation(Ressource.Nothing, 80, 0.4f, 1, true);
            Running = new Animation(Ressource.Running, 80, 0.1f, 1, true);
            Jumping = new Animation(Ressource.Jumping, 110, 0.1f, 1, false);
            JumpingAttack=new Animation(Ressource.JumpingAttack,80,0.1f,1,true);
            Degaine = new Animation(Ressource.Degaine, 100, 0.1f, 1, true);
            RunningSword = new Animation(Ressource.RunningSword, 90, 0.1f, 1, true);
            NothingSword = new Animation(Ressource.NothingSword, 80, 0.4f, 1, true);
            Attack1 = new Animation(Ressource.Attack1, 80, 0.1f, 1, true);
            Attack2 = new Animation(Ressource.Attack2, 80, 0.1f, 1, true);
            Attack3 = new Animation(Ressource.Attack3, 100, 0.1f, 1, true);
            APJoueur = new AnimationPlayer();
            Vie = new BarreDeVie(new Vector2(Position.X,Position.Y), 100);
        }

        public void Update(List<OBJCollisionable>Obj)
        {
            RecPerso = new Rectangle((int)Position.X - 40, (int)Position.Y - 80, 80, 80);

            //Collision avec les obstacles
            if (Obj != null) foreach (OBJCollisionable O in Obj) O.Update(this);

            Vie.Update(this);
          
                Deplacement = false;
                #region Controle
                if (KeyboardHelper.KeyHold(Keys.Up) || KeyboardHelper.KeyHold(Keys.W)) { Position.Y -= 2; RecPerso.Y -= 4; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Down) || KeyboardHelper.KeyHold(Keys.S)) { Position.Y += 2; RecPerso.Y += 4; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Right) || KeyboardHelper.KeyHold(Keys.D)) { Position.X += 2; RecPerso.X += 4; flip = SpriteEffects.None; Deplacement = true; }
                if (KeyboardHelper.KeyHold(Keys.Left) || KeyboardHelper.KeyHold(Keys.A)) { Position.X -= 2; RecPerso.X -= 4; flip = SpriteEffects.FlipHorizontally; Deplacement = true; }
                #endregion
            

            #region Animation Selon les touche

            if (Deplacement)
            {
                if (HaveSword)
                {
                    if (Attack)
                    {
                        switch (NumCombo)
                        {
                            case 0: APJoueur.PlayAnimation(Attack1);
                                break;
                            case 1: APJoueur.PlayAnimation(Attack2);
                                break;
                            case 2: APJoueur.PlayAnimation(Attack3);
                                break;
                        }
                    }
                    else if (!PrepSword) APJoueur.PlayAnimation(RunningSword);
                }
                else
                {
                    if (Jump)
                    {
                        if (Attack) APJoueur.PlayAnimation(JumpingAttack);
                        else APJoueur.PlayAnimation(Jumping);
                    }
                    else
                    {
                        if (!PrepSword) APJoueur.PlayAnimation(Running);
                    }
                }
            }

            else
            {
                if (HaveSword)
                {
                    if (Attack)
                    {
                        switch (NumCombo)
                        {
                            case 0: APJoueur.PlayAnimation(Attack1);
                                break;
                            case 1: APJoueur.PlayAnimation(Attack2);
                                break;
                            case 2: APJoueur.PlayAnimation(Attack3);
                                break;
                        }
                      
                    }
                    else if (!PrepSword) APJoueur.PlayAnimation(NothingSword);
                }
                else
                {

                    if (Jump)
                    {
                        if (Attack) APJoueur.PlayAnimation(JumpingAttack);
                        else APJoueur.PlayAnimation(Jumping);
                    }
                    else
                    {
                        if (!PrepSword) APJoueur.PlayAnimation(Nothing);
                    }
                }
            }

            //Saut Et Attack
            if (KeyboardHelper.KeyPressed(Keys.Space))
            {
                if (!PrepSword)
                {
                    if (!HaveSword) { if (Jump) Attack = true; Jump = true; }
                    else
                    {
                        if (APJoueur.Animation == NothingSword) Ressource.BruitEpee.Play();

                        Attack = true; Jump = false;

                        //Combo
                        if (APJoueur.Animation == Attack1)
                        {
                          if (APJoueur.FrameIndex == 7 || APJoueur.FrameIndex == 8)
                            {
                                Ressource.BruitEpee.Play();
                                NumCombo++;
                            }
                        }
                        else if (APJoueur.Animation == Attack2)
                        {
                            if (APJoueur.FrameIndex == 6 || APJoueur.FrameIndex == 7)
                            {
                                Ressource.BruitEpee.Play();
                                NumCombo++;
                            }
                        }
                        
                        if (NumCombo > 3) NumCombo = 3;
                    } 
                }
            }

   
            // Activation de l'épee 
            if (KeyboardHelper.KeyPressed(Keys.R))
            {
                if (HaveSword) HaveSword = false;
                else HaveSword = true;
                APJoueur.PlayAnimation(Degaine); 
                PrepSword = true;
                Jump = false;
            }

            // Arret des animatiom
            if (APJoueur.Animation == Jumping && APJoueur.FrameIndex == 11) Jump = false;
            if (APJoueur.Animation == JumpingAttack && APJoueur.FrameIndex == 6) { Attack = false; Jump = false; }
            if (APJoueur.Animation == Degaine && APJoueur.FrameIndex == 10) { PrepSword = false; }
            if (APJoueur.Animation == Attack1 && APJoueur.FrameIndex == 8 && NumCombo==0)
            { Attack = false; NumCombo = 0; }
            if (APJoueur.Animation == Attack2 && APJoueur.FrameIndex == 7 && NumCombo==1)
            { Attack = false; NumCombo = 0; }
            if (APJoueur.Animation == Attack3 && APJoueur.FrameIndex == 9 && NumCombo == 2)
            { Attack = false; NumCombo = 0; }
            #endregion
        }

        public void Draw(GameTime gametime, SpriteBatch g)
        {
            APJoueur.Draw(gametime, g, Position, flip);
            Vie.Draw(g);
        }
    }
}
