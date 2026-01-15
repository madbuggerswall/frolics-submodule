using Frolics.Tweens.Types;
using Frolics.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct TextColor : IPropertyAccessor<TextMeshPro, Color> {
		public Color Get(TextMeshPro tweener) => tweener.color;
		public void Set(TextMeshPro tweener, Color value) => tweener.color = value;
	}

	internal struct TextAlpha : IPropertyAccessor<TextMeshPro, float> {
		public float Get(TextMeshPro tweener) => tweener.color.a;
		public void Set(TextMeshPro tweener, float value) => tweener.color.WithAlpha(value);
	}
}
