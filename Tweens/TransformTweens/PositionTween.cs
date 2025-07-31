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

	public class PositionXTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionX;

		public PositionXTween(Transform tweener, float targetX, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionX.initial = tweener.position.x;
			this.positionX.target = targetX;
		}

		protected override void UpdateTween() {
			float posX = Mathf.Lerp(positionX.initial, positionX.target, progress);
			tweener.position = new Vector3(posX, tweener.position.y, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionX.initial = tweener.position.x;
		}
	}

	public class PositionYTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionY;

		public PositionYTween(Transform tweener, float targetY, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionY.initial = tweener.position.y;
			this.positionY.target = targetY;
		}

		protected override void UpdateTween() {
			float posY = Mathf.Lerp(positionY.initial, positionY.target, progress);
			tweener.position = new Vector3(tweener.position.x, posY, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionY.initial = tweener.position.y;
		}
	}

	public class PositionZTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionZ;

		public PositionZTween(Transform tweener, float targetZ, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionZ.initial = tweener.position.z;
			this.positionZ.target = targetZ;
		}

		protected override void UpdateTween() {
			float posZ = Mathf.Lerp(positionZ.initial, positionZ.target, progress);
			tweener.position = new Vector3(tweener.position.x, tweener.position.y, posZ);
		}

		protected override void SampleInitialState() {
			this.positionZ.initial = tweener.position.z;
		}
	}
}
