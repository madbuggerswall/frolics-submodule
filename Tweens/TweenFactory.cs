using Frolics.Tweens.TransformTweens;
using UnityEngine;

namespace Frolics.Tweens {
	internal class TweenFactory {
		private readonly TweenPool tweenPool = new();
		
		#region Transform Tweens

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

		#endregion

		#region Rigidbody Tweens

		public PropertyTween<Rigidbody, Vector3> TweenPosition(Rigidbody rb, Vector3 target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Rigidbody, Vector3>>();

			propertyTween.Configure(
				rb,
				getter: r => r.position,
				setter: (r, v) => r.MovePosition(v),
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<Rigidbody, Quaternion> TweenRotation(Rigidbody rb, Quaternion target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Rigidbody, Quaternion>>();

			propertyTween.Configure(
				rb,
				getter: r => r.rotation,
				setter: (r, q) => r.MoveRotation(q),
				target: target,
				duration: duration,
				lerp: Quaternion.Lerp
			);

			return propertyTween;
		}

		#endregion

		#region RectTransform Tweens

		public PropertyTween<RectTransform, Vector2> TweenAnchoredPosition(
			RectTransform rt,
			Vector2 target,
			float duration
		) {
			var propertyTween = tweenPool.Spawn<PropertyTween<RectTransform, Vector2>>();

			propertyTween.Configure(
				rt,
				getter: r => r.anchoredPosition,
				setter: (r, v) => r.anchoredPosition = v,
				target: target,
				duration: duration,
				lerp: Vector2.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<RectTransform, Vector3> TweenLocalScale(RectTransform rt, Vector3 target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<RectTransform, Vector3>>();

			propertyTween.Configure(
				rt,
				getter: r => r.localScale,
				setter: (r, v) => r.localScale = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<RectTransform, Vector3> TweenEulerAngles(
			RectTransform rt,
			Vector3 target,
			float duration
		) {
			var propertyTween = tweenPool.Spawn<PropertyTween<RectTransform, Vector3>>();

			propertyTween.Configure(
				rt,
				getter: r => r.eulerAngles,
				setter: (r, v) => r.eulerAngles = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);

			return propertyTween;
		}

		#endregion

		#region Camera Tweens

		public PropertyTween<Camera, float> TweenOrthoSize(Camera cam, float target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<Camera, float>>();

			propertyTween.Configure(
				cam,
				getter: c => c.orthographicSize,
				setter: (c, v) => c.orthographicSize = v,
				target: target,
				duration: duration,
				lerp: Mathf.Lerp
			);

			return propertyTween;
		}
		

		#endregion

		#region SpriteRenderer Tweens

		public PropertyTween<SpriteRenderer, Color> TweenColor(SpriteRenderer sr, Color target, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<SpriteRenderer, Color>>();

			propertyTween.Configure(
				sr,
				getter: s => s.color,
				setter: (s, c) => s.color = c,
				target: target,
				duration: duration,
				lerp: Color.Lerp
			);

			return propertyTween;
		}

		public PropertyTween<SpriteRenderer, float> TweenAlpha(SpriteRenderer sr, float targetAlpha, float duration) {
			var propertyTween = tweenPool.Spawn<PropertyTween<SpriteRenderer, float>>();

			propertyTween.Configure(
				sr,
				getter: s => s.color.a,
				setter: (s, a) => {
					Color c = s.color;
					c.a = a;
					s.color = c;
				},
				target: targetAlpha,
				duration: duration,
				lerp: Mathf.Lerp
			);

			return propertyTween;
		}

		#endregion
	}
}
