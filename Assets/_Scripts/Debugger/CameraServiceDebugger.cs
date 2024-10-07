using CameraControl;
using UnityEngine;
using UnityEngine.UI;

namespace CameraControlSamples {
	public sealed class CameraServiceDebugger : MonoBehaviour {
		private CameraService cameraService;

		[SerializeField] private int updateStep = 2;

		[SerializeField] private LabeledToggle isRunningToggle = default;
		private bool previousIsRunning = default;

		[SerializeField] private LabeledToggle supportsControlsToggle = default;
		private bool previousSupportsControls = default;

		[SerializeField] private LabeledText maxControlsCountText = default;
		private int previousMaxControlsCount = default;

		[SerializeField] private LabeledText controlCountText = default;
		private int previousControlCount = default;

		[SerializeField] private LabeledToggle controlsActiveToggle = default;
		[SerializeField] private LabeledToggle fullscreenAppearanceToggle = default;

		[SerializeField] private LabeledGradientControl captureSliderGradientControl = default;

		[SerializeField] private CaptureSliderDebugger captureSliderPrefab = default;
		private CaptureSliderDebugger captureSliderDebugger = default;

		// MARK: - Lifecycle

		private void Awake() {
			cameraService = new CameraService();
		}

		private void Start() {
			UpdateIsRunningToggle(force: true);
			UpdateSupportsControlsToggle(force: true);
			UpdateMaxControlsCount(force: true);
			UpdateControlCount(force: true);
			controlsActiveToggle.Value = false;
			fullscreenAppearanceToggle.Value = false;

			captureSliderGradientControl.RemoveInteractable = false;
			captureSliderGradientControl.AddInteractable = true;
		}

		private void OnEnable() {
			isRunningToggle.OnValueChanged += OnIsRunningToggleChanged;
			captureSliderGradientControl.OnRemove += OnRemoveCaptureSliderButton;
			captureSliderGradientControl.OnAdd += OnAddCaptureSliderButton;

			cameraService.DidBecomeActive += OnCameraServiceDidBecomeActive;
			cameraService.DidBecomeInactive += OnCameraServiceDidBecomeInactive;
			cameraService.WillEnterFullscreenAppearance += OnCameraServiceWillEnterFullscreenAppearance;
			cameraService.WillExitFullscreenAppearance += OnCameraServiceWillExitFullscreenAppearance;
		}

		private void OnDisable() {
			isRunningToggle.OnValueChanged -= OnIsRunningToggleChanged;
			captureSliderGradientControl.OnRemove -= OnRemoveCaptureSliderButton;
			captureSliderGradientControl.OnAdd -= OnAddCaptureSliderButton;

			cameraService.DidBecomeActive -= OnCameraServiceDidBecomeActive;
			cameraService.DidBecomeInactive -= OnCameraServiceDidBecomeInactive;
			cameraService.WillEnterFullscreenAppearance -= OnCameraServiceWillEnterFullscreenAppearance;
			cameraService.WillExitFullscreenAppearance -= OnCameraServiceWillExitFullscreenAppearance;
		}

		private void OnDestroy() {
			cameraService.Dispose();
		}

		private void Update() {
			if (Time.frameCount % updateStep != 0) {
				return;
			}

			UpdateIsRunningToggle();
			UpdateSupportsControlsToggle();
			UpdateMaxControlsCount();
			UpdateControlCount();
		}

		// MARK: - Value Update

		private void UpdateIsRunningToggle(bool force = false) {
			bool currentValue = cameraService.IsRunning;
			if (force || previousIsRunning != currentValue) {
				isRunningToggle.Value = currentValue;
				previousIsRunning = currentValue;
			}
		}

		private void UpdateSupportsControlsToggle(bool force = false) {
			bool currentValue = cameraService.SupportsControls;
			if (force || previousSupportsControls != currentValue) {
				supportsControlsToggle.Value = currentValue;
				previousSupportsControls = currentValue;
			}
		}

		private void UpdateMaxControlsCount(bool force = false) {
			int currentValue = cameraService.MaxControlsCount;
			if (force || previousMaxControlsCount != currentValue) {
				maxControlsCountText.Text = currentValue.ToString();
				previousMaxControlsCount = currentValue;
			}
		}

		private void UpdateControlCount(bool force = false) {
			int currentValue = cameraService.ControlCount;
			if (force || previousControlCount != currentValue) {
				controlCountText.Text = currentValue.ToString();
				previousControlCount = currentValue;
			}
		}

		// MARK: - Events

		private void OnIsRunningToggleChanged(bool newValue) {
			cameraService.IsRunning = newValue;
		}

		private void OnAddCaptureSliderButton() {
			Debug.Assert(captureSliderDebugger == null);

			captureSliderDebugger = Object.Instantiate(captureSliderPrefab, transform.parent);

			Debug.Assert(captureSliderDebugger.CaptureSlider != null);
			this.cameraService.AddControl(captureSliderDebugger.CaptureSlider);

			this.captureSliderGradientControl.RemoveInteractable = true;
			this.captureSliderGradientControl.AddInteractable = false;
		}

		private void OnRemoveCaptureSliderButton() {
			Debug.Assert(captureSliderDebugger != null);
			Debug.Assert(captureSliderDebugger.CaptureSlider != null);

			this.cameraService.RemoveControl(captureSliderDebugger.CaptureSlider);

			Object.Destroy(captureSliderDebugger.gameObject);
			captureSliderDebugger = null;

			this.captureSliderGradientControl.RemoveInteractable = false;
			this.captureSliderGradientControl.AddInteractable = false;
		}

		private void OnCameraServiceDidBecomeActive(CameraService sender) {
			Debug.Log($"recieved DidBecomeActive");
			Debug.Assert(sender == cameraService);
			controlsActiveToggle.Value = true;
		}

		private void OnCameraServiceDidBecomeInactive(CameraService sender) {
			Debug.Log($"recieved DidBecomeInactive");
			Debug.Assert(sender == cameraService);
			controlsActiveToggle.Value = false;
		}

		private void OnCameraServiceWillEnterFullscreenAppearance(CameraService sender) {
			Debug.Log($"recieved WillEnterFullscreenAppearance");
			Debug.Assert(sender == cameraService);
			fullscreenAppearanceToggle.Value = true;
		}

		private void OnCameraServiceWillExitFullscreenAppearance(CameraService sender) {
			Debug.Log($"recieved WillExitFullscreenAppearance");
			Debug.Assert(sender == cameraService);
			fullscreenAppearanceToggle.Value = false;
		}
	}
}
