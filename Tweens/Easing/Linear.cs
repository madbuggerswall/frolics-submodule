namespace Frolics.Tweens.Easing {
	public class Linear : EaseFunction {
		public override float Evaluate(float time) {
			return time;
		}
	}
}