using System;
using System.Linq;
using System.Collections.Generic;
using Utilities.Math;
using Microsoft.Xna.Framework.Graphics;
using gamejam2014;
using gamejam2014.Minigames;
using V2 = Microsoft.Xna.Framework.Vector2;

namespace gamejam2014.Minigames.Minigame_5
{
    public class Minigame5 : Minigame
    {
        public Jousting.Blocker BlackHole;
        public Utilities.Math.Shape.Circle BlackHoleCol { get { return (Utilities.Math.Shape.Circle)BlackHole.ColShape; } }

        public Minigame5(ZoomLevels zoom)
            : base(zoom)
        {

        }

        public void LaunchRing(V2 pos, V2 dir)
        {
            Jousting.Blocker ring = new Jousting.Blocker(new Utilities.Graphics.AnimatedSprite(ArtAssets5.Ring),
                                                         ArtAssets5.GetRingShape(pos, WorldData.ZoomScaleAmount[CurrentZoom]),
                                                         true, PhysicsData5.GetRingSpeed(WorldData.ZoomScaleAmount[CurrentZoom]));
            ring.Rotation = UsefulMath.FindRotation(dir);
            ring.Velocity = ring.MaxVelocity * dir;

            SoundAssets5.PlayRandomProjectileHit();

            ring.OnHitByJouster += (s, e) =>
                {
                    if (e.Enemy.ThisJouster == Jousting.Jousters.Dischord) return;

                    SoundAssets5.PlayRandomProjectileHit();

                    e.Enemy.Health -= PhysicsData5.RingDamage;
                    KillBlocker((Jousting.Blocker)s);
                };

            Blockers.Add(ring);
        }

        protected override void Reset()
        {
            Blockers.Add(new Jousting.Blocker(ArtAssets5.BlackHole, PhysicsData5.GetBlackHole(), true, 0.0f, 1.0f));
            Blockers[0].OnHitByJouster += (s, e) =>
            {
                e.Enemy.Velocity = WorldData.ZoomScaleAmount[World.CurrentZoom] * PhysicsData5.BlackHolePushBack * V2.Normalize(UsefulMath.FindDirection(e.Enemy.Pos, BlackHole.Pos, false));
                e.Enemy.Health -= PhysicsData5.BlackHoleDamage;

                SoundAssets5.SunExplode.Play();

                V2 fromSun = UsefulMath.FindDirection(BlackHole.Pos, e.Enemy.Pos);
                AdditiveParticles.Merge(ParticleAssets5.GetSunHitParticles(fromSun,
                                                                           BlackHole.Pos + (fromSun * BlackHoleCol.Radius),
                                                                           World.CurrentTime));
            };
            BlackHole = Blockers[0];

            Harmony.OnHurtEnemy += (s, e) =>
                {
                    SoundAssets5.PlayRandomCollision();
                    AdditiveParticles.Merge(ParticleAssets5.GetPlanetHitParticles((e.Enemy.Pos + Harmony.Pos) * 0.5f,
                                                                                  Utilities.Conversions.GetPerp(UsefulMath.FindDirection(e.Enemy.Pos, Harmony.Pos)), World.CurrentTime));
                };
            Harmony.OnHurtByEnemy += (s, e) =>
                {
                    SoundAssets5.PlayRandomCollision();
                    AdditiveParticles.Merge(ParticleAssets5.GetPlanetHitParticles((e.Enemy.Pos + Harmony.Pos) * 0.5f,
                                                                                  Utilities.Conversions.GetPerp(UsefulMath.FindDirection(e.Enemy.Pos, Harmony.Pos)), World.CurrentTime));
                };
        }

        protected override void Update(Jousting.Jouster.CollisionData playerCollision)
        {
            Utilities.Math.Shape.Circle blackHole = BlackHoleCol;
            V2 harmonyToBH = UsefulMath.FindDirection(Harmony.Pos, blackHole.Center, false);
            V2 dischordToBH = UsefulMath.FindDirection(Dischord.Pos, blackHole.Center, false);

            float scale = WorldData.ZoomScaleAmount[World.CurrentZoom];


            if (!Harmony.IsSpiky_Aura)
                Harmony.Acceleration = V2.Normalize(harmonyToBH) * PhysicsData5.GetBlackHolePull(harmonyToBH.Length(), scale);
            if (!Dischord.IsSpiky_Aura)
                Dischord.Acceleration = V2.Normalize(dischordToBH) * PhysicsData5.GetBlackHolePull(dischordToBH.Length(), scale);
            foreach (Jousting.Blocker blocker in Blockers)
            {
                blocker.Acceleration = UsefulMath.FindDirection(blocker.Pos, blackHole.Center) *
                                       PhysicsData5.GetBlackHolePull((blocker.Pos - blackHole.Center).Length(), scale);

            }

            //Keep all rings facing the right way.
            for (int i = 1; i < Blockers.Count; ++i)
            {
                Blockers[i].Rotation = UsefulMath.FindRotation(Blockers[i].Velocity);
            }


            //Update animation.
            ArtAssets5.NoGravOverlay.UpdateAnimation(World.CurrentTime);
            ArtAssets5.Ring.UpdateAnimation(World.CurrentTime);
        }

        protected override string GetHarmonyIntroString()
        {
            return "Destroy!";
        }
        protected override string GetDischordIntroString()
        {
            return "Destroy!";
        }

        protected override void OnMinigameEnd()
        {
            Harmony.Rotation = 0.0f;
            HarmonySprite.DrawArgs.Rotation = 0.0f;
        }

        public override void OnHarmonySpecial()
        {
            SoundAssets5.ProjectileEarth.CreateInstance().Play();
            Harmony.IsSpiky_Aura = true;

            Utilities.IntervalCounter ic = new Utilities.IntervalCounter(TimeSpan.FromSeconds(PhysicsData5.NoGravLength));
            ic.IntervalTrigger += (s, e) =>
                {
                    Harmony.IsSpiky_Aura = false;
                };
            World.Timers.AddTimerNextUpdate(ic, true);
        }
        public override void OnDischordSpecial()
        {
            LaunchRing(Dischord.Pos, UsefulMath.FindDirection(Dischord.Rotation));
        }

        protected override void DrawAbovePlayers(SpriteBatch sb)
        {
            bool first = (World.CurrentZoom == WorldData.ZoomIn(ZoomLevels.Five) && World.ZoomingIn) ||
                         (World.CurrentZoom == ZoomLevels.Five && World.ZoomingOut),
                 second = Harmony.IsSpiky_Aura;

            if (!first && !second) return;

            sb.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, World.CamTransform);

            if (first)
            {
                //Draw the rendered world on top of the globe.
                sb.Draw(World.RenderedWorldTex, Harmony.Pos, null, Microsoft.Xna.Framework.Color.White, 0.0f,
                        0.5f * new V2(World.RenderedWorldTex.Width, World.RenderedWorldTex.Height), 1.0f, SpriteEffects.None, 1.0f);
            }

            //Draw the overlay.
            if (second)
            {
                ArtAssets5.NoGravOverlay.DrawArgs.Scale *= WorldData.ZoomScaleAmount[ZoomLevels.Five];
                ArtAssets5.NoGravOverlay.Draw(Harmony.Pos, sb);
                ArtAssets5.NoGravOverlay.DrawArgs.Scale /= WorldData.ZoomScaleAmount[ZoomLevels.Five];
            }

            sb.End();
        }
    }
}
