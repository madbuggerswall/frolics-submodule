using UnityEngine;

namespace Frolics.Utilities {
	public class ParentConstraint : MonoBehaviour {
		[SerializeField] private Transform parent;
		[SerializeField, Range(0f, 1f)] private float weight;

		[SerializeField] private bool isActive;

		private void Update() {
			if (!isActive || parent == null)
				return;

			transform.position = Vector3.Lerp(transform.position, parent.position, weight);
			transform.rotation = Quaternion.Lerp(transform.rotation, parent.rotation, weight);
		}

		// Setters
		public void SetWeight(float weight) => this.weight = weight;
		public void SetActive(bool isActive) => this.isActive = isActive;
		public void SetParent(Transform parent) => this.parent = parent;

		// Getters
		public float GetWeight() => weight;
	}
}
