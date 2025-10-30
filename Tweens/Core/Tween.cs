using System;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	// TODO Sequence : ISequence
	public abstract class Tween {
		public enum CycleType { Restart, Yoyo }

		private CycleType cycleType;
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		private bool includeDelay;
		private float delay;

		private int cyclesCompleted;
		private int cycleCount;

		private float elapsedTime;
		private float easedTime;
		protected float duration;

		private bool isStopped;
		private bool isCompleted;

		protected Tween() => Reset();

		protected abstract void UpdateTween(float easedTime);
		protected abstract void SampleInitialState();
		protected abstract UnityEngine.Object GetTweener();
		internal abstract void Recycle(ITweenPool pool);

		internal void Reset() {
			includeDelay = false;
			delay = 0f;

			cyclesCompleted = 0;
			cycleCount = 1;

			elapsedTime = 0;
			easedTime = 0;
			duration = 1f;

			isStopped = false;
			isCompleted = false;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);
		}

		internal void UpdateProgress(float deltaTime) {
			if (isCompleted || isStopped)
				return;

			if (GetTweener() == null) {
				isStopped = true;
				return;
			}

			elapsedTime += deltaTime;
			if (elapsedTime - delay <= 0f)
				return;

			float yoyoTime = (cycleType == CycleType.Yoyo && cyclesCompleted % 2 == 1) ? 1f : 0f;
			easedTime = easeFunction(Mathf.Clamp01((elapsedTime - delay) / duration) - yoyoTime);
			UpdateTween(easedTime);

			if (easedTime < 1)
				return;

			cyclesCompleted++;
			if (cycleCount - cyclesCompleted > 0) {
				elapsedTime = includeDelay ? 0f : delay;
				return;
			}

			Complete();
		}

		internal bool IsCompleted() => isCompleted;
		internal bool IsStopped() => isStopped;

		// Interface
		public void Play() {
			// IDEA Fire an event or set a bool flag 
		}

		public void Stop() {
			isStopped = true;
			onCompleteCallback?.Invoke();
		}

		public void Complete() {
			isCompleted = true;
			onCompleteCallback?.Invoke();
		}

		public void Rewind() {
			easedTime = 0;
			elapsedTime = 0;
			SampleInitialState();
		}

		public void SetDelay(float delay) {
			this.delay = delay;
		}

		public void SetEase(Ease.Type easeType) {
			easeFunction = Ease.Get(easeType);
		}

		// Creates a delegate instance pointing to the instance method Evaluate. Hence, no closure
		public void SetEase(AnimationCurve animationCurve) {
			easeFunction = animationCurve.Evaluate;
		}

		// TODO Cycle type
		public void SetCycles(CycleType cycleType, int cycleCount, bool includeDelay = false) {
			this.cycleType = cycleType;
			this.cycleCount = cycleCount;
			this.includeDelay = includeDelay;
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}
	}
}
