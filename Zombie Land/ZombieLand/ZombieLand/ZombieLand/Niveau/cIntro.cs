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
    class cIntro:GameScreen
    {
        Animation GrandMere, Explosion;
                
        AnimationPlayer GMPlayer, OtherPlayer;
        Vector2 PositionGMLF;
        List<FireBall> FireBalls;
        ZLPlayer Player;
        bool ArretExplosion;
       
        public cIntro(IServiceProvider serviceProvider, GraphicsDeviceManager graphics)
            : base(serviceProvider, graphics)
        {
            Player = new ZLPlayer(new Vector2(-100, 300), true, false);
            GMPlayer = new AnimationPlayer();
            GrandMere = new Animation(Ressources.GrandMa, 150, 0.2f, 1, true);
            Explosion = new Animation(Ressources.Explosion, 100, 0.2f, 1.5f, false);
            PositionGMLF = new Vector2(700, 300);
            FireBalls = new List<FireBall>();
            FireBalls.Add(new FireBall(new Vector2(-100, 280), SpriteEffects.None));

            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Ressources.CHSong);
        }

        public override void Load()
        {
            Player.Load(1);
           GestionExterne.argent = 200;
            GestionExterne.argent = 100000;
            GestionExterne.nbBalleGun = 40;
            GestionExterne.nbFleche = 2;
            GestionExterne.nivGun = 1;
            GestionExterne.nivShotgun = 1;
            GestionExterne.numSong = 1;
            
        }

        public override void Update(GameTime gameTime)
        {
            #region AvantMenu
            if (!ArretExplosion)
            {
                //-----------------Bloc animation Balle de feu dans auto
                if (FireBalls[0].position.X > PositionGMLF.X)
                {
                    GMPlayer.PlayAnimation(null);
                    OtherPlayer.PlayAnimation(Explosion);
                    FireBalls[0].invisible = true;

                    if (OtherPlayer.FrameIndex == 5)
                    {
                        ArretExplosion = true;
                        PositionGMLF.X = -100;
                    }
                }
                else
                {
                    GMPlayer.PlayAnimation(GrandMere);
                    FireBalls[0].Update();
                    OtherPlayer.PlayAnimation(null);
                    PositionGMLF.X -= 3;
                }
                //--------------------------------(Fin Bloc Anim feu dans Auto)
            }
            else
            {
                //Animation SkateBoard
                if (Player.position.X < FireBalls[0].position.X - 200)
                {
                    Player.position = new Vector2(Player.position.X + 3, Player.position.Y);
                }
                else { FireBalls.RemoveAt(0); RemoveScreen(this); AddScreen(new cMainMenu(serviceProvider, GraphicsDeviceManager)); }
            }
            #endregion
        }

        public override void Draw(GameTime gametime, SpriteBatch g)
        {
         
               //Animation GrandMere Et FireBall
                GMPlayer.Draw(gametime, g, PositionGMLF, SpriteEffects.None);
                foreach(FireBall F in FireBalls) F.Draw(gametime, g);

                g.Draw(Ressources.TitreJeu, new Vector2(0, 0), Color.Green);
            

            //Others AnimationPlayer(joueur)
            OtherPlayer.Draw(gametime, g, PositionGMLF,SpriteEffects.None);
            
            Player.Draw(gametime, g);
        }
    }
}
