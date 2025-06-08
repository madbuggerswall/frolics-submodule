using System.Collections.Generic;
using Frolics.Contexts;
using UnityEngine;

// TODO Needs a tween pool
namespace Frolics.Tweens {
	public class TweenManager : MonoBehaviour, IInitializable {
		private List<Tween> tweens;

		public void Initialize() {
			tweens = new List<Tween>();
		}

		private void Update() {
			UpdateAllTweens();
		}

		internal void AddTween(Tween tween) {
			tweens.Add(tween);
		}

		private void UpdateAllTweens() {
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
	}
}
