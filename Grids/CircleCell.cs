using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Grids {
	public class CircleCell : Cell {
		// IDEA Can hold axial coordinate
		public CircleCell(Vector3 worldPosition, float diameter) : base(worldPosition, diameter) { }

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			float radiusSquared = radius * radius;

			// Disregard Z since it's a 2D operation
			float distanceSquared = (worldPosition.GetXY() - point.GetXY()).sqrMagnitude;
			return distanceSquared <= radiusSquared;
		}
	}
}
