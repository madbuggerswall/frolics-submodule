using Frolics.Tweens.Core;
using UnityEngine;

namespace Frolics.Tweens.Extensions {
	internal static class CameraTweenExtensions {
		public static Tween TweenOrthoSize(this Camera cam, float target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenOrthoSize(cam, target, duration);
		}
	}
}
