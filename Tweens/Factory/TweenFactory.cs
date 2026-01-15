using System;
using Frolics.Tweens.Accessors;
using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using Frolics.Tweens.PropertyLerps;
using Frolics.Tweens.Types;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using ParentConstraint = Frolics.Utilities.ParentConstraint;


namespace Frolics.Tweens.Factory {
	internal class TweenFactory {
		private readonly ITweenPool tweenPool;

		internal TweenFactory(ITweenPool tweenPool) {
			this.tweenPool = tweenPool;
		}

		#region Sequence

		internal Sequence TweenSequence() {
			Sequence sequence = tweenPool.Spawn<Sequence>();
			return sequence;
		}

		#endregion

		#region Virtual

		internal FloatTween TweenFloat(float target, float duration, Func<float> getter, Action<float> setter) {
			FloatTween tween = tweenPool.Spawn<FloatTween>();
			tween.Configure(target, duration, getter, setter);
			return tween;
		}

		#endregion

		#region Constraint Tweens

		internal Tween TweenWeight(PositionConstraint tweener, float target, float duration) {
			var tween = tweenPool
				.Spawn<PropertyTween<PositionConstraint, float, PositionConstraintWeight, FloatLerp>>();

			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenWeight(RotationConstraint tweener, float target, float duration) {
			var tween = tweenPool
				.Spawn<PropertyTween<RotationConstraint, float, RotationConstraintWeight, FloatLerp>>();

			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenWeight(ParentConstraint tweener, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<ParentConstraint, float, ParentConstraintWeight, FloatLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		#endregion

		#region Transform Tweens

		internal Tween TweenPosition(Transform tweener, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Vector3, Position, Vector3Lerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenPositionX(Transform tweener, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float, PositionX, FloatLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenPositionY(Transform tweener, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float, PositionY, FloatLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenPositionZ(Transform tweener, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, float, PositionZ, FloatLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenLocalScale(Transform tweener, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Vector3, LocalScale, Vector3Lerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenRotation(Transform tweener, Quaternion target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Quaternion, Rotation, QuaternionLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenLocalRotation(Transform tweener, Quaternion target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Quaternion, LocalRotation, QuaternionLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenEulerAngles(Transform tweener, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Transform, Vector3, LocalEulerAngles, Vector3Lerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		#endregion

		#region Rigidbody Tweens

		internal Tween TweenMovePosition(Rigidbody rigidbody, Vector3 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Rigidbody, Vector3, MovePosition, Vector3Lerp>>();
			tween.Configure(rigidbody, target, duration);
			return tween;
		}

		internal Tween TweenMoveRotation(Rigidbody rigidbody, Quaternion target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Rigidbody, Quaternion, MoveRotation, QuaternionLerp>>();
			tween.Configure(rigidbody, target, duration);
			return tween;
		}

		#endregion

		#region RectTransform Tweens

		internal Tween TweenAnchoredPosition(RectTransform tweener, Vector2 target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<RectTransform, Vector2, AnchoredPosition, Vector2Lerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		#endregion

		#region Camera Tweens

		internal Tween TweenOrthoSize(Camera tweener, float target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<Camera, float, OrthoSize, FloatLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		#endregion

		#region SpriteRenderer Tweens

		internal Tween TweenColor(SpriteRenderer tweener, Color target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<SpriteRenderer, Color, SpriteRendererColor, ColorLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenAlpha(SpriteRenderer tweener, float targetAlpha, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<SpriteRenderer, float, SpriteRendererAlpha, FloatLerp>>();
			tween.Configure(tweener, targetAlpha, duration);
			return tween;
		}

		#endregion

		#region TextMeshPro Tweens

		internal Tween TweenColor(TextMeshPro tweener, Color target, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<TextMeshPro, Color, TextColor, ColorLerp>>();
			tween.Configure(tweener, target, duration);
			return tween;
		}

		internal Tween TweenAlpha(TextMeshPro tweener, float targetAlpha, float duration) {
			var tween = tweenPool.Spawn<PropertyTween<TextMeshPro, float, TextAlpha, FloatLerp>>();
			tween.Configure(tweener, targetAlpha, duration);
			return tween;
		}

		#endregion
	}
}
// End
