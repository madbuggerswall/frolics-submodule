using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public class PositionYTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionY;

		public PositionYTween(Transform tweener, float targetY, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionY.initial = tweener.position.y;
			this.positionY.target = targetY;
		}

		protected override void UpdateTween() {
			float posY = Mathf.Lerp(positionY.initial, positionY.target, easedTime);
			tweener.position = new Vector3(tweener.position.x, posY, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionY.initial = tweener.position.y;
		}
	}
}