using Core.ShipModel.Modifiers.Water;
using NaughtyAttributes;
using UnityEngine;

namespace Core.ShipModel.Modifiers.GravityField {
    // Controller of boost modifier object, used to set the various params and serialise etc
    [ExecuteAlways]
    public class ModifierGravityField : MonoBehaviour {
        [SerializeField] private ModifierGravityFieldAttractor modifierGravityFieldAttractor;
        [SerializeField] private ModifierGravityFieldThrust modifierGravityFieldThrust;


        [Range(1000, 50000)] [OnValueChanged("BoostLengthChanged")] [SerializeField]
        private float attractorRadiousMeters;
        private float attractorStrength;

        public float GravityFieldAttractorRadiousMeters {
            get => attractorRadiousMeters;
            set {
                attractorRadiousMeters = value;
                AttractorRadiousChanged();
            }
        }

        public float GravityFieldAttractorStrength {
            get => attractorStrength;
            set {
                attractorStrength = value;
                AttractorStrengthChanged();
            }
        }

        private void Awake() {
            attractorRadiousMeters = modifierGravityFieldAttractor.RadiousMeters;
        }

        private void OnEnable() {
            //modifierGraviThrust.UseDistortion = FindObjectOfType<ModifierWater>() == null;
        }

        private void AttractorRadiousChanged() {
            modifierGravityFieldAttractor.RadiousMeters = attractorRadiousMeters;
        }

        private void AttractorStrengthChanged() {
            modifierGravityFieldAttractor.AttractionStrength = attractorStrength;
        }
    }
}