using System;
using System.Collections.Generic;
using System.Linq;
using Core.MapData;
using Core.MapData.Serializable;
using Core.ShipModel.Modifiers.Boost;
using Core.ShipModel.Modifiers.Boost2;
using Core.ShipModel.Modifiers.GravityField;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

namespace Gameplay {
    public class ModifierSpawner : MonoBehaviour {
        [Dropdown("GetModifierTypes")] [OnValueChanged("RefreshFromModifierData")] [SerializeField]
        private string modifierType;

        [SerializeField] private ModifierBoost modifierBoost;
        [SerializeField] private ModifierBoost2 modifierBoost2;
        [SerializeField] private ModifierGravityField modifierGravityField;

        [ShowIf("ShowBoostAttributes")] [Range(1000, 50000)] [OnValueChanged("SetModifierAttributes")] [SerializeField]
        private float boostTrailLength = 15000;
        private float boost2TrailLength = 15000;

        private ModifierData _modifierData;

        public float BoostTrailLength => boostTrailLength;
        public float Boost2TrailLength => boost2TrailLength;

        public ModifierData ModifierData {
            get {
                if (_modifierData == null) _modifierData = ModifierType.FromString(modifierType).ModifierData;
                return _modifierData;
            }
            private set {
                _modifierData = value;
                modifierType = ModifierType.FromString(_modifierData.Name).Name;
                Debug.Log(_modifierData.Name);
                Debug.Log(modifierType);

                if (_modifierData is BoostModifierData boostModifierData) boostTrailLength = boostModifierData.BoostLengthMeters;
                if (_modifierData is Boost2ModifierData boost2ModifierData) boost2TrailLength = boost2ModifierData.Boost2LengthMeters;

                ResetAll();
            }
        }

        public void Deserialize(SerializableModifier serializedData) {
            var serializedModifierType = ModifierType.FromString(serializedData.type);
            ModifierData = serializedModifierType.ModifierData;

            var modifierTransform = transform;
            modifierTransform.position = serializedData.position.ToVector3();
            modifierTransform.rotation = Quaternion.Euler(serializedData.rotation.ToVector3());

            // overrides
            if (ModifierData is BoostModifierData boostModifierData)
                boostTrailLength = serializedData.boostTrailLengthMeters != null &&
                                   Math.Abs(serializedData.boostTrailLengthMeters.Value - boostModifierData.BoostLengthMeters) > 0.01f
                    ? serializedData.boostTrailLengthMeters.Value
                    : boostModifierData.BoostLengthMeters;

            if (ModifierData is Boost2ModifierData boost2ModifierData)
                boostTrailLength = serializedData.boost2TrailLengthMeters != null &&
                                   Math.Abs(serializedData.boost2TrailLengthMeters.Value - boost2ModifierData.Boost2LengthMeters) > 0.01f
                    ? serializedData.boost2TrailLengthMeters.Value
                    : boost2ModifierData.Boost2LengthMeters;

            ResetAll();
            SetModifierAttributes();
        }

        [UsedImplicitly]
        private List<string> GetModifierTypes() {
            return ModifierType.List().Select(b => b.Name).ToList();
        }

        [UsedImplicitly]
        private void RefreshFromModifierData() {
            ModifierData = ModifierType.FromString(modifierType).ModifierData;
            ResetAll();
        }

        [UsedImplicitly]
        private bool ShowBoostAttributes() {
            return modifierType.Equals(ModifierType.BoostModifierType.Name);
        }

        [Button("Reset to type default")]
        private void ResetAll() {
            modifierBoost.gameObject.SetActive(false);
            //modifierBoost2.gameObject.SetActive(false);
            switch (modifierType) {
                case "Boost":
                    modifierBoost.gameObject.SetActive(true);
                    break;
                case "Boost2":
                 //   modifierBoost2.gameObject.SetActive(true);
                    break;
            }
        }

        private void SetModifierAttributes() {
            switch (modifierType) {
                case "Boost":
                    modifierBoost.BoostStreamLengthMeters = boostTrailLength;
                    break;
                case "Boost2":
                    //modifierBoost2.Boost2StreamLengthMeters = boostTrailLength;
                    break;
            }
        }
    }
}