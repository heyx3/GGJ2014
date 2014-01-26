using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace gamejam2014.Minigames.Minigame_3
{
    public static class SoundAssets3
    {
        public static List<SoundEffect> Barks = new List<SoundEffect>(),
                                        Snarls = new List<SoundEffect>(),
                                        WoodHits = new List<SoundEffect>();
        public static SoundEffect BothBark;
        
        public static void PlayRandomBark()
        {
            Barks[Utilities.Math.UsefulMath.Random(0, Barks.Count - 1)].CreateInstance().Play();
        }
        public static void PlayRandomSnarl()
        {
            Snarls[Utilities.Math.UsefulMath.Random(0, Snarls.Count - 1)].CreateInstance().Play();
        }
        public static void PlayRandomWoodHit()
        {
            WoodHits[Utilities.Math.UsefulMath.Random(0, WoodHits.Count - 1)].CreateInstance().Play();
        }

        public static void Initialize(ContentManager content)
        {
            for (int i = 0; i < 3; ++i)
            {
                Barks.Add(content.Load<SoundEffect>("Sound/Z3/Bark_" + (i + 1).ToString()));
            }
            for (int i = 0; i < 2; ++i)
            {
                Snarls.Add(content.Load<SoundEffect>("Sound/Z3/Snarl_" + (i + 1).ToString()));
            }
            for (int i = 0; i < 2; ++i)
            {
                WoodHits.Add(content.Load<SoundEffect>("Sound/Z3/Wood_Hit_" + (i + 1).ToString()));
            }

            BothBark = content.Load<SoundEffect>("Sound/Z3/Both_Bark_1");
        }
    }
}
