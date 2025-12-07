namespace Frolics.Tweens.Core {
	internal readonly struct SequenceEntry {
		public readonly Tween tween;
		public readonly float startTime; // IDEA rename to startOffset
		public readonly float duration;  // IDEA Might be redundant

		public SequenceEntry(Tween tween, float startTime) {
			this.tween = tween;
			this.startTime = startTime;
			this.duration = tween.GetDuration() * tween.GetCycleCount();
		}

		public float GetEndTime() => startTime + duration;
	}
}
