using Frolics.Tweens.TransformTweens;
using UnityEngine;

namespace Frolics.Tweens {
	public class TweenFactory {
		private readonly TweenPool tweenPool = new();

		public PropertyTween<Transform, Vector3> TweenPosition(Transform tweener, Vector3 target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, Vector3>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.position,
				setter: (t, v) => t.position = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Transform, float> TweenPositionX(Transform tweener, float targetX, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.position.x,
				setter: (t, x) => t.position = new Vector3(x, t.position.y, t.position.z),
				target: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Transform, float> TweenPositionY(Transform tweener, float targetX, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.position.y,
				setter: (t, y) => t.position = new Vector3(t.position.x, y, t.position.z),
				target: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Transform, float> TweenPositionZ(Transform tweener, float targetX, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.position.z,
				setter: (t, z) => t.position = new Vector3(t.position.x, t.position.y, z),
				target: targetX,
				duration: duration,
				lerp: Mathf.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Transform, Vector3> TweenLocalScale(Transform tweener, Vector3 target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, Vector3>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.localScale,
				setter: (t, v) => t.localScale = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Transform, Quaternion> TweenRotation(
			Transform tweener,
			Quaternion target,
			float duration
		) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Transform, Quaternion>>();

			propertyTween.Configure(
				tweener: tweener,
				getter: t => t.rotation,
				setter: (t, q) => t.rotation = q,
				target: target,
				duration: duration,
				lerp: Quaternion.Lerp
			);

			return propertyTween;
		}

		public RotateAroundTween PlayRotateAround(
			Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			var rotateAroundTween = tweenPool.Spawn<RotateAroundTween>();
			rotateAroundTween.Configure(tweener, axis, pivot, targetAngle, duration);
			return rotateAroundTween;
		}
	}
}
