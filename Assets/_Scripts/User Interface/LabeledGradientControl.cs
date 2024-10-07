using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace CameraControlSamples {
	public sealed class LabeledGradientControl : LabeledContent<TextMeshProUGUI, GradientControl> {
		[SerializeField] private string defaultLabel = default;

		// MARK: - Properties

		public string Label {
			get => labelObject.text;
			set => labelObject.text = value;
		}

		public bool RemoveInteractable {
			get => contentObject.RemoveInteractable;
			set => contentObject.RemoveInteractable = value;
		}

		public bool AddInteractable {
			get => contentObject.AddInteractable;
			set => contentObject.AddInteractable = value;
		}

		// MARK: - Events

		public event UnityAction OnRemove {
			add => contentObject.OnRemove += value;
			remove => contentObject.OnRemove -= value;
		}

		public event UnityAction OnAdd {
			add => contentObject.OnAdd += value;
			remove => contentObject.OnAdd -= value;
		}

		// MARK: - Lifecycle

		private void Start() {
			Label = defaultLabel;
		}
	}
}
