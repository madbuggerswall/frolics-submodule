namespace Frolics.Tweens.Easing {
	public class InQuart : EaseFunction {
		public override float Evaluate(float time) {
			return time * time * time * time;
		}
	}
}