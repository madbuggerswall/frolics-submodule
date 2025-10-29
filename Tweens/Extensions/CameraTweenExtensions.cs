using UnityEngine;

namespace Frolics.Tweens.CameraTweens {
	public static class CameraTweenExtensions {
		public static PropertyTween<Camera, float> TweenOrthoSize(this Camera cam, float target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenOrthoSize(cam, target, duration);
		}
	}
}
