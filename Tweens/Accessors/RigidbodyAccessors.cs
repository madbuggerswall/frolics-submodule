using Frolics.Tweens.Types;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct MovePosition : IPropertyAccessor<Rigidbody, Vector3> {
		public Vector3 Get(Rigidbody tweener) => tweener.position;
		public void Set(Rigidbody tweener, Vector3 value) => tweener.MovePosition(value);
	}

	internal struct MoveRotation : IPropertyAccessor<Rigidbody, Quaternion> {
		public Quaternion Get(Rigidbody tweener) => tweener.rotation;
		public void Set(Rigidbody tweener, Quaternion value) => tweener.MoveRotation(value);
	}
}
