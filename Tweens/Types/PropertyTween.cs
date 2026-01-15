using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Types {
	/// <summary>
	/// A generic tween that interpolates a specific property of a target object.
	/// Avoids closures by storing the target reference and using a strongly typed
	/// apply function.
	/// </summary>
	internal sealed class PropertyTween<TTweener, TValue, TAccessor, TLerp> : Tween
	where TTweener : UnityEngine.Object
	where TAccessor : struct, IPropertyAccessor<TTweener, TValue>
	where TLerp : struct, ILerp<TValue> {
		private TTweener tweener;
		private TValue initial;
		private TValue target;

		private TAccessor accessor;
		private TLerp lerp;

		public PropertyTween() { }

		internal void Configure(TTweener tweener, TValue target, float duration) {
			this.tweener = tweener;
			this.target = target;
			this.duration = duration;

			accessor = default;
			lerp = default;

			this.initial = accessor.Get(tweener);
			this.updatePhase = tweener is Rigidbody ? UpdatePhase.Physics : UpdatePhase.Normal;
		}

		protected override void SampleInitialState() {
			this.initial = accessor.Get(tweener);
		}

		protected override void UpdateTween(float easedTime) {
			accessor.Set(tweener, lerp.Evaluate(initial, target, easedTime));
		}

		internal override bool IsTargetAlive() => tweener != null;

		internal override void Recycle(ITweenPool pool) {
			tweener = null;
			Reset();
			pool.Despawn(this);
		}
	}
}
