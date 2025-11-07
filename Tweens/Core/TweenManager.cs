using System.Collections.Generic;
using Frolics.Tweens.Factory;
using Frolics.Tweens.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Tweens.Core {
	internal class TweenManager : SingletonMono<TweenManager> {
		private List<Tween> tweens;
		private List<Tween> rigidbodyTweens;

		private ITweenPool tweenPool;
		private TweenFactory tweenFactory;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void OnAfterAssembliesLoaded() {
			GameObject gameObject = new($"{nameof(TweenManager)} (Auto-Generated)");
			gameObject.AddComponent<TweenManager>();
		}

		protected override void Awake() {
			base.Awake();

			tweens = new List<Tween>();
			rigidbodyTweens = new List<Tween>();

			tweenPool = new TweenPool();
			tweenFactory = new TweenFactory(tweenPool, AddTween);
		}

		private void Update() {
			UpdateTweens(tweens);
		}

		private void FixedUpdate() {
			UpdateTweens(rigidbodyTweens);
		}

		private void AddTween(Tween tween) {
			if (tween.GetTweener() is Rigidbody)
				rigidbodyTweens.Add(tween);
			else
				tweens.Add(tween);
		}

		private void UpdateTweens(List<Tween> tweens) {
			for (int i = tweens.Count - 1; i >= 0; i--) {
				Tween tween = tweens[i];

				// Remove completed tween efficiently by swapping with last and popping
				if (!tween.IsCompleted() || !tween.IsStopped()) {
					tween.UpdateProgress(Time.deltaTime);
				} else {
					tweens[i].Recycle(tweenPool);

					int lastIndex = tweens.Count - 1;
					tweens[i] = tweens[lastIndex];
					tweens.RemoveAt(lastIndex);
				}
			}
		}

		internal TweenFactory GetTweenFactory() => tweenFactory;
	}
}
