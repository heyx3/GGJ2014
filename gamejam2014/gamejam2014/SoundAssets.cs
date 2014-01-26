using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace gamejam2014
{
    /// <summary>
    /// All the sound assets used in the game.
    /// </summary>
    public static class SoundAssets
    {
        public static float LevelMusicVolume = 1.0f;

        public static Dictionary<ZoomLevels, SoundEffectInstance> LevelMusic = new Dictionary<ZoomLevels, SoundEffectInstance>()
        {
            { ZoomLevels.One, null },
            { ZoomLevels.Two, null },
            { ZoomLevels.Three, null },
            { ZoomLevels.Four, null },
            { ZoomLevels.Five, null },
        };
        public static void SwitchZoomMusic(ZoomLevels newZoom)
        {
            foreach (ZoomLevels zoom in WorldData.AscendingZooms) if (LevelMusic[zoom] != null)
            {
                if (zoom == newZoom) LevelMusic[zoom].Volume = LevelMusicVolume;
                else LevelMusic[zoom].Volume = 0.0f;
            }
        }

        public static void Initialize(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            LevelMusic[ZoomLevels.One] = content.Load<SoundEffect>("Sound/Level_Amoeba_v3").CreateInstance();
            LevelMusic[ZoomLevels.Three] = content.Load<SoundEffect>("Sound/Level_Dog").CreateInstance();
            LevelMusic[ZoomLevels.Five] = content.Load<SoundEffect>("Sound/Level_Planet_v2").CreateInstance();

            foreach (SoundEffectInstance inst in LevelMusic.Values)
            {
                if (inst != null)
                    inst.Play();
            }

            SwitchZoomMusic(ZoomLevels.Three);
        }
    }
}
