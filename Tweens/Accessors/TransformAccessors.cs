using Frolics.Tweens.Types;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct Position : IPropertyAccessor<Transform, Vector3> {
		public Vector3 Get(Transform tweener) => tweener.position;
		public void Set(Transform tweener, Vector3 value) => tweener.position = value;
	}

	internal struct PositionX : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.position.x;
		public void Set(Transform tweener, float value) => tweener.position = tweener.position.WithX(value);
	}

	internal struct PositionY : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.position.y;
		public void Set(Transform tweener, float value) => tweener.position = tweener.position.WithY(value);
	}

	internal struct PositionZ : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.position.x;
		public void Set(Transform tweener, float value) => tweener.position = tweener.position.WithZ(value);
	}

	internal struct LocalScale : IPropertyAccessor<Transform, Vector3> {
		public Vector3 Get(Transform tweener) => tweener.localScale;
		public void Set(Transform tweener, Vector3 value) => tweener.localScale = value;
	}

	internal struct Rotation : IPropertyAccessor<Transform, Quaternion> {
		public Quaternion Get(Transform tweener) => tweener.rotation;
		public void Set(Transform tweener, Quaternion value) => tweener.rotation = value;
	}

	internal struct LocalRotation : IPropertyAccessor<Transform, Quaternion> {
		public Quaternion Get(Transform tweener) => tweener.localRotation;
		public void Set(Transform tweener, Quaternion value) => tweener.localRotation = value;
	}

	internal struct LocalEulerAngles : IPropertyAccessor<Transform, Vector3> {
		public Vector3 Get(Transform tweener) => tweener.localEulerAngles;
		public void Set(Transform tweener, Vector3 value) => tweener.localEulerAngles = value;
	}
}
