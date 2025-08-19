using Frolics.Grids.NeighborHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public enum GridPlane { XY, XZ, YZ }

	public abstract class SquareGrid<T> : Grid<T> where T : SquareCell {
		public struct CellParams {
			public CellFactory<T> CellFactory { get; set; }
			public float CellDiameter { get; set; }
		}

		public struct GridParams {
			public Vector2Int GridSize { get; set; }
			public GridPlane GridPlane { get; set; }
		}

		protected readonly SquareGridNeighborHelper<T> neighborHelper;

		protected SquareGrid(GridParams gridParams, CellParams cellParams) {
			this.cellDiameter = cellParams.CellDiameter;
			this.gridSize = gridParams.GridSize;
			this.gridLength = GetFittingGridLength(gridSize);
			
			this.cells = GenerateCells(cellParams.CellFactory, gridSize, gridParams.GridPlane);
			this.centerPoint = CalculateGridCenterPoint();

			this.neighborHelper = new SquareGridNeighborHelper<T>(this, false);
		}

		private T[] GenerateCells(CellFactory<T> cellFactory, Vector2Int gridSize, GridPlane gridPlane) {
			T[] cells = new T[gridSize.x * gridSize.y];
			Vector3 cellSpacing = new(cellDiameter, cellDiameter);
			Vector3 cellOffset = new(cellDiameter / 2, cellDiameter / 2);
			Vector2Int index = Vector2Int.zero;

			for (index.y = 0; index.y < gridSize.y; index.y++) {
				for (index.x = 0; index.x < gridSize.x; index.x++) {
					float posX = cellOffset.x + index.x * cellSpacing.x;
					float posY = cellOffset.y + index.y * cellSpacing.y;
					Vector3 cellPosition = GetCellPosition(posX, posY, gridPlane);

					cells[index.x + index.y * gridSize.x] = cellFactory.Create(cellPosition, cellDiameter);
				}
			}

			return cells;
		}

		private Vector3 GetCellPosition(float posX, float posY, GridPlane gridPlane) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(posX, posY),
				GridPlane.XZ => new Vector3(posX, 0, posY),
				GridPlane.YZ => new Vector3(0, posX, posY),
				_ => new Vector3(posX, posY)
			};
		}

		private Vector3 CalculateGridCenterPoint() {
			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cells.Length; i++)
				positionSum += cells[i].GetPosition();

			return positionSum / cells.Length;
		}

		private Vector2 GetFittingGridLength(Vector2Int gridSizeInCells) {
			return new Vector2(gridSizeInCells.x * cellDiameter, gridSizeInCells.y * cellDiameter);
		}

		public T[] GetNeighbors(T cell) {
			return neighborHelper.GetCellNeighbors(cell);
		}
	}
}
