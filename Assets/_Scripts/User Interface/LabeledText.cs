using TMPro;
using UnityEngine;

namespace CameraControlSamples {
	public sealed class LabeledText : LabeledContent<TextMeshProUGUI, TextMeshProUGUI> {
		[SerializeField] private string defaultLabel = default;
		[SerializeField] private string defaultValue = default;

		// MARK: - Properties

		public string Label {
			get => labelObject.text;
			set => labelObject.text = value;
		}

		public string Text {
			get => contentObject.text;
			set => contentObject.text = value;
		}

		// MARK: - Lifecycle

		private void Awake() {
			Label = defaultLabel;
			Text = defaultValue;
		}
	}
}
