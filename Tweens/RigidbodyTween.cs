using UnityEngine;

namespace Frolics.Tweens {
	public abstract class RigidbodyTween : Tween {
		protected Rigidbody tweener;

		public RigidbodyTween(Rigidbody tweener, float duration) : base() {
			this.tweener = tweener;
		}
	}
}
