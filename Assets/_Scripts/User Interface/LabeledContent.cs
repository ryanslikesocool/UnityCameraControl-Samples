using UnityEngine;

namespace CameraControlSamples {
	public abstract class LabeledContent<Label, Content> : MonoBehaviour
		where Label : UnityEngine.Object
		where Content : UnityEngine.Object 
	{
		[Header("References")]
		[SerializeField] protected Label labelObject = default;
		[SerializeField] protected Content contentObject = default;
	}
}