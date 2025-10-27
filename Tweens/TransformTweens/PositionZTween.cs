using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public class PositionZTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionZ;

		public PositionZTween(Transform tweener, float targetZ, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionZ.initial = tweener.position.z;
			this.positionZ.target = targetZ;
		}

		protected override void UpdateTween() {
			float posZ = Mathf.Lerp(positionZ.initial, positionZ.target, easedTime);
			tweener.position = new Vector3(tweener.position.x, tweener.position.y, posZ);
		}

		protected override void SampleInitialState() {
			this.positionZ.initial = tweener.position.z;
		}
	}
}