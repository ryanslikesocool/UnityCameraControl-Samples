using CameraControl;
using TMPro;
using UnityEngine;

namespace CameraControlSamples {
	internal sealed class CaptureSliderDebugger : MonoBehaviour {
		[SerializeField]
		private CaptureSliderDescriptor descriptor = new CaptureSliderDescriptor {
			lowerBound = 0,
			upperBound = 1,
		};

		internal CaptureSlider CaptureSlider { get; private set; }

		[SerializeField] private int updateStep = 2;

		[SerializeField] private LabeledToggle isEnabledToggle = default;
		private bool previousIsEnabled = default;

		[SerializeField] private string valueTextFormat = "0.00";
		[SerializeField] private LabeledText valueText = default;
		private float previousValue = default;

		// MARK: - Lifecycle

		private void Awake() {
			CaptureSlider = new CaptureSlider(descriptor);
		}

		private void Start() {
			UpdateIsEnabled(force: true);
			UpdateValue(force: true);
		}

		private void OnEnable() {
			isEnabledToggle.OnValueChanged += OnIsEnabledToggleChanged;
			CaptureSlider.OnValueChanged += OnCaptureSliderValueChanged;
		}

		private void OnDisable() {
			isEnabledToggle.OnValueChanged -= OnIsEnabledToggleChanged;
			CaptureSlider.OnValueChanged -= OnCaptureSliderValueChanged;
		}

		private void OnDestroy() {
			CaptureSlider.Dispose();
		}

		private void Update() {
			if (Time.frameCount % updateStep != 0) {
				return;
			}

			UpdateIsEnabled();
			//UpdateValue();
		}

		// MARK: - Value Update

		private void UpdateIsEnabled(bool force = false) {
			bool currentValue = CaptureSlider.IsEnabled;
			if (force || previousIsEnabled != currentValue) {
				isEnabledToggle.Value = currentValue;
				previousIsEnabled = currentValue;
			}
		}

		private void UpdateValue(bool force = false) {
			float currentValue = CaptureSlider.Value;
			if (force || previousValue != currentValue) {
				OnCaptureSliderValueChanged(CaptureSlider, currentValue);
			}
		}

		// MARK: - Events

		private void OnIsEnabledToggleChanged(bool newValue) {
			CaptureSlider.IsEnabled = newValue;
		}

		private void OnCaptureSliderValueChanged(CaptureControl sender, float newValue) {
			Debug.Assert(sender == CaptureSlider);
			valueText.Text = newValue.ToString(valueTextFormat);
			previousValue = newValue;
		}
	}
}
