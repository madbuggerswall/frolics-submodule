using System;
using System.Collections.Generic;
using Frolics.Tweens.Factory;
using Frolics.Tweens.Pooling;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Tweens.Core {
	internal class TweenManager : PersistentSingletonMono<TweenManager> {
		private List<Tween> normalTweens;
		private List<Tween> physicsTweens;

		private ITweenPool tweenPool;
		private TweenFactory tweenFactory;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void OnAfterAssembliesLoaded() {
			GameObject gameObject = new($"{nameof(TweenManager)} (Auto-Generated)");
			gameObject.AddComponent<TweenManager>();
		}

		protected override void Awake() {
			base.Awake();

			normalTweens = new List<Tween>();
			physicsTweens = new List<Tween>();

			tweenPool = new TweenPool();
			tweenFactory = new TweenFactory(tweenPool);
		}

		private void Update() => UpdateTweens(normalTweens, Time.deltaTime);
		private void FixedUpdate() => UpdateTweens(physicsTweens, Time.fixedDeltaTime);

		internal void Register(Tween tween) {
			Tween.UpdatePhase updatePhase = tween.GetUpdatePhase();
			if (updatePhase is Tween.UpdatePhase.Normal)
				normalTweens.Add(tween);
			else if (updatePhase is Tween.UpdatePhase.Physics)
				physicsTweens.Add(tween);
			else
				throw new ArgumentException();
		}

		private void UpdateTweens(List<Tween> tweens, float deltaTime) {
			for (int i = tweens.Count - 1; i >= 0; i--) {
				Tween tween = tweens[i];

				// Remove completed tween efficiently by swapping with last and popping
				if (!tween.IsStopped()) {
					tween.UpdateProgress(deltaTime);
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
