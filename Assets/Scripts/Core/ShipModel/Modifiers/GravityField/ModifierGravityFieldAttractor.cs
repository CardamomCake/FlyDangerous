using Core.Player;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.ShipModel.Modifiers.GravityField {
    public class ModifierGravityFieldAttractor : MonoBehaviour, IModifier {
        [SerializeField] private float radiousMeters = 50000;
        [SerializeField] private float attractionStrength = 5000000;
        public void ApplyModifierEffect(Rigidbody ship, ref AppliedEffects effects) {
            var distance = transform.position - ship.transform.position;
            var parameters = ship.gameObject.GetComponent<ShipPlayer>().ShipPhysics.FlightParameters;
            var massNorm = parameters.mass / ShipParameters.Defaults.mass;
            effects.shipForce += Vector3.Lerp(Vector3.zero, massNorm * distance.normalized * attractionStrength, 1 - distance.magnitude / radiousMeters);    
        }

        public float RadiousMeters {
            get => radiousMeters;
            set {
                //var scale = transform.lossyScale.z;
                //var capsuleCollider = GetComponent<CapsuleCollider>();
                //var vfx = GetComponent<VisualEffect>();

                radiousMeters = value;
                //var lengthLocal = radiousMeters / scale;

                //capsuleCollider.height = lengthLocal;
                //capsuleCollider.center = new Vector3(0, 0, lengthLocal / 2 - capsuleCollider.radius);
                //vfx.SetFloat("_streamLength", lengthLocal);
                //terrainGenEndpointMarker.localPosition = new Vector3(0, 0, lengthLocal);
            }
        }

        public float AttractionStrength {
            get => attractionStrength;
            set {
                //var scale = transform.lossyScale.z;
                //var capsuleCollider = GetComponent<CapsuleCollider>();
                //var vfx = GetComponent<VisualEffect>();

                attractionStrength = value;
                //var lengthLocal = radiousMeters / scale;

                //capsuleCollider.height = lengthLocal;
                //capsuleCollider.center = new Vector3(0, 0, lengthLocal / 2 - capsuleCollider.radius);
                //vfx.SetFloat("_streamLength", lengthLocal);
                //terrainGenEndpointMarker.localPosition = new Vector3(0, 0, lengthLocal);
            }
        }

        public void ApplyInitialEffect(Rigidbody ship, ref AppliedEffects effects) {}
    }
}