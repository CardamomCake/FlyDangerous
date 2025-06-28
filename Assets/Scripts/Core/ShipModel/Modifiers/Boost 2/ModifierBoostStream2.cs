using Core.Player;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.ShipModel.Modifiers.Boost2 {
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(VisualEffect))]
    public class ModifierBoost2Stream : MonoBehaviour, IModifier {
        [SerializeField] private float shipForceAdd = 200000;
        [SerializeField] private float shipSpeedAdd = 15;
        [SerializeField] private float shipThrustAdd = 5000;

        [SerializeField] private float lengthMeters = 15000;
        [SerializeField] private float streamCapsuleEndCapRadius = 500;
        [SerializeField] private Transform terrainGenEndpointMarker;

        private CapsuleCollider _capsuleCollider;
        private VisualEffect _visualEffect;

        public float TrailLengthMeters {
            get => lengthMeters;
            set {
                var scale = transform.lossyScale.z;
                var capsuleCollider = GetComponent<CapsuleCollider>();
                var vfx = GetComponent<VisualEffect>();

                lengthMeters = value;
                var lengthLocal = lengthMeters / scale;

                capsuleCollider.height = lengthLocal;
                capsuleCollider.center = new Vector3(0, 0, lengthLocal / 2 - capsuleCollider.radius);
                vfx.SetFloat("_streamLength", lengthLocal);
                terrainGenEndpointMarker.localPosition = new Vector3(0, 0, lengthLocal);
            }
        }

        public void ApplyModifierEffect(Rigidbody shipRigidBody, ref AppliedEffects effects) {
            var streamTransform = transform;

            // apply force along the funnel with force linear equivalent to the distance from the source
            var streamPosition = streamTransform.position;
            var shipPosition = shipRigidBody.transform.position;
            var distance = streamPosition - shipPosition;
            var effectOverDistanceNormalised = 1 - distance.magnitude / (lengthMeters - streamCapsuleEndCapRadius);

            var parameters = shipRigidBody.gameObject.GetComponent<ShipPlayer>().ShipPhysics.FlightParameters;
            var massNorm = parameters.mass / ShipParameters.Defaults.mass;

            effects.shipForce += Vector3.Lerp(Vector3.zero, massNorm * shipForceAdd * streamTransform.forward, effectOverDistanceNormalised);

            // apply additional thrust and max speed if the ship vector is facing the correct direction
            if (Vector3.Dot(transform.forward, shipRigidBody.velocity) > 0) {
                effects.shipDeltaSpeedCap += Mathf.Lerp(0, shipSpeedAdd * parameters.boosterVelocityMultiplier, effectOverDistanceNormalised);
                effects.shipDeltaThrust += Mathf.Lerp(0, shipThrustAdd * parameters.boosterThrustMultiplier * massNorm, effectOverDistanceNormalised);
            }
        }

        public void ApplyInitialEffect(Rigidbody ship, ref AppliedEffects effects) {}

        // for some reason the unity editor insists on resetting the vfx which makes building levels a PITA
        public void OnGUI() {
            TrailLengthMeters = lengthMeters;
        }
    }
}