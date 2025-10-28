using System;
using Frolics.Contexts;
using Frolics.Tweens.Easing;
using UnityEngine;


namespace Frolics.Tweens {
	/// <summary>
	/// A generic tween that interpolates a specific property of a target object.
	/// Avoids closures by storing the target reference and using a strongly typed
	/// apply function.
	/// </summary>
	public class PropertyTween<TTarget, TValue> : Tween where TTarget : UnityEngine.Object {
		private readonly TTarget target;
		private readonly Func<TTarget, TValue> getter;
		private readonly Action<TTarget, TValue> setter;
		private readonly Func<TValue, TValue, float, TValue> lerp;

		private TValue start;
		private readonly TValue end;

		public PropertyTween(
			TTarget target,
			Func<TTarget, TValue> getter,
			Action<TTarget, TValue> setter,
			TValue end,
			float duration,
			Func<TValue, TValue, float, TValue> lerp
		) : base(duration) {
			this.target = target;
			this.getter = getter;
			this.setter = setter;
			this.end = end;
			this.lerp = lerp;

			start = getter(target);
		}

		protected override void UpdateTween() {
			setter(target, lerp(start, end, normalizedTime));
		}

		protected override void SampleInitialState() {
			start = getter(target);
		}
	}

	// TODO Needs a tween pool
	public abstract class Tween {
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		protected float normalizedTime;
		private float elapsedTime;
		private readonly float duration;

		// Dependencies
		protected readonly TweenManager tweenManager;

		private Tween() {
			elapsedTime = 0;
			normalizedTime = 0;
			duration = 1;
			easeFunction = Ease.Get(Ease.Type.Linear);

			// NOTE Handle this differently
			tweenManager = SceneContext.GetInstance().Get<TweenManager>();
		}

		protected Tween(float duration) : this() {
			this.duration = duration;
		}

		public virtual void Play() {
			Rewind();
			tweenManager.AddTween(this);
		}

		protected void Rewind() {
			normalizedTime = 0;
			elapsedTime = 0;
			SampleInitialState();
		}

		public void Stop(bool invokeCallback = false) {
			normalizedTime = 1;
			if (invokeCallback)
				onCompleteCallback?.Invoke();
		}


		public void SetDelay(float delay) {
			throw new NotImplementedException();
		}

		public void SetEase(Ease.Type easeType) {
			easeFunction = Ease.Get(easeType);
		}

		// Creates a delegate instance pointing to the instance method Evaluate of the specific animationCurve object.
		// Hence, no closure
		public void SetEase(AnimationCurve animationCurve) {
			easeFunction = animationCurve.Evaluate;
		}

		// TODO int cycles = -1 (infinity)
		public void SetRepeat() {
			throw new NotImplementedException();
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}

		// TODO float normalizedTime/time, Action callback
		// TODO Stop method should also invoke the inserted callbacks
		public void InsertCallback() {
			throw new NotImplementedException();
		}


		// Tween operations
		internal void UpdateProgress(float deltaTime) {
			elapsedTime += deltaTime;
			normalizedTime = easeFunction(Mathf.Clamp01(elapsedTime / duration));

			UpdateTween();

			// IDEA Callback can be called from TweenManager
			if (normalizedTime >= 1)
				onCompleteCallback?.Invoke();
		}

		internal bool IsCompleted() { return normalizedTime >= 1; }
		internal bool IsPlaying() { return normalizedTime is > 0 and < 1; }

		protected abstract void UpdateTween();
		protected abstract void SampleInitialState();
	}
}
