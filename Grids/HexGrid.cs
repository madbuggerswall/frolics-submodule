using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	// float cellRadius = cellDiameter / 2;
	// float size = 2f / Sqrt(3f) * cellRadius;
	// float width = 2f * cellRadius;
	// float height = 2f * size;
	// float horizontalSpacing = width;
	// float verticalSpacing = 3f / 4f * height;

	/// <summary>
	///	Represents a pointy-top hexagonal grid using the offset odd-r coordinate system.
	/// </summary>
	public class HexGrid<T> : Grid<T> where T : HexCell {
		private readonly Dictionary<AxialCoord, T> cellsByAxialCoord = new();

		protected HexGrid(
			HexCellFactory<T> cellFactory,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) {
			this.gridSize = gridSize;
			this.gridPlane = gridPlane;
			this.cellDiameter = cellDiameter;

			this.gridLength = GetFittingGridSize(gridSize);
			this.cells = GenerateCells(cellFactory);
			this.centerPoint = CalculateGridCenterPoint();
		}


		private T[] GenerateCells(HexCellFactory<T> cellFactory) {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			T[] cells = new T[gridSize.x * gridSize.y + evenRowCount];

			for (int y = 0; y < gridSize.y; y++) {
				int rowSizeInCells = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;

				for (int x = 0; x < rowSizeInCells; x++) {
					AxialCoord axialCoord = new OffsetOddRCoord(x, -y).ToAxial();
					Vector3 positionXY = AxialCoord.AxialToWorld(axialCoord, cellDiameter);
					Vector3 position = ConvertPositionPlane(positionXY.x, positionXY.y, gridPlane);

					int evenRowsPassed = Mathf.CeilToInt(y / 2f);
					int positionIndex = x + y * gridSize.x + evenRowsPassed;
					T cell = cellFactory.Create(axialCoord, position, cellDiameter);
					cells[positionIndex] = cell;

					cellsByAxialCoord.Add(axialCoord, cell);
				}
			}

			return cells;
		}

		private Vector2 GetFittingGridSize(Vector2Int gridSizeInCells) {
			float width = gridSizeInCells.x * cellDiameter;
			float height = (gridSizeInCells.y - 1) * cellDiameter * Mathf.Cos(30 * Mathf.Deg2Rad);

			return new Vector2(width, height);
		}

		private Vector3 CalculateGridCenterPoint() {
			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cells.Length; i++)
				positionSum += cells[i].GetPosition();

			return positionSum / cells.Length;
		}


		public bool TryGetCell(Vector3 worldPosition, out T cell) {
			AxialCoord centerAxial = AxialCoord.WorldToAxial(worldPosition, cellDiameter);
			return cellsByAxialCoord.TryGetValue(centerAxial, out cell);
		}

		public bool TryGetCell(AxialCoord axialCoord, out T cell) {
			return cellsByAxialCoord.TryGetValue(axialCoord, out cell);
		}

		private void AxialStudy() {
			float cellRadius = cellDiameter / 2;

			// Hexagon (pointy-top)
			float size = 2f / Mathf.Sqrt(3f) * cellRadius;
			float width = 2f * cellRadius;
			float height = 2f * size;

			float horizontalSpacing = width;
			float verticalSpacing = 3f / 4f * height;
		}
	}
}
