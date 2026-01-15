using Frolics.Tweens.Types;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct SpriteRendererColor : IPropertyAccessor<SpriteRenderer, Color> {
		public Color Get(SpriteRenderer tweener) => tweener.color;
		public void Set(SpriteRenderer tweener, Color value) => tweener.color = value;
	}

	internal struct SpriteRendererAlpha : IPropertyAccessor<SpriteRenderer, float> {
		public float Get(SpriteRenderer tweener) => tweener.color.a;
		public void Set(SpriteRenderer tweener, float value) => tweener.color.WithAlpha(value);
	}
}
