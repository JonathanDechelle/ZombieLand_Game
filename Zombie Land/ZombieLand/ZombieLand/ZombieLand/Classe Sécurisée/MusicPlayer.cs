using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace ZombieLand
{
    class MusicPlayer
    {
        Song[] Songs;
        float TimerSong;
        int Track;
        bool ArretSpecial;

        public MusicPlayer(Song[]Songs)
        {
            this.Songs = Songs;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
        }


        public void Load(int NumSong)
        {
            MediaPlayer.Play(Songs[NumSong - 1]);
            Track = NumSong - 1;
        }

        public void Update(GameTime gameTime)
        {
            TimerSong += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TimerSong >= MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds || ArretSpecial)
            {
                Track++;
                if (Track == Songs.Length) Track = 0;
                MediaPlayer.Play(Songs[Track]);
                TimerSong = 0;
                GestionExterne.numSong = Track + 1;
                ArretSpecial=false;
            }

            if (MyGameLibrairy.KeyboardHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.P)) ArretSpecial = true;
        }


    }
}
