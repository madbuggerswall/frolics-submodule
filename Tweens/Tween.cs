using System;
using Frolics.Contexts;
using Frolics.Tweens.Easing;
using UnityEngine;

// TODO Needs a tween pool
namespace Frolics.Tweens {
	public abstract class Tween {
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		protected float easedTime;
		private float elapsed;
		private readonly float duration;

		// Dependencies
		protected readonly TweenManager tweenManager;

		private Tween() {
			elapsed = 0;
			easedTime = 0;
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
			easedTime = 0;
			elapsed = 0;
			SampleInitialState();
		}

		public void Stop(bool invokeCallback = false) {
			easedTime = 1;
			if (invokeCallback)
				onCompleteCallback?.Invoke();
		}


		public void SetDelay(float delay) {
			throw new NotImplementedException();
		}

		public void SetEase(Ease.Type easeType) {
			this.easeFunction = Ease.Get(easeType);
		}

		// Creates a delegate instance pointing to the instance method Evaluate of the specific animationCurve object.
		// Hence, no closure
		public void SetEase(AnimationCurve animationCurve) {
			this.easeFunction = animationCurve.Evaluate;
		}

		// TODO int cycles = -1 (infinity)
		public void SetRepeat() {
			throw new NotImplementedException();
		}

		public void SetOnComplete(Action callback) {
			this.onCompleteCallback = callback;
		}

		// TODO float normalizedTime/time, Action callback
		public void InsertCallback() {
			throw new NotImplementedException();
		}


		// Tween operations
		internal void UpdateProgress(float deltaTime) {
			elapsed += deltaTime;

			// IDEA Rename progress to easedTime
			// IDEA Make normalizedTime a member field
			float normalizedTime = Mathf.Clamp01(elapsed / duration);
			easedTime = easeFunction(normalizedTime);

			UpdateTween();

			// IDEA Callback can be called from TweenManager
			if (easedTime >= 1)
				onCompleteCallback?.Invoke();
		}

		internal bool IsCompleted() { return easedTime >= 1; }
		internal bool IsPlaying() { return easedTime is > 0 and < 1; }

		protected abstract void UpdateTween();
		protected abstract void SampleInitialState();
	}
}
