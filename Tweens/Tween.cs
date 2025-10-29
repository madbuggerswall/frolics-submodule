using System;
using Frolics.Tweens.Easing;
using UnityEngine;


namespace Frolics.Tweens {
	// TODO Sequence : ISequence
	public class Tween : ITween {
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		protected float normalizedTime;
		private float elapsedTime;
		protected float duration;

		public Tween() => Reset();

		// ITween
		public void Reset() {
			elapsedTime = 0;
			normalizedTime = 0;
			duration = 1f;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);
		}

		public void Recycle(ITweenPool pool) {
			pool.Despawn(this);
		}

		public void Play() {
			// IDEA Fire an event or set a bool flag 
		}

		// IDEA Stop might add a flag instead
		// IDEA Complete method would invoke the callback and this wouldn't
		public void Stop(bool invokeCallback = false) {
			normalizedTime = 1;
			if (invokeCallback)
				onCompleteCallback?.Invoke();
		}

		public void Rewind() {
			normalizedTime = 0;
			elapsedTime = 0;
			SampleInitialState();
		}

		public void SetDuration(float duration) {
			this.duration = duration;
		}

		// This might be done by adding an empty tween to a sequence
		public void SetDelay(float delay) {
			throw new NotImplementedException();
		}

		public void SetEase(Ease.Type easeType) {
			easeFunction = Ease.Get(easeType);
		}

		// Creates a delegate instance pointing to the instance method Evaluate. Hence, no closure
		public void SetEase(AnimationCurve animationCurve) {
			easeFunction = animationCurve.Evaluate;
		}

		// TODO Cycle type
		public void SetRepeat(int cycleCount) {
			throw new NotImplementedException();
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}

		// TODO float normalizedTime/time, Action callback
		// TODO Stop method should also invoke the inserted callbacks
		public void InsertCallback(float time, Action callback) {
			// IDEA Check an ordered list of (time, callback) tuples
			// IDEA Check the first item and continue to the next one once the call time is reached
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

		protected virtual void UpdateTween() { }
		protected virtual void SampleInitialState() { }
	}
}
