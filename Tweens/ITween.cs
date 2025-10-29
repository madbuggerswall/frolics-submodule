using System;
using Frolics.Tweens.Easing;
using UnityEngine;

namespace Frolics.Tweens {
	public interface ITween {
		void Reset();
		void Recycle(ITweenPool pool);

		void Play();
		void Stop(bool invokeCallback = false);
		void Rewind();
		void SetDuration(float duration);
		void SetDelay(float delay);
		void SetEase(Ease.Type easeType);
		void SetEase(AnimationCurve animationCurve);
		void SetRepeat(int cycleCount);
		void SetOnComplete(Action callback);
		void InsertCallback(float time, Action callback);
	}
}