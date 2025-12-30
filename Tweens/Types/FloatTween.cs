using System;
using Frolics.Tweens.Core;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Types {
	internal sealed class FloatTween : Tween {
		private Func<float> getter;
		private Action<float> setter;

		private float initial;
		private float target;

		public FloatTween() { }

		internal void Configure(float target, float duration, Func<float> getter, Action<float> setter) {
			this.duration = duration;

			this.getter = getter;
			this.setter = setter;

			this.initial = getter();
			this.target = target;

			// TODO Physics mode option in Configure 
			this.updatePhase = UpdatePhase.Normal;
		}

		protected override void SampleInitialState() {
			this.initial = getter();
		}

		protected override void UpdateTween(float easedTime) {
			setter(Mathf.Lerp(initial, target, easedTime));
		}

		internal override bool IsTargetAlive() {
			return getter is not null && setter is not null;
		}

		internal override void Recycle(ITweenPool pool) {
			getter = null;
			setter = null;
			Reset();
			pool.Despawn(this);
		}
	}
}
