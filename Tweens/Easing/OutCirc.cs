using UnityEngine;

namespace Frolics.Tweens.Easing {
	public class OutCirc : EaseFunction {
		public override float Evaluate(float time) {
			return Mathf.Sqrt(1 - (time - 1) * (time - 1));
		}
	}
}