using System;
using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Types {
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
			TValue target,
			float duration,
			Func<TTweener, TValue> getter,
			Action<TTweener, TValue> setter,
			Func<TValue, TValue, float, TValue> lerp
		) {
			Configure(tweener, target, duration, getter, setter, lerp);
		}

		internal void Configure(
			TTweener tweener,
			TValue target,
			float duration,
			Func<TTweener, TValue> getter,
			Action<TTweener, TValue> setter,
			Func<TValue, TValue, float, TValue> lerp
		) {
			this.duration = duration;

			this.tweener = tweener;
			this.getter = getter;
			this.setter = setter;
			this.lerp = lerp;

			this.initial = getter(tweener);
			this.target = target;
			this.updatePhase = tweener is Rigidbody ? UpdatePhase.Physics : UpdatePhase.Normal;
		}

		protected override void SampleInitialState() {
			this.initial = getter(tweener);
		}

		protected override void UpdateTween(float easedTime) {
			setter(tweener, lerp(initial, target, easedTime));
		}

		internal override bool IsTargetAlive() {
			return tweener != null;
		}

		internal override void Recycle(ITweenPool pool) {
			tweener = null;
			getter = null;
			setter = null;
			lerp = null;
			Reset();
			pool.Despawn(this);
		}
	}
}
