using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CameraControlSamples {
	public sealed class LabeledToggle : LabeledContent<TextMeshProUGUI, Toggle> {
		[SerializeField] private string defaultLabel = default;
		[SerializeField] private bool defaultToggleState = default;

		// MARK: - Properties

		public string Label {
			get => labelObject.text;
			set => labelObject.text = value;
		}

		public bool Value {
			get => contentObject.isOn;
			set => contentObject.isOn = value;
		}

		// MARK: - Events

		public event UnityAction<bool> OnValueChanged {
			add => contentObject.onValueChanged.AddListener(value);
			remove => contentObject.onValueChanged.RemoveListener(value);
		}

		// MARK: - Lifecycle

		private void Awake() {
			Label = defaultLabel;
			Value = defaultToggleState;
		}
	}
}