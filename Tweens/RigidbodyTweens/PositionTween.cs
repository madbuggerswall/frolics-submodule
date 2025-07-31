using UnityEngine;

namespace Frolics.Tweens.RigidbodyTweens {
	public class PositionTween : RigidbodyTween {
		private (Vector3 initial, Vector3 target) position;

		public PositionTween(Rigidbody tweener, Vector3 targetPosition, float duration) : base(tweener, duration) {
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.MovePosition(Vector3.Lerp(position.initial, position.target, progress));
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}
	}

	public class PositionXTween : RigidbodyTween {
		private (float initial, float target) positionX;

		public PositionXTween(Rigidbody tweener, float targetX, float duration) : base(tweener, duration) {
			this.tweener = tweener;
			this.positionX.initial = tweener.position.x;
			this.positionX.target = targetX;
		}

		protected override void UpdateTween() {
			float posX = Mathf.Lerp(positionX.initial, positionX.target, progress);
			tweener.MovePosition(new Vector3(posX, tweener.position.y, tweener.position.z));
		}

		protected override void SampleInitialState() {
			this.positionX.initial = tweener.position.x;
		}
	}

	public class PositionYTween : RigidbodyTween {
		private (float initial, float target) positionY;

		public PositionYTween(Rigidbody tweener, float targetY, float duration) : base(tweener, duration) {
			this.tweener = tweener;
			this.positionY.initial = tweener.position.y;
			this.positionY.target = targetY;
		}

		protected override void UpdateTween() {
			float posY = Mathf.Lerp(positionY.initial, positionY.target, progress);
			tweener.MovePosition(new Vector3(tweener.position.x, posY, tweener.position.z));
		}

		protected override void SampleInitialState() {
			this.positionY.initial = tweener.position.y;
		}
	}

	public class PositionZTween : RigidbodyTween {
		private (float initial, float target) positionZ;

		public PositionZTween(Rigidbody tweener, float targetZ, float duration) : base(tweener, duration) {
			this.tweener = tweener;
			this.positionZ.initial = tweener.position.z;
			this.positionZ.target = targetZ;
		}

		protected override void UpdateTween() {
			float posZ = Mathf.Lerp(positionZ.initial, positionZ.target, progress);
			tweener.MovePosition(new Vector3(tweener.position.x, tweener.position.y, posZ));
		}

		protected override void SampleInitialState() {
			this.positionZ.initial = tweener.position.z;
		}
	}
}
