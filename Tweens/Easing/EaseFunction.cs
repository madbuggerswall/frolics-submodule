using UnityEngine;

namespace Frolics.Tweens.Easing {
	public static class EaseFunction {
		public static float OutSine(float time) {
			return Mathf.Sin(time * Mathf.PI / 2);
		}

		public static float OutQuad(float time) {
			return 1 - (1 - time) * (1 - time);
		}

		public static float Curve(AnimationCurve curve, float time) {
			return curve.Evaluate(time);
		}

		public static float InQuint(float time) {
			return time * time * time * time * time;
		}

		public static float InOutSine(float time) {
			return -(Mathf.Cos(Mathf.PI * time) - 1) / 2;
		}

		public static float OutCirc(float time) {
			return Mathf.Sqrt(1 - (time - 1) * (time - 1));
		}

		public static float OutExpo(float time) {
			return Mathf.Approximately(time, 1) ? 1 : 1 - Mathf.Pow(2, -10 * time);
		}

		public static float OutQuart(float time) {
			return 1 - (1 - time) * (1 - time) * (1 - time) * (1 - time);
		}

		public static float OutCubic(float time) {
			return 1 - (1 - time) * (1 - time) * (1 - time);
		}

		public static float InQuart(float time) {
			return time * time * time * time;
		}

		public static float OutQuint(float time) {
			return 1 - (1 - time) * (1 - time) * (1 - time) * (1 - time) * (1 - time);
		}

		public static float InSine(float time) {
			return 1 - Mathf.Cos(time * Mathf.PI / 2);
		}

		public static float Linear(float time) {
			return time;
		}

		public static float InQuad(float time) {
			return time * time;
		}

		public static float InOutQuint(float time) {
			return time < 0.5
				? 16 * time * time * time * time * time
				: 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}

		public static float InOutQuart(float time) {
			return time < 0.5
				? 8 * time * time * time * time
				: 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}

		public static float InOutQuad(float time) {
			return time < 0.5f ? 2f * time * time : 1 - (-2 * time + 2) * (-2 * time + 2) / 2;
		}

		public static float InOutExpo(float time) {
			if (Mathf.Approximately(time, 0))
				return 0;
			else if (Mathf.Approximately(time, 1))
				return 1;
			else
				return time < 0.5 ? Mathf.Pow(2, 20 * time - 10) / 2 : (2 - Mathf.Pow(2, -20 * time + 10)) / 2;
		}

		public static float InOutCubic(float time) {
			return time < 0.5 ? 4 * time * time * time : 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}

		public static float InOutCirc(float time) {
			return time < 0.5
				? (1 - Mathf.Sqrt(1 - (2 * time) * (2 * time))) / 2
				: (Mathf.Sqrt(1 - (-2 * time + 2) * (-2 * time + 2)) + 1) / 2;
		}

		public static float InExpo(float time) {
			return Mathf.Approximately(time, 0) ? 0 : Mathf.Pow(2, 10 * time - 10);
		}

		public static float InCubic(float time) {
			return time * time * time;
		}

		public static float InCirc(float time) {
			return 1f - Mathf.Sqrt(1 - time * time);
		}
	}
}
