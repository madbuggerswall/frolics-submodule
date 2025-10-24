using UnityEngine;

namespace Frolics.Grids {
	public class DefaultSquareGrid : SquareGrid<SquareCell> {
		public DefaultSquareGrid(Vector3 pivotPoint, Vector2Int gridSize, float cellDiameter, GridPlane gridPlane) :
			base(pivotPoint, gridSize, cellDiameter, gridPlane, new DefaultSquareCellFactory()) { }
	}
}