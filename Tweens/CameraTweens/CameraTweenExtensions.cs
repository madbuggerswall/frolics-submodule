using UnityEngine;

namespace Frolics.Tweens.CameraTweens {
	public static class CameraTweenExtensions {
		public static PropertyTween<Camera, float> TweenOrthoSize(this Camera cam, float target, float duration) {
			return new PropertyTween<Camera, float>(
				cam,
				getter: c => c.orthographicSize,
				setter: (c, v) => c.orthographicSize = v,
				target: target,
				duration: duration,
				lerp: Mathf.Lerp
			);
		}
	}
}
