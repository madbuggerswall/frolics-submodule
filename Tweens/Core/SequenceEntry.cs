namespace Frolics.Tweens.Core {
	internal readonly struct SequenceEntry {
		public readonly Tween tween;
		public readonly float startTime;
		public readonly float endTime;

		public SequenceEntry(Tween tween, float startTime) {
			this.tween = tween;
			this.startTime = startTime;
			this.endTime = startTime + tween.GetDuration() * tween.GetCycleCount();
		}
	}
}
