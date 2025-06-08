using UnityEngine;

namespace Frolics.Tweens.Easing {
	public class InOutSine : EaseFunction {
		public override float Evaluate(float time) {
			return -(Mathf.Cos(Mathf.PI * time) - 1) / 2;
		}
	}
}