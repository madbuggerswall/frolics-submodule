using System;
using Frolics.Tweens.Easing;
using UnityEngine;


namespace Frolics.Tweens {
	// TODO Sequence : ISequence
	public abstract class Tween {
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		private float elapsedTime;
		protected float normalizedTime;
		protected float duration;

		protected Tween() => Reset();

		protected abstract void UpdateTween();
		protected abstract void SampleInitialState();
		internal abstract void Recycle(ITweenPool pool);

		internal void Reset() {
			elapsedTime = 0;
			normalizedTime = 0;
			duration = 1f;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);
		}

		internal void UpdateProgress(float deltaTime) {
			elapsedTime += deltaTime;
			normalizedTime = easeFunction(Mathf.Clamp01(elapsedTime / duration));

			UpdateTween();

			// IDEA Callback can be called from TweenManager
			if (normalizedTime >= 1)
				onCompleteCallback?.Invoke();
		}
		
		internal bool IsCompleted() => normalizedTime >= 1;

		// TODO This is the same with !IsCompleted, instead check the play bool
		internal bool IsPlaying() => normalizedTime is > 0 and < 1;


		// Interface
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
	}
}
