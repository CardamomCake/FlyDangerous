using Core.ShipModel.Modifiers.Water;
using NaughtyAttributes;
using UnityEngine;

namespace Core.ShipModel.Modifiers.Boost2 {
    // Controller of boost2 modifier object, used to set the various params and serialise etc
    [ExecuteAlways]
    public class ModifierBoost2 : MonoBehaviour {
        [SerializeField] private ModifierBoost2Attractor modifierBoost2Attractor;
        [SerializeField] private ModifierBoost2Thrust modifierBoost2Thrust;
        [SerializeField] private ModifierBoost2Stream modifierBoost2Stream;

        [Range(1000, 50000)] [OnValueChanged("Boost2LengthChanged")] [SerializeField]
        private float boost2StreamLengthMeters;

        public float Boost2StreamLengthMeters {
            get => boost2StreamLengthMeters;
            set {
                boost2StreamLengthMeters = value;
                Boost2LengthChanged();
            }
        }

        private void Awake() {
            boost2StreamLengthMeters = modifierBoost2Stream.TrailLengthMeters;
        }

        private void OnEnable() {
            modifierBoost2Thrust.UseDistortion = FindObjectOfType<ModifierWater>() == null;
        }

        private void Boost2LengthChanged() {
            modifierBoost2Stream.TrailLengthMeters = boost2StreamLengthMeters;
        }
    }
}