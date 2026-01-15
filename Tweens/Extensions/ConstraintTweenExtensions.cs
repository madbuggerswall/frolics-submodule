using Frolics.Tweens.Core;
using UnityEngine.Animations;
using ParentConstraint = Frolics.Utilities.ParentConstraint;

namespace Frolics.Tweens.Extensions {
	public static class ConstraintTweenExtensions {
		public static Tween TweenWeight(this PositionConstraint tweener, float target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenWeight(tweener, target, duration);
		}

		public static Tween TweenWeight(this RotationConstraint tweener, float target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenWeight(tweener, target, duration);
		}

		public static Tween TweenWeight(this ParentConstraint tweener, float target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenWeight(tweener, target, duration);
		}
	}
}
