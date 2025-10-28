using Frolics.Tweens.Experimental;
using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public static class TransformTweenExtensions {
		public static PropertyTween<Transform, Vector3> TweenPosition(
			this Transform tweener,
			Vector3 target,
			float duration
		) {
			return new PropertyTween<Transform, Vector3>(
				target: tweener,
				getter: t => t.position,
				setter: (t, v) => t.position = v,
				end: target,
				duration: duration,
				lerp: Vector3.Lerp
			);
		}

		public static PropertyTween<Transform, float> TweenPositionX(
			this Transform tweener,
			float targetX,
			float duration
		) {
			return new PropertyTween<Transform, float>(
				target: tweener,
				getter: t => t.position.x,
				setter: (t, x) => t.position = new Vector3(x, t.position.y, t.position.z),
				end: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);
		}

		public static PropertyTween<Transform, float> TweenPositionY(
			this Transform tweener,
			float targetX,
			float duration
		) {
			return new PropertyTween<Transform, float>(
				target: tweener,
				getter: t => t.position.y,
				setter: (t, y) => t.position = new Vector3(t.position.x, y, t.position.z),
				end: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);
		}

		public static PropertyTween<Transform, float> TweenPositionZ(
			this Transform tweener,
			float targetX,
			float duration
		) {
			return new PropertyTween<Transform, float>(
				target: tweener,
				getter: t => t.position.z,
				setter: (t, z) => t.position = new Vector3(t.position.x, t.position.y, z),
				end: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);
		}

		public static PropertyTween<Transform, Vector3> TweenLocalScale(
			this Transform tweener,
			Vector3 target,
			float duration
		) {
			return new PropertyTween<Transform, Vector3>(
				target: tweener,
				getter: t => t.localScale,
				setter: (t, v) => t.localScale = v,
				end: target,
				duration: duration,
				lerp: Vector3.Lerp
			);
		}

		public static PropertyTween<Transform, Quaternion> TweenRotation(
			this Transform tweener,
			Quaternion target,
			float duration
		) {
			return new PropertyTween<Transform, Quaternion>(
				target: tweener,
				getter: t => t.rotation,
				setter: (t, q) => t.rotation = q,
				end: target,
				duration: duration,
				lerp: Quaternion.Lerp
			);
		}

		public static RotateAroundTween PlayRotateAround(
			this Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			return new RotateAroundTween(tweener, axis, pivot, targetAngle, duration);
		}
	}
}
