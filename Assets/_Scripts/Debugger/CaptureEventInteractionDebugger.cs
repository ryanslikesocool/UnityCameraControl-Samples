using UnityEngine;
using CameraControl;

namespace CameraControlSamples {
	internal sealed class CaptureEventInteractionDebugger : MonoBehaviour {
		private CaptureEventInteraction eventInteraction;

		[SerializeField] private LabeledToggle isEnabledToggle = default;
		[SerializeField] private LabeledText eventPhaseText = default;

		// MARK: - Lifecycle

		private void Awake() {
			eventInteraction = new CaptureEventInteraction(OnCaptureEvent);
		}

		private void Start() {
			SetIsEnabledToggle(eventInteraction.IsEnabled);
		}

		private void OnEnable() {
			isEnabledToggle.OnValueChanged += OnIsEnabledToggleChanged;
		}

		private void OnDisable() {
			isEnabledToggle.OnValueChanged -= OnIsEnabledToggleChanged;
		}

		private void OnDestroy() {
			eventInteraction.Dispose();
		}

		// MARK: - Values

		private void SetIsEnabledToggle(bool newValue) {
			isEnabledToggle.Value = newValue;
		}

		private void SetEventPhaseText(CaptureEventPhase newValue) {
			eventPhaseText.Text = newValue.ToString();
		}

		// MARK: - Events

		private void OnIsEnabledToggleChanged(bool newValue) {
			eventInteraction.IsEnabled = newValue;
		}

		private void OnCaptureEvent(CaptureEvent captureEvent) {
			//Debug.Assert(sender == eventInteraction);

			SetEventPhaseText(captureEvent.phase);
		}
	}
}