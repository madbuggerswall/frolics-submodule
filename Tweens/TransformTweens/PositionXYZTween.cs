using System;
using Frolics.Tweens.Easing;
using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public class PositionXYZTween : Tween {
		private readonly Transform tweener;

		private Func<float, float> easeFunctionPosX = Ease.Get(Ease.Type.Linear);
		private Func<float, float> easeFunctionPosY = Ease.Get(Ease.Type.Linear);
		private Func<float, float> easeFunctionPosZ = Ease.Get(Ease.Type.Linear);

		private (Vector3 initial, Vector3 target) position;

		public PositionXYZTween(Transform tweener, Vector3 targetPosition, float duration) : base(duration) {
			this.tweener = tweener;
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			// progress will be eased if SetEase is used to assign a function other than Linear
			// which may result in overlapping easing with axis-specific functions.
			float easedX = Mathf.Lerp(position.initial.x, position.target.x, easeFunctionPosX(easedTime));
			float easedY = Mathf.Lerp(position.initial.y, position.target.y, easeFunctionPosY(easedTime));
			float easedZ = Mathf.Lerp(position.initial.z, position.target.z, easeFunctionPosZ(easedTime));

			tweener.position = new Vector3(easedX, easedY, easedZ);
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}

		public void SetEaseX(Ease.Type easeType) => this.easeFunctionPosX = Ease.Get(easeType);
		public void SetEaseY(Ease.Type easeType) => this.easeFunctionPosY = Ease.Get(easeType);
		public void SetEaseZ(Ease.Type easeType) => this.easeFunctionPosZ = Ease.Get(easeType);

		public void SetEaseX(AnimationCurve animationCurve) => this.easeFunctionPosX = animationCurve.Evaluate;
		public void SetEaseY(AnimationCurve animationCurve) => this.easeFunctionPosY = animationCurve.Evaluate;
		public void SetEaseZ(AnimationCurve animationCurve) => this.easeFunctionPosZ = animationCurve.Evaluate;
	}
}
