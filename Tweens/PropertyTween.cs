using System;

namespace Frolics.Tweens {
	/// <summary>
	/// A generic tween that interpolates a specific property of a target object.
	/// Avoids closures by storing the target reference and using a strongly typed
	/// apply function.
	/// </summary>
	internal sealed class PropertyTween<TTweener, TValue> : Tween where TTweener : UnityEngine.Object {
		private TTweener tweener;

		private Func<TTweener, TValue> getter;
		private Action<TTweener, TValue> setter;
		private Func<TValue, TValue, float, TValue> lerp;

		private TValue initial;
		private TValue target;

		public PropertyTween() { }

		internal PropertyTween(
			TTweener tweener,
			Func<TTweener, TValue> getter,
			Action<TTweener, TValue> setter,
			TValue target,
			float duration,
			Func<TValue, TValue, float, TValue> lerp
		) {
			Configure(tweener, getter, setter, target, duration, lerp);
		}

		internal void Configure(
			TTweener tweener,
			Func<TTweener, TValue> getter,
			Action<TTweener, TValue> setter,
			TValue target,
			float duration,
			Func<TValue, TValue, float, TValue> lerp
		) {
			this.duration = duration;

			this.tweener = tweener;
			this.getter = getter;
			this.setter = setter;
			this.lerp = lerp;

			this.initial = getter(tweener);
			this.target = target;
		}

		protected override void UpdateTween() {
			setter(tweener, lerp(initial, target, normalizedTime));
		}

		protected override void SampleInitialState() {
			initial = getter(tweener);
		}

		internal override void Recycle(ITweenPool pool) {
			Reset();
			pool.Despawn(this);
		}
	}
}
