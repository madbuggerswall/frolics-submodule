using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public class PositionXTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionX;

		public PositionXTween(Transform tweener, float targetX, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionX.initial = tweener.position.x;
			this.positionX.target = targetX;
		}

		protected override void UpdateTween() {
			float posX = Mathf.Lerp(positionX.initial, positionX.target, easedTime);
			tweener.position = new Vector3(posX, tweener.position.y, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionX.initial = tweener.position.x;
		}
	}
}