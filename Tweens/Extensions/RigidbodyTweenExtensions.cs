using Frolics.Tweens.Core;
using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class RigidbodyTweenExtensions {
		public static Tween TweenMovePosition(this Rigidbody rb, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenMovePosition(rb, target, duration);
		}

		public static Tween TweenMoveRotation(this Rigidbody rb, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenMoveRotation(rb, target, duration);
		}
	}
}
