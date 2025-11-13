using System;
using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using Frolics.Tweens.Types;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Frolics.Tweens.Factory {
	internal class TweenFactory {
		private readonly ITweenPool tweenPool;
		private readonly Action<Tween> register;

		internal TweenFactory(ITweenPool tweenPool, Action<Tween> register) {
			this.tweenPool = tweenPool;
			this.register = register;
		}


		#region Transform Tweens

		internal PropertyTween<Transform, Vector3> TweenPosition(Transform tweener, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Vector3>>();

			tween.Configure(
				tweener: tweener,
				target: target,
				duration: duration,
				getter: t => t.position,
				setter: (t, v) => t.position = v,
				lerp: Vector3.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, float> TweenPositionX(Transform tweener, float targetX, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			tween.Configure(
				tweener: tweener,
				target: targetX,
				duration: duration,
				getter: t => t.position.x,
				setter: (t, x) => t.position = t.position.WithX(x),
				lerp: Mathf.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, float> TweenPositionY(Transform tweener, float targetY, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			tween.Configure(
				tweener: tweener,
				target: targetY,
				duration: duration,
				getter: t => t.position.y,
				setter: (t, y) => t.position = t.position.WithY(y),
				lerp: Mathf.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, float> TweenPositionZ(Transform tweener, float targetX, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float>>();

			tween.Configure(
				tweener: tweener,
				target: targetX,
				duration: duration,
				getter: t => t.position.z,
				setter: (t, z) => t.position = t.position.WithZ(z),
				lerp: Mathf.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, Vector3> TweenLocalScale(Transform tweener, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Vector3>>();

			tween.Configure(
				tweener: tweener,
				target: target,
				duration: duration,
				getter: t => t.localScale,
				setter: (t, v) => t.localScale = v,
				lerp: Vector3.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, Quaternion> TweenRotation(
			Transform tweener,
			Quaternion target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Quaternion>>();

			tween.Configure(
				tweener: tweener,
				target: target,
				duration: duration,
				getter: t => t.rotation,
				setter: (t, q) => t.rotation = q,
				lerp: Quaternion.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Transform, Quaternion> TweenLocalRotation(
			Transform tweener,
			Quaternion target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Quaternion>>();

			tween.Configure(
				tweener: tweener,
				target: target,
				duration: duration,
				getter: t => t.localRotation,
				setter: (t, q) => t.localRotation = q,
				lerp: Quaternion.Lerp
			);

			register(tween);
			return tween;
		}

		internal RotateAroundTween TweenRotateAround(
			Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			var tween = tweenPool.Spawn<RotateAroundTween>();
			tween.Configure(tweener, axis, pivot, targetAngle, duration);

			register(tween);
			return tween;
		}

		#endregion

		#region Rigidbody Tweens

		internal PropertyTween<Rigidbody, Vector3> TweenMovePosition(Rigidbody rb, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Rigidbody, Vector3>>();

			tween.Configure(
				rb,
				target: target,
				duration: duration,
				getter: r => r.position,
				setter: (r, v) => r.MovePosition(v),
				lerp: Vector3.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<Rigidbody, Quaternion> TweenMoveRotation(
			Rigidbody rb,
			Quaternion target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<Rigidbody, Quaternion>>();

			tween.Configure(
				rb,
				target: target,
				duration: duration,
				getter: r => r.rotation,
				setter: (r, q) => r.MoveRotation(q),
				lerp: Quaternion.Lerp
			);

			register(tween);
			return tween;
		}

		#endregion

		#region RectTransform Tweens

		internal PropertyTween<RectTransform, Vector2> TweenAnchoredPosition(
			RectTransform rt,
			Vector2 target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<RectTransform, Vector2>>();

			tween.Configure(
				rt,
				target: target,
				duration: duration,
				getter: r => r.anchoredPosition,
				setter: (r, v) => r.anchoredPosition = v,
				lerp: Vector2.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<RectTransform, Vector3> TweenLocalScale(
			RectTransform rt,
			Vector3 target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<RectTransform, Vector3>>();

			tween.Configure(
				rt,
				target: target,
				duration: duration,
				getter: r => r.localScale,
				setter: (r, v) => r.localScale = v,
				lerp: Vector3.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<RectTransform, Vector3> TweenEulerAngles(
			RectTransform rt,
			Vector3 target,
			float duration
		) {
			var tween = tweenPool.Spawn<PropertyTween<RectTransform, Vector3>>();

			tween.Configure(
				rt,
				target: target,
				duration: duration,
				getter: r => r.eulerAngles,
				setter: (r, v) => r.eulerAngles = v,
				lerp: Vector3.Lerp
			);

			register(tween);
			return tween;
		}

		#endregion

		#region Camera Tweens

		internal PropertyTween<Camera, float> TweenOrthoSize(Camera cam, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Camera, float>>();

			tween.Configure(
				cam,
				target: target,
				duration: duration,
				getter: c => c.orthographicSize,
				setter: (c, v) => c.orthographicSize = v,
				lerp: Mathf.Lerp
			);

			register(tween);
			return tween;
		}

		#endregion

		#region SpriteRenderer Tweens

		internal PropertyTween<SpriteRenderer, Color> TweenColor(SpriteRenderer sr, Color target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<SpriteRenderer, Color>>();

			tween.Configure(
				sr,
				target: target,
				duration: duration,
				getter: s => s.color,
				setter: (s, c) => s.color = c,
				lerp: Color.Lerp
			);

			register(tween);
			return tween;
		}

		internal PropertyTween<SpriteRenderer, float> TweenAlpha(SpriteRenderer sr, float targetAlpha, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<SpriteRenderer, float>>();

			tween.Configure(
				sr,
				target: targetAlpha,
				duration: duration,
				getter: s => s.color.a,
				setter: (s, a) => s.color = s.color.WithAlpha(a),
				lerp: Mathf.Lerp
			);

			register(tween);
			return tween;
		}

		#endregion
	}
}
