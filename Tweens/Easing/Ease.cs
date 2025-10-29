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
