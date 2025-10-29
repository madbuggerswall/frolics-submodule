using System;

namespace Frolics.Tweens.Easing {
	public static class Ease {
		public enum Type {
			Linear,
			InQuad,
			OutQuad,
			InOutQuad,
			InCubic,
			OutCubic,
			InOutCubic,
			InQuart,
			OutQuart,
			InOutQuart,
			InQuint,
			OutQuint,
			InOutQuint,
			InSine,
			OutSine,
			InOutSine,
			InExpo,
			OutExpo,
			InOutExpo,
			InCirc,
			OutCirc,
			InOutCirc
		}

		public static float Evaluate(Type type, float time) {
			return type switch {
				Type.Linear => EaseFunction.Linear(time),
				Type.InQuad => EaseFunction.InQuad(time),
				Type.OutQuad => EaseFunction.OutQuad(time),
				Type.InOutQuad => EaseFunction.InOutQuad(time),
				Type.InCubic => EaseFunction.InCubic(time),
				Type.OutCubic => EaseFunction.OutCubic(time),
				Type.InOutCubic => EaseFunction.InOutCubic(time),
				Type.InQuart => EaseFunction.InQuart(time),
				Type.OutQuart => EaseFunction.OutQuart(time),
				Type.InOutQuart => EaseFunction.InOutQuart(time),
				Type.InQuint => EaseFunction.InQuint(time),
				Type.OutQuint => EaseFunction.OutQuint(time),
				Type.InOutQuint => EaseFunction.InOutQuint(time),
				Type.InSine => EaseFunction.InSine(time),
				Type.OutSine => EaseFunction.OutSine(time),
				Type.InOutSine => EaseFunction.InOutSine(time),
				Type.InExpo => EaseFunction.InExpo(time),
				Type.OutExpo => EaseFunction.OutExpo(time),
				Type.InOutExpo => EaseFunction.InOutExpo(time),
				Type.InCirc => EaseFunction.InCirc(time),
				Type.OutCirc => EaseFunction.OutCirc(time),
				Type.InOutCirc => EaseFunction.InOutCirc(time),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}

		public static Func<float, float> Get(Type type) {
			return type switch {
				Type.Linear => EaseFunction.Linear,
				Type.InQuad => EaseFunction.InQuad,
				Type.OutQuad => EaseFunction.OutQuad,
				Type.InOutQuad => EaseFunction.InOutQuad,
				Type.InCubic => EaseFunction.InCubic,
				Type.OutCubic => EaseFunction.OutCubic,
				Type.InOutCubic => EaseFunction.InOutCubic,
				Type.InQuart => EaseFunction.InQuart,
				Type.OutQuart => EaseFunction.OutQuart,
				Type.InOutQuart => EaseFunction.InOutQuart,
				Type.InQuint => EaseFunction.InQuint,
				Type.OutQuint => EaseFunction.OutQuint,
				Type.InOutQuint => EaseFunction.InOutQuint,
				Type.InSine => EaseFunction.InSine,
				Type.OutSine => EaseFunction.OutSine,
				Type.InOutSine => EaseFunction.InOutSine,
				Type.InExpo => EaseFunction.InExpo,
				Type.OutExpo => EaseFunction.OutExpo,
				Type.InOutExpo => EaseFunction.InOutExpo,
				Type.InCirc => EaseFunction.InCirc,
				Type.OutCirc => EaseFunction.OutCirc,
				Type.InOutCirc => EaseFunction.InOutCirc,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
	}
}
