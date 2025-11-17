using UnityEngine;

namespace Frolics.Grids {
	public class DefaultSquareGrid : SquareGrid<SquareCell> {
		private readonly Vector3 pivotPoint;

		public DefaultSquareGrid(Vector3 pivotPoint, Vector2Int gridSize, float cellDiameter, GridPlane gridPlane) :
			base(gridSize, cellDiameter, gridPlane, new DefaultSquareCellFactory()) {
			this.pivotPoint = pivotPoint;
		}

		public override Vector3 GetPivotPoint() => pivotPoint;
	}
}
