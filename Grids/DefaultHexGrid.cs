using UnityEngine;

namespace Frolics.Grids {
	public class DefaultHexGrid : HexGrid<HexCell> {
		private readonly Vector3 pivotPoint;

		public DefaultHexGrid(Vector3 pivotPoint, Vector2Int gridSize, float cellDiameter, GridPlane gridPlane) : base(
			gridSize,
			cellDiameter,
			gridPlane,
			new DefaultHexCellFactory()
		) {
			this.pivotPoint = pivotPoint;
		}

		public override Vector3 GetPivotPoint() => pivotPoint;
	}
}
