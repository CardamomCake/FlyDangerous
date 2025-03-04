using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FdUI {
    public class FdSlider : MonoBehaviour, IMoveHandler {
        [SerializeField] private InputField numberTextBox;
        [SerializeField] private Slider slider;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;

        [Tooltip("Set the slider increments from gamepad / keyboard input")] [SerializeField]
        private float sliderIncrements = 0.5f;

        [SerializeField] public UnityEvent<float> onValueChanged;


        public float Value {
            get => slider.value;
            set {
                UpdateSliderConstraints();
                slider.value = Math.Min(Math.Max(value, minValue), maxValue);

                // Apparently this doesn't trigger if the value is ONE?! SERIOUSLY? 
                OnSliderChanged();
            }
        }

        public void OnEnable() {
            UpdateSliderConstraints();
        }

        public void OnSliderChanged() {
            numberTextBox.text = slider.wholeNumbers || slider.value >= 100
                ? slider.value.ToString("0")
                : slider.value >= 10
                    ? slider.value.ToString("0.0")
                    : slider.value.ToString("0.00");
            OnValueChanged();
        }

        public void OnTextEntryChanged() {
            try {
                var value = Math.Min(Math.Max(float.Parse(numberTextBox.text, CultureInfo.InvariantCulture), minValue),
                    maxValue);
                slider.value = slider.wholeNumbers ? (int)value : value;
                numberTextBox.text = slider.wholeNumbers
                    ? value.ToString("0")
                    : value.ToString("0.00");
            }
            catch {
                slider.value = 0;
                numberTextBox.text = "0";
            }
            finally {
                OnValueChanged();
            }
        }

        private void UpdateSliderConstraints() {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
        }

        private void OnValueChanged() {
            onValueChanged.Invoke(Value);
        }

        public void OnMove(AxisEventData eventData) {
            // handle left and right 
            if (eventData.moveDir == MoveDirection.Left || eventData.moveDir == MoveDirection.Right) {
                var multipleOf = sliderIncrements;
                slider.value += eventData.moveDir == MoveDirection.Right ? multipleOf : -multipleOf;
                // round to nearest increment value
                slider.value = Mathf.Round(slider.value / multipleOf) * multipleOf;
            }

            // otherwise yeet the event up
            else {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.moveHandler);
            }
        }
    }
}