﻿using System.Collections.Generic;
using Misc;

namespace Core.MapData {
    public class Environment : IFdEnum {
        private static int _id;

        public static readonly Environment SunriseClear = new("Sunrise Clear", "Sunrise_Clear", 1.65f);
        public static readonly Environment SunriseHazy = new("Sunrise Hazy", "Sunrise_Hazy", 1.65f);
        public static readonly Environment NoonClear = new("Noon Clear", "Noon_Clear", 0.9f);
        public static readonly Environment NoonCloudy = new("Noon Cloudy", "Noon_Cloudy", 0.8f);
        public static readonly Environment NoonStormy = new("Noon Stormy", "Noon_Stormy", 0.8f);
        public static readonly Environment SunsetClear = new("Sunset Clear", "Sunset_Clear", 4);
        public static readonly Environment SunsetCloudy = new("Sunset Cloudy", "Sunset_Cloudy", 2.5f);
        public static readonly Environment SunsetStormy = new("Sunset Stormy", "Sunset_Stormy", 3);
        public static readonly Environment NightClear = new("Night Clear", "Night_Clear", 4);
        public static readonly Environment NightCloudy = new("Night Cloudy", "Night_Cloudy", 5);
        public static readonly Environment ClearLowAtmosphere = new("Clear, Low Atmosphere", "Low_Atmosphere_Clear", 3);
        public static readonly Environment PlanetOrbitBottom = new("Red Planet", "Planet_Orbit_Bottom", 2.5f);
        public static readonly Environment PlanetOrbitTop = new("Blue Planet", "Planet_Orbit_Top", 1.8f);
        public static readonly Environment RedBlueNebula = new("Red / Blue Nebula", "Red_Blue_Nebula", 2.8f);
        public static readonly Environment YellowGreenNebula = new("Yellow / Green Nebula", "Yellow_Green_Nebula", 3);
        public static readonly Environment RedPlanet = new("Red World", "Red_Planet", 1.5f);

        private Environment(string name, string sceneToLoad, float nightVisionAmbientLight) {
            Id = GenerateId;
            Name = name;
            SceneToLoad = sceneToLoad;
            NightVisionAmbientLight = nightVisionAmbientLight;
        }

        private static int GenerateId => _id++;
        public string SceneToLoad { get; }
        public float NightVisionAmbientLight { get; }

        public int Id { get; }
        public string Name { get; }

        public static IEnumerable<Environment> List() {
            return new[] {
                SunriseClear, SunriseHazy, NoonClear, NoonCloudy, NoonStormy, SunsetClear, SunsetCloudy, SunsetStormy,
                NightClear, NightCloudy, ClearLowAtmosphere,
                PlanetOrbitBottom, PlanetOrbitTop, RedBlueNebula, YellowGreenNebula, RedPlanet
            };
        }

        public static Environment FromString(string locationString) {
            return FdEnum.FromString(List(), locationString);
        }

        public static Environment FromId(int id) {
            return FdEnum.FromId(List(), id);
        }
    }
}