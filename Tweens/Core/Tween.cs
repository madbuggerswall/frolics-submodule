using System;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	// TODO Sequence : ISequence
	public abstract class Tween {
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		private float elapsedTime;
		protected float easedTime;
		protected float duration;

		protected Tween() => Reset();

		protected abstract void UpdateTween(float easedTime);
		protected abstract void SampleInitialState();
		internal abstract void Recycle(ITweenPool pool);

		internal void Reset() {
			elapsedTime = 0;
			easedTime = 0;
			duration = 1f;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);
		}

		internal void UpdateProgress(float deltaTime) {
			elapsedTime += deltaTime;
			easedTime = easeFunction(Mathf.Clamp01(elapsedTime / duration));
			UpdateTween(easedTime);

			if (easedTime >= 1)
				onCompleteCallback?.Invoke();
		}

		internal bool IsCompleted() => easedTime >= 1;

		// TODO This is the same with !IsCompleted, instead check the play bool
		internal bool IsPlaying() => easedTime is > 0 and < 1;


		// Interface
		public void Play() {
			// IDEA Fire an event or set a bool flag 
		}

		// IDEA Stop might add a flag instead
		// IDEA Complete method would invoke the callback and this wouldn't
		public void Stop(bool invokeCallback = false) {
			easedTime = 1;
			if (invokeCallback)
				onCompleteCallback?.Invoke();
		}

		public void Rewind() {
			easedTime = 0;
			elapsedTime = 0;
			SampleInitialState();
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
