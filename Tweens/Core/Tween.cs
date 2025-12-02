using System;
using System.Collections.Generic;
using Frolics.Tweens.Easing;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	internal readonly struct SequenceEntry {
		public readonly Tween tween;
		public readonly float startTime; // IDEA rename to startOffset
		public readonly float duration; // IDEA Might be redundant

		public SequenceEntry(Tween tween, float startTime) {
			this.tween = tween;
			this.startTime = startTime;
			this.duration = tween.GetDuration();
		}

		public float GetEndTime() => startTime + duration;
	}

	public sealed class Sequence : Tween {
		private readonly List<SequenceEntry> entries = new();
		private float totalDuration;

		public Sequence Append(Tween tween) {
			if (tween is null)
				throw new ArgumentNullException(nameof(tween));

			SequenceEntry entry = new SequenceEntry(tween, totalDuration);
			entries.Add(entry);

			totalDuration = entry.GetEndTime();
			duration = totalDuration;

			updatePhase = tween.GetUpdatePhase();
			return this;
		}

		public Sequence Join(Tween tween) {
			if (tween is null)
				throw new ArgumentNullException(nameof(tween));

			float start = entries.Count > 0 ? entries[^1].startTime : 0f;
			SequenceEntry entry = new SequenceEntry(tween, start);
			entries.Add(entry);

			totalDuration = Mathf.Max(totalDuration, entry.GetEndTime());
			duration = totalDuration;

			updatePhase = tween.GetUpdatePhase();
			return this;
		}

		// Tween
		protected override void UpdateTween(float easedTime) {
			float sequenceTime = easedTime * duration;
			float deltaTime = updatePhase is UpdatePhase.Normal ? Time.deltaTime : Time.fixedDeltaTime;
			for (int i = 0; i < entries.Count; i++) {
				SequenceEntry entry = entries[i];
				if (sequenceTime >= entry.startTime && sequenceTime <= entry.GetEndTime())
					entry.tween.UpdateProgress(deltaTime);
			}
		}

		internal override bool IsTargetAlive() {
			for (int i = 0; i < entries.Count; i++)
				if (!entries[i].tween.IsTargetAlive())
					return false;

			return true;
		}


		internal override void Recycle(ITweenPool pool) {
			for (int i = 0; i < entries.Count; i++) {
				Tween tween = entries[i].tween;
				tween.Recycle(pool);
			}

			entries.Clear();
			totalDuration = 0;
			Reset();
			pool.Despawn(this);
		}
	}

	// TODO Sequence : ISequence
	public abstract class Tween {
		public enum CycleType { Restart, Reflected }

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

		protected UpdatePhase updatePhase;

		protected Tween() => Reset();

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

			// TODO Fix this
			isStopped = false;
			isActive = false;

			onCompleteCallback = null;
			easeFunction = Ease.Get(Ease.Type.Linear);

			updatePhase = UpdatePhase.Normal;
		}

		internal void UpdateProgress(float deltaTime) {
			if (isStopped)
				return;

			if (!IsTargetAlive()) {
				isStopped = true;
				return;
			}

			isActive = true; // IDEA Migrate to Play? 
			elapsedTime += deltaTime;
			if (elapsedTime - delay <= 0f)
				return;

			float normalizedTime = Mathf.Clamp01((elapsedTime - delay) / duration);
			bool isReflectionCycle = cycleType == CycleType.Reflected && cyclesCompleted % 2 == 1;

			if (easeSymmetry) {
				// Mirror the input domain
				easedTime = easeFunction(isReflectionCycle ? 1 - normalizedTime : normalizedTime);
				UpdateTween(easedTime);
			} else {
				// Mirror the output curve
				easedTime = easeFunction(normalizedTime);
				UpdateTween(isReflectionCycle ? 1 - easedTime : easedTime);
			}

			if (normalizedTime < 1f)
				return;

			cyclesCompleted++;
			if (cycleCount < 0 || cycleCount - cyclesCompleted > 0) {
				elapsedTime = includeDelay ? 0f : delay;
				return;
			}

			Complete();
		}

		internal UpdatePhase GetUpdatePhase() => updatePhase;
		internal float GetDuration() => duration;
		internal bool IsStopped() => isStopped;

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
			this.easeSymmetry = easeSymmetry;
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}
	}
}
