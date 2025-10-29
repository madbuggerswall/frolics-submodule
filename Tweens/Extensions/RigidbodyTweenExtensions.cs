using UnityEngine;

namespace Frolics.Tweens.Extensions {
	internal static class RigidbodyTweenExtensions {
		// TODO Convert these to RigidbodyTween
		public static Tween PlayPosition(this Rigidbody rb, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPosition(rb, target, duration);
		}

		public static Tween PlayRotation(this Rigidbody rb, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenRotation(rb, target, duration);
		}
	}
}
