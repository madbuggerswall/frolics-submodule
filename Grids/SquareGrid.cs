using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareGrid<TCell> : GridBase<TCell, SquareCoord> where TCell : SquareCell {
		public SquareGrid(
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane,
			ICellFactory<TCell, SquareCoord> cellFactory,
			ICoordinateConverter<SquareCoord> converter = null,
			ICoordinateGenerator<SquareCoord> generator = null,
			ICellLookup<TCell, SquareCoord> lookup = null
		) : base(
			pivotPoint,
			gridSize,
			cellDiameter,
			gridPlane,
			cellFactory,
			converter ?? new SquareCoordinateConverter(),
			generator ?? new SquareGridCoordinateGenerator(),
			lookup ?? new DefaultCellLookup<TCell, SquareCoord>()
		) { }

		public override Vector2 GetGridLength() {
			return new Vector2(gridSize.x * cellDiameter, gridSize.y * cellDiameter);
		}

		public override Vector2 GetCenterPoint() {
			return GetGridLength() * 0.5f;
		}
	}
}
