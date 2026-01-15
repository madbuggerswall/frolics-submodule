namespace Frolics.Tweens.Types {
	internal interface ILerp<TValue> {
		public TValue Evaluate(TValue start, TValue end, float t);
	}
}
