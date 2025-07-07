using UnityEngine;

namespace Frolics.Tweens.SpriteRendererTweens {
	public class ColorAlphaTween : Tween {
		private readonly SpriteRenderer tweener;
		private readonly (Color initial, Color target) color;

		public ColorAlphaTween(SpriteRenderer tweener, float targetAlpha, float duration) : base(duration) {
			this.tweener = tweener;
			this.color.initial = tweener.color;
			this.color.target = tweener.color;
			this.color.target.a = targetAlpha;
		}

		protected override void UpdateTween() {
			tweener.color = Color.Lerp(color.initial, color.target, progress);
		}
	}
}
