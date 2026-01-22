using System;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	public abstract class Tween {
		public enum CycleType { Restart, Reflect }

		// TODO Rename to Fixed and Scaled
		protected internal enum UpdatePhase { Normal, Physics }

		private CycleType cycleType;
		private Action onCompleteCallback;
		private Func<float, float> easeFunction;

		private bool includeDelay;
		private float delay;

		private int cyclesCompleted;
		private int cycleCount;
		private bool easeSymmetry;

		private float elapsedTime;
		private float easedTime;
		protected float duration;

		private bool isStopped;
		private bool isActive;
		private bool isInitialized;
		protected bool isTargetValid;

		protected UpdatePhase updatePhase;

		protected Tween() => Reset();

		protected abstract void SampleInitialState();
		protected abstract void UpdateTween(float easedTime);
		internal abstract bool IsTargetAlive();
		internal abstract void Recycle(ITweenPool pool);

		internal void Reset() {
			includeDelay = false;
			delay = 0f;

			cyclesCompleted = 0;
			cycleCount = 1;
			easeSymmetry = false;

			elapsedTime = 0;
			easedTime = 0;
			duration = 1f;

			isStopped = false;
			isActive = false;
			isInitialized = false;
			isTargetValid = true;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);

			updatePhase = UpdatePhase.Normal;
		}

		internal void UpdateProgress(float deltaTime) {
			// If the tween has already been stopped, exit early
			if (isStopped)
				return;

			// If the target object is no longer valid, stop the tween
			if (!IsTargetAlive()) {
				isStopped = true;
				isTargetValid = false;
				return;
			}

			// Activate the tween on its first update call
			if (!isActive)
				isActive = true;

			// Wait until the delay has elapsed
			elapsedTime += deltaTime;
			if (elapsedTime - delay <= 0f)
				return;

			// First valid frame after delay: sample initial state
			if (!isInitialized) {
				SampleInitialState();
				isInitialized = true;
			}

			// Normalized time
			float time = Mathf.Clamp01((elapsedTime - delay) / duration);

			// Is reflection cycle
			bool reflect = cycleType == CycleType.Reflect && cyclesCompleted % 2 == 1;

			// EaseSymmetry -> Mirror the input domain : Mirror the output curve
			easedTime = easeSymmetry ? easeFunction(reflect ? 1 - time : time) : easeFunction(time);
			UpdateTween(easeSymmetry ? easedTime : reflect ? 1 - easedTime : easedTime);

			if (time < 1f)
				return;

			// Increment cycle count once a full duration has completed
			cyclesCompleted++;
			if (cycleCount < 0 || cycleCount - cyclesCompleted > 0) {
				elapsedTime = includeDelay ? 0f : delay;
				return;
			}

			Complete();
		}

		internal UpdatePhase GetUpdatePhase() => updatePhase;
		internal float GetDuration() => duration;
		internal float GetCycleCount() => cycleCount;
		internal bool IsStopped() => isStopped;
		internal bool IsTargetValid() => isTargetValid;

		// Interface
		public void Play() {
			TweenManager.GetInstance().Register(this);
		}

		public bool IsPlaying() {
			return isActive && !isStopped;
		}

		public void Stop() {
			isStopped = true;
		}

		public void Complete() {
			isStopped = true;
			onCompleteCallback?.Invoke();
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

		public void SetCycles(
			CycleType cycleType,
			int cycleCount,
			bool includeDelay = false,
			bool easeSymmetry = false
		) {
			this.cycleType = cycleType;
			this.cycleCount = cycleCount;
			this.includeDelay = includeDelay;
			// TODO This should be  CycleType.Rewind 
			this.easeSymmetry = easeSymmetry;
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}
	}
}
