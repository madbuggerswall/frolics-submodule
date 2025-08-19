using UnityEngine;

namespace Frolics.Grids {
	public abstract class Cell {
		protected Vector3 position;
		protected float diameter;

		protected Cell(Vector3 position, float diameter) {
			this.position = position;
			this.diameter = diameter;
		}

		public abstract bool IsInsideCell(Vector3 point);

		public Vector3 GetPosition() => position;
		public float GetDiameter() => diameter;
	}
}
