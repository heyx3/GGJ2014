using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace gamejam2014.Minigames.Minigame_5
{
    public static class SoundAssets5
    {
        public static List<SoundEffect> Collisions = new List<SoundEffect>(),
                                        ProjectileHits = new List<SoundEffect>();
        public static SoundEffect ProjectileEarth, ProjectileSaturn, SunExplode;

        public static void PlayRandomCollision()
        {
            Collisions[Utilities.Math.UsefulMath.Random(0, Collisions.Count - 1)].Play();
        }
        public static void PlayRandomProjectileHit()
        {
            ProjectileHits[Utilities.Math.UsefulMath.Random(0, ProjectileHits.Count - 1)].Play();
        }


        public static void Initialize(ContentManager content)
        {
            for (int i = 0; i < 3; ++i)
            {
                Collisions.Add(content.Load<SoundEffect>("Sound/Z5/Collision_" + (i + 1).ToString()));
            }
            for (int i = 0; i < 2; ++i)
            {
                ProjectileHits.Add(content.Load<SoundEffect>("Sound/Z5/Projectile_Hit_" + (i + 1).ToString()));
            }

            ProjectileEarth = content.Load<SoundEffect>("Sound/Z5/Projectile_Earth");
            ProjectileSaturn = content.Load<SoundEffect>("Sound/Z5/Projectile_Saturn");
            SunExplode = content.Load<SoundEffect>("Sound/Z5/Sun_Exploding");
        }
    }
}
