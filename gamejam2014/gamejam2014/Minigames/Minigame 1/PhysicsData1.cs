﻿using System;
using System.Collections.Generic;
using Utilities.Math;
using Utilities.Math.Shape;

namespace gamejam2014.Minigames.Minigame_1
{
    public static class PhysicsData1
    {
        public static float GoodBacteriaMass = 0.2f,
                            InfectedBacteriaMass = 0.1f;
        public static float BacteriaMassGiveToPlayerScale = 1.0f;

        public static float SpikePowerScale = 1.5f;
        public static float SpikePowerLength = 8.0f;

        private static float AuraDist = 5.0f;
        public static float GetAuraDist(float zoomScale) { return zoomScale * AuraDist; }
        public static float AuraPowerLength = 5.0f;

        public static float BacteriaInfectEnergyDamp = 0.1f;

        public static float BacteriaLifeLength = 30.0f;

        public static float BacteriaAppearanceTime = 2.5f;
        public static float BacteriaDriftMaxSpeed = 300.0f;
        public static float GetBacteriaDriftMaxSpeed(float zoomScale) { return BacteriaDriftMaxSpeed * zoomScale; }
    }
}