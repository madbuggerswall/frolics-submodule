using System.Collections.Generic;
using Frolics.Tweens.Pooling;
using UnityEngine;

namespace Frolics.Tweens.Core {
	public sealed class Sequence : Tween {
		private readonly List<SequenceEntry> entries = new();
		private float totalDuration;

		public Sequence() { }

		public void Append(Tween tween) {
			SequenceEntry entry = new(tween, totalDuration);
			entries.Add(entry);

			totalDuration = entry.endTime;
			duration = totalDuration;

			// IDEA Might be redundant
			updatePhase = tween.GetUpdatePhase();
		}

		public void Join(Tween tween) {
			float start = entries.Count > 0 ? entries[^1].startTime : 0f;
			SequenceEntry entry = new(tween, start);
			entries.Add(entry);

			totalDuration = Mathf.Max(totalDuration, entry.endTime);
			duration = totalDuration;

			// IDEA Might be redundant
			updatePhase = tween.GetUpdatePhase();
		}

		// Tween
		// Sequences don’t have a single target; children will sample themselves on their first valid frame
		protected override void SampleInitialState() { }

		protected override void UpdateTween(float easedTime) {
			float sequenceTime = easedTime * duration;
			float deltaTime = updatePhase is UpdatePhase.Normal ? Time.deltaTime : Time.fixedDeltaTime;
			for (int i = 0; i < entries.Count; i++) {
				if (sequenceTime < entries[i].startTime)
					continue;

				Tween tween = entries[i].tween;
				tween.UpdateProgress(deltaTime);
				isTargetValid = isTargetValid && tween.IsTargetValid();
			}
		}

		internal override bool IsTargetAlive() => isTargetValid;


		internal override void Recycle(ITweenPool pool) {
			for (int i = 0; i < entries.Count; i++)
				entries[i].tween.Recycle(pool);

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
