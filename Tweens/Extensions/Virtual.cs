using System;
using Frolics.Tweens.Core;

namespace Frolics.Tweens.Extensions {
	public static class Virtual {
		public static Tween TweenFloat(float target, float duration, Func<float> getter, Action<float> setter) {
			return TweenManager.GetInstance().GetTweenFactory().TweenFloat(target, duration, getter, setter);
		}
	}
}
