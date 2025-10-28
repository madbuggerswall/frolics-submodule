using UnityEngine;

namespace Frolics.Tweens {
	public abstract class RigidbodyTween : Tween {
		protected Rigidbody tweener;

		public RigidbodyTween(Rigidbody tweener, float duration) : base(duration) {
			this.tweener = tweener;
		}

		public override void Play() {
			Rewind();
			tweenManager.AddTween(this);
		}
	}
}