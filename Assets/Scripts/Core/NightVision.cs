﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core {
    [RequireComponent(typeof(Volume))]
    public class NightVision : MonoBehaviour {

        private Volume _volume;
        private bool _nightVisionEnabled;
        private void FixedUpdate() {
            _volume.weight += _nightVisionEnabled ? 0.005f : -1f;
            _volume.weight = Mathf.Clamp(_volume.weight, 0, 1);

            // gradually reduce ambient night vision light
            RenderSettings.ambientIntensity =
                _nightVisionEnabled
                    ? RenderSettings.ambientIntensity >= Game.Instance.LoadedLevelData.environment.NightVisionAmbientLight
                        ? RenderSettings.ambientIntensity * 0.99f
                        : Game.Instance.LoadedLevelData.environment.NightVisionAmbientLight
                    : 0;
        }

        private void OnEnable() {
            _volume = GetComponent<Volume>();
        }

        public void SetNightVisionActive(bool isActive) {
            _nightVisionEnabled = isActive;
            if (isActive) _volume.weight = 0.5f;
            RenderSettings.ambientIntensity = isActive ? Game.Instance.LoadedLevelData.environment.NightVisionAmbientLight * 4 : 0;
        }

        public void SetNightVisionColor(Color nightVisionColor) {
            _volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments);
            colorAdjustments.colorFilter.value = nightVisionColor;
        }
    }
}