using Frolics.Tweens.Types;
using Frolics.Utilities;
using UnityEngine.Animations;

namespace Frolics.Tweens.Accessors {
	internal struct PositionConstraintWeight : IPropertyAccessor<PositionConstraint, float> {
		public float Get(PositionConstraint tweener) => tweener.weight;
		public void Set(PositionConstraint tweener, float value) => tweener.weight = value;
	}

	internal struct RotationConstraintWeight : IPropertyAccessor<RotationConstraint, float> {
		public float Get(RotationConstraint tweener) => tweener.weight;
		public void Set(RotationConstraint tweener, float value) => tweener.weight = value;
	}

	// Frolics.Utilities.ParentConstraintWeight
	internal struct SoftParentConstraintWeight : IPropertyAccessor<SoftParentConstraint, float> {
		public float Get(SoftParentConstraint tweener) => tweener.GetWeight();
		public void Set(SoftParentConstraint tweener, float value) => tweener.SetWeight(value);
	}
}
