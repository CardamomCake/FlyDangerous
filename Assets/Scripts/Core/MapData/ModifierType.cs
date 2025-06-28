using System.Collections.Generic;
using Misc;

namespace Core.MapData {
    public class ModifierType : IFdEnum {
        private static int _id;

        public static readonly ModifierData BoostModifierData = new BoostModifierData { Name = "Boost", BoostLengthMeters = 300 };
        public static readonly ModifierData Boost2ModifierData = new Boost2ModifierData { Name = "Boost2", Boost2LengthMeters = 300 };
        public static readonly ModifierData GravityFieldModifierData = new GravityFieldModifierData { Name = "GravityField", AttractorRadiousMeters = 300,AttractorStrength = 10000 };

        public static readonly ModifierType BoostModifierType = new(BoostModifierData);
        public static readonly ModifierType Boost2ModifierType = new(Boost2ModifierData);
        public static readonly ModifierType GravityFieldModifierType = new(GravityFieldModifierData);

        private ModifierType(ModifierData modifierData) {
            Id = GenerateId;
            ModifierData = modifierData;
        }

        private static int GenerateId => _id++;
        public int Id { get; }
        public string Name => ModifierData.Name;
        public ModifierData ModifierData { get; }

        public static IEnumerable<ModifierType> List() {
            return new[] {
                BoostModifierType,
                Boost2ModifierType,
                GravityFieldModifierType
            };
        }

        public static ModifierType FromString(string modifierName) {
            return FdEnum.FromString(List(), modifierName);
        }

        public static ModifierType FromId(int id) {
            return FdEnum.FromId(List(), id);
        }
    }
}