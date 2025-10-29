using UnityEngine;

namespace Frolics.Tweens.RigidbodyTweens {
	public static class RigidbodyTweenExtensions {
		// TODO Convert these to RigidbodyTween
		public static PropertyTween<Rigidbody, Vector3> PlayPosition(
			this Rigidbody rb,
			Vector3 target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPosition(rb, target, duration);
		}

		public static PropertyTween<Rigidbody, Quaternion> PlayRotation(
			this Rigidbody rb,
			Quaternion target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenRotation(rb, target, duration);
		}
	}
}
