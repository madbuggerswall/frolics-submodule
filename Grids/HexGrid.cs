using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	/// <summary>
	///	Represents a pointy-top hexagonal grid initialized with offset odd-r coords.
	/// </summary>
	public abstract class HexGrid<TCell> : GridBase<TCell, AxialCoord> where TCell : HexCell {
		public HexGrid(
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane,
			ICellFactory<TCell, AxialCoord> cellFactory,
			ICoordinateConverter<AxialCoord> converter = null,
			ICoordinateGenerator<AxialCoord> generator = null,
			ICellLookup<TCell, AxialCoord> lookup = null
		) : base(
			gridSize,
			cellDiameter,
			gridPlane,
			cellFactory,
			converter ?? new AxialCoordinateConverter(),
			generator ?? new HexGridCoordGenerator(),
			lookup ?? new DefaultCellLookup<TCell, AxialCoord>()
		) { }

		public override Vector2 GetGridLength() {
			float width = gridSize.x * cellDiameter;
			float height = (gridSize.y - 1) * cellDiameter * Mathf.Cos(30 * Mathf.Deg2Rad);

			return new Vector2(width, height);
		}
	}
}
