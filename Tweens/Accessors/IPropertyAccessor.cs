namespace Frolics.Tweens.Types {
	internal interface IPropertyAccessor<in TTweener, TValue> {
		public TValue Get(TTweener tweener);
		public void Set(TTweener tweener, TValue value);
	}
}
