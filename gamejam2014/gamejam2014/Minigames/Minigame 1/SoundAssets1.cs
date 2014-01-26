using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace gamejam2014.Minigames.Minigame_1
{
    public static class SoundAssets1
    {
        public static List<SoundEffect> Hiss = new List<SoundEffect>();
        public static List<SoundEffect> Squish = new List<SoundEffect>();

        public static void PlayRandomHiss()
        {
            Hiss[Utilities.Math.UsefulMath.Random(0, Hiss.Count - 1)].CreateInstance().Play();
        }
        public static void PlayRandomSquish()
        {
            Squish[Utilities.Math.UsefulMath.Random(0, Squish.Count - 1)].CreateInstance().Play();
        }

        public static void Initialize(ContentManager content)
        {
            for (int i = 0; i < 3; ++i)
            {
                Hiss.Add(content.Load<SoundEffect>("Sound/Z1/Hiss_" + (i + 1).ToString()));
            }
            for (int i = 0; i < 4; ++i)
            {
                Squish.Add(content.Load<SoundEffect>("Sound/Z1/Squish_" + (i + 1).ToString()));
            }
        }
    }
}
