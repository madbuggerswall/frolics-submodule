using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Grids {
	public class CircleCell : Cell {
		protected CircleCell(Vector3 position, float diameter) : base(position, diameter) { }

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			float radiusSquared = radius * radius;

			// Disregard Z since it's a 2D operation
			float distanceSquared = (position.GetXY() - point.GetXY()).sqrMagnitude;
			return distanceSquared <= radiusSquared;
		}
	}
}
