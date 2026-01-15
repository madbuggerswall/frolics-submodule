using Frolics.Tweens.Types;
using UnityEngine.Animations;
using ParentConstraint = Frolics.Utilities.ParentConstraint;

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
	internal struct ParentConstraintWeight : IPropertyAccessor<ParentConstraint, float> {
		public float Get(ParentConstraint tweener) => tweener.GetWeight();
		public void Set(ParentConstraint tweener, float value) => tweener.SetWeight(value);
	}
}
