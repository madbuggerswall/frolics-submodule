using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public class PositionTween : Tween {
		private readonly Transform tweener;
		private (Vector3 initial, Vector3 target) position;

		public PositionTween(Transform tweener, Vector3 targetPosition, float duration) : base(duration) {
			this.tweener = tweener;
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.position = Vector3.Lerp(position.initial, position.target, progress);
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}
	}
}
