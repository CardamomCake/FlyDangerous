namespace Core.MapData {
    public enum ModifierMode {
        Boost,
        Boost2,
        GravityField
    }

    public class ModifierData {
        public string Name { get; set; }
        public virtual ModifierMode ModifierMode { get; set; }
    }

    public class BoostModifierData : ModifierData {
        public override ModifierMode ModifierMode => ModifierMode.Boost;
        public float BoostLengthMeters { get; set; }
    }

    public class Boost2ModifierData : ModifierData {
        public override ModifierMode ModifierMode => ModifierMode.Boost2;
        public float Boost2LengthMeters { get; set; }
    }

    public class GravityFieldModifierData : ModifierData {
        public override ModifierMode ModifierMode => ModifierMode.GravityField;
        public float AttractorRadiousMeters { get; set; }
        public float AttractorStrength { get;set;}
    }
}