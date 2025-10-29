using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Tweens {
	public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
		private static T instance;

		protected virtual void Awake() {
			AssetSingleton();
		}

		private void AssetSingleton() {
			if (instance != null && instance != this) {
				Destroy(gameObject);
				return;
			}

			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}

		public static T GetInstance() => instance;
	}

	public class TweenManager : SingletonMono<TweenManager> {
		private List<Tween> tweens;
		private List<Tween> rigidbodyTweens;

		private TweenFactory tweenFactory;

		protected override void Awake() {
			base.Awake();
			tweenFactory = new TweenFactory();
		}

		private void Update() {
			UpdateTweens(tweens);
		}

		private void FixedUpdate() {
			UpdateTweens(rigidbodyTweens);
		}

		internal void AddTween(Tween tween) {
			// TODO
			tweens.Add(tween);
		}

		private void UpdateTweens(List<Tween> tweens) {
			for (int i = tweens.Count - 1; i >= 0; i--) {
				Tween tween = tweens[i];

				// Remove completed tween efficiently by swapping with last and popping
				if (!tween.IsCompleted()) {
					tween.UpdateProgress(Time.deltaTime);
				} else {
					int lastIndex = tweens.Count - 1;
					tweens[i] = tweens[lastIndex];
					tweens.RemoveAt(lastIndex);
				}
			}
		}

		internal TweenFactory GetTweenFactory() => tweenFactory;
	}
}
