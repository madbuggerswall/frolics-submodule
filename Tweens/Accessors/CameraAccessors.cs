using Frolics.Tweens.Types;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct OrthoSize : IPropertyAccessor<Camera, float> {
		public float Get(Camera tweener) => tweener.orthographicSize;
		public void Set(Camera tweener, float value) => tweener.orthographicSize = value;
	}
}
