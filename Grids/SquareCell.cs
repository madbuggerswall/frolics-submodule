using UnityEngine;

namespace Frolics.Grids {
	public class SquareCell : Cell {
		public SquareCell(Vector3 position, float diameter) : base(position, diameter) {
			this.position = position;
			this.diameter = diameter;
		}

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			bool inHorizontally = position.x - radius <= point.x && point.x <= position.x + radius;
			bool inVertically = position.y - radius <= point.y && point.y <= position.y + radius;

			return inHorizontally && inVertically;
		}
	}
}
