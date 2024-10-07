using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CameraControlSamples {
	public sealed class GradientControl : MonoBehaviour {
		[SerializeField] private Button removeButton = default;
		[SerializeField] private Button addButton = default;

		// MARK: - Properties

		public bool RemoveInteractable {
			get => removeButton.interactable;
			set => removeButton.interactable = value;
		}

		public bool AddInteractable {
			get => addButton.interactable;
			set => addButton.interactable = value;
		}

		// MARK: - Events

		public event UnityAction OnRemove {
			add => removeButton.onClick.AddListener(value);
			remove => removeButton.onClick.RemoveListener(value);
		}

		public event UnityAction OnAdd {
			add => addButton.onClick.AddListener(value);
			remove => addButton.onClick.RemoveListener(value);
		}
	}
}
