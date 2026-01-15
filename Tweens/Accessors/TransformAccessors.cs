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
		public float Get(Transform tweener) => tweener.position.z;
		public void Set(Transform tweener, float value) => tweener.position = tweener.position.WithZ(value);
	}

	internal struct LocalPosition : IPropertyAccessor<Transform, Vector3> {
		public Vector3 Get(Transform tweener) => tweener.localPosition;
		public void Set(Transform tweener, Vector3 value) => tweener.localPosition = value;
	}

	internal struct LocalPositionX : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.localPosition.x;
		public void Set(Transform tweener, float value) => tweener.localPosition = tweener.localPosition.WithX(value);
	}

	internal struct LocalPositionY : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.localPosition.y;
		public void Set(Transform tweener, float value) => tweener.localPosition = tweener.localPosition.WithY(value);
	}

	internal struct LocalPositionZ : IPropertyAccessor<Transform, float> {
		public float Get(Transform tweener) => tweener.localPosition.z;
		public void Set(Transform tweener, float value) => tweener.localPosition = tweener.localPosition.WithZ(value);
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
