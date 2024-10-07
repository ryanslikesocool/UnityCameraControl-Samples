using CameraControl;
using UnityEngine;

namespace CameraControlSamples {
	internal sealed class CameraControlManager : MonoBehaviour {
		[SerializeField] private LogFlags logFlags = LogFlags.None;

		// MARK: - Lifecycle

		private void Awake() {
			CameraControl.Logger.LogFlags = logFlags;
		}
	}
}