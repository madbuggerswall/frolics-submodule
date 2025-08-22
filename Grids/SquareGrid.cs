using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class SquareGrid<T> : Grid<T> where T : SquareCell {
		private readonly Dictionary<SquareCoord, T> cellsBySquareCoord = new();

		protected SquareGrid(
			SquareCellFactory<T> cellFactory,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XY
		) {
			this.gridPlane = gridPlane;
			this.cellDiameter = cellDiameter;
			this.gridSize = gridSize;
			this.gridLength = GetFittingGridLength(gridSize);

			this.cells = GenerateCells(cellFactory);
			this.centerPoint = CalculateGridCenterPoint();
		}

		private T[] GenerateCells(SquareCellFactory<T> cellFactory) {
			T[] cells = new T[gridSize.x * gridSize.y];
			Vector3 cellSpacing = new(cellDiameter, cellDiameter);
			Vector3 cellOffset = new(cellDiameter / 2, cellDiameter / 2);

			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					float posX = cellOffset.x + x * cellSpacing.x;
					float posY = cellOffset.y + y * cellSpacing.y;
					Vector3 cellPosition = gridPlane.ConvertPositionPlane(posX, posY);

					SquareCoord squareCoord = new(x, y);
					T cell = cellFactory.Create(squareCoord, cellPosition, cellDiameter);
					cells[x + y * gridSize.x] = cell;
					cellsBySquareCoord.Add(squareCoord, cell);
				}
			}

			return cells;
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

		public bool TryGetCell(Vector3 worldPosition, out T cell) {
			SquareCoord center = SquareCoord.WorldToSquareCoord(worldPosition, cellDiameter);
			return cellsBySquareCoord.TryGetValue(center, out cell);
		}

		public bool TryGetCell(SquareCoord squareCoord, out T cell) {
			return cellsBySquareCoord.TryGetValue(squareCoord, out cell);
		}
	}
}
