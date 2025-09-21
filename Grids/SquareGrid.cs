using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareGrid<T> : GridBase<T, SquareCoord> where T : SquareCell {
		private readonly SquareCellFactory<T> cellFactory;

		public SquareGrid(
			SquareCellFactory<T> cellFactory,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) : base(pivotPoint, gridSize, cellDiameter, gridPlane) {
			this.cellFactory = cellFactory;

			this.cells = GenerateCells();
			this.gridLength = CalculateGridLength();
			this.centerPoint = CalculateGridCenterPoint();
			this.coordinateMapper = new CoordinateMapper<T, SquareCoord>(this, new SquareCoordinateConverter());
		}

		private T[] GenerateCells() {
			T[] cells = new T[gridSize.x * gridSize.y];

			Vector2 planePosition = gridPlane.WorldToPlanePosition(pivotPoint);
			SquareCoord pivotCoord = SquareCoord.PlaneToSquareCoord(planePosition, cellDiameter);
			float planeHeight = gridPlane.GetPlaneHeight(pivotPoint);

			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					SquareCoord coordinate = new SquareCoord(x, y) + pivotCoord;
					Vector2 position = SquareCoord.SquareCoordToPlane(coordinate, cellDiameter);
					Vector3 worldPosition = gridPlane.PlaneToWorldPosition(position, planeHeight);
					T cell = cellFactory.CreateCell(coordinate, worldPosition, cellDiameter);
					cells[x + y * gridSize.x] = cell;
				}
			}

			return cells;
		}

		private Vector2 CalculateGridLength() {
			return new Vector2(gridSize.x * cellDiameter, gridSize.y * cellDiameter);
		}
	}
}
