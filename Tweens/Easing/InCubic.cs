namespace Frolics.Tweens.Easing {
	public class InCubic : EaseFunction {
		public override float Evaluate(float time) {
			return time * time * time;
		}
	}
}