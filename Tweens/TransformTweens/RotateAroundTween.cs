using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	internal class RotateAroundTween : Tween {
		private Transform tweener;
		private Vector3 axis;
		private Vector3 pivot;
		private (float initial, float target) angle;

		private Vector3 initialDirection;

		public RotateAroundTween() { }

		internal RotateAroundTween(Transform tweener, Vector3 axis, Vector3 pivot, float targetAngle, float duration) {
			Configure(tweener, axis, pivot, targetAngle, duration);
		}

		internal void Configure(Transform tweener, Vector3 axis, Vector3 pivot, float targetAngle, float duration) {
			this.tweener = tweener;
			this.axis = axis.normalized;
			this.pivot = pivot;
			this.angle.target = targetAngle;
			this.duration = duration;

			// Store initial direction from pivot to object
			this.initialDirection = tweener.position - pivot;
		}

		protected override void UpdateTween() {
			// Current direction from pivot to tweener
			Vector3 currentDirection = tweener.position - pivot;

			// Signed angle between initial and current directions
			float currentAngle = Vector3.SignedAngle(initialDirection, currentDirection, axis);

			// Target angle at this progress
			float targetAngle = Mathf.Lerp(angle.initial, angle.target, normalizedTime);

			// Rotate by the delta
			float deltaAngle = targetAngle - currentAngle;
			tweener.RotateAround(pivot, axis, deltaAngle);
		}

		protected override void SampleInitialState() {
			throw new System.NotImplementedException();
		}

		internal override void Recycle(ITweenPool pool) {
			Reset();
			pool.Despawn(this);
		}
	}
}
