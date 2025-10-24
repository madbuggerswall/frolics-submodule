using UnityEngine;

namespace Frolics.Grids {
	public class DefaultHexGrid : HexGrid<HexCell> {
		public DefaultHexGrid(Vector3 pivotPoint, Vector2Int gridSize, float cellDiameter, GridPlane gridPlane) : base(
			pivotPoint,
			gridSize,
			cellDiameter,
			gridPlane,
			new DefaultHexCellFactory()
		) { }
	}
}