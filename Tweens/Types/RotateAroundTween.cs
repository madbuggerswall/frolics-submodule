using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Types {
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
			this.angle.initial = Vector3.Dot(tweener.eulerAngles, axis.normalized);
			this.angle.target = targetAngle;
			this.duration = duration;

			// Store initial direction from pivot to object
			this.initialDirection = (tweener.position - pivot);
			if (angle.initial <= angle.target)
				return;

			Quaternion rotation = Quaternion.AngleAxis(-angle.initial, axis);
			this.initialDirection = rotation * initialDirection;
		}

		protected override void UpdateTween(float easedTime) {
			// Target angle at this progress
			float targetAngle = Mathf.Lerp(angle.initial, angle.target, easedTime);

			// Build a rotation around the axis
			Quaternion rotation = Quaternion.AngleAxis(targetAngle, axis);

			// Position: Rotate the initial direction vector and reapply pivot offset
			Vector3 rotatedDirection = rotation * initialDirection;
			tweener.position = pivot + rotatedDirection;

			// Rotation: Apply the same rotation to tweener's initial rotation
			tweener.rotation = rotation;
		}

		internal override void Recycle(ITweenPool pool) {
			Reset();
			pool.Despawn(this);
		}

		internal override UnityEngine.Object GetTweener() {
			return tweener;
		}
	}
}
