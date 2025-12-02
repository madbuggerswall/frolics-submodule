using System;
using System.Collections.Generic;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	public sealed class Sequence : Tween {
		private readonly List<SequenceEntry> entries = new();
		private float totalDuration;

		public Sequence() { }

		public void Append(Tween tween) {
			if (tween is null)
				throw new ArgumentNullException(nameof(tween));

			SequenceEntry entry = new(tween, totalDuration);
			entries.Add(entry);

			totalDuration = entry.GetEndTime();
			duration = totalDuration;

			// IDEA Might be redundant
			updatePhase = tween.GetUpdatePhase();
		}

		public void Join(Tween tween) {
			if (tween is null)
				throw new ArgumentNullException(nameof(tween));

			float start = entries.Count > 0 ? entries[^1].startTime : 0f;
			SequenceEntry entry = new(tween, start);
			entries.Add(entry);

			totalDuration = Mathf.Max(totalDuration, entry.GetEndTime());
			duration = totalDuration;

			// IDEA Might be redundant
			updatePhase = tween.GetUpdatePhase();
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

		// Static Factory Method
		public static Sequence Create() {
			return TweenManager.GetInstance().GetTweenFactory().TweenSequence();
		}
	}
}
