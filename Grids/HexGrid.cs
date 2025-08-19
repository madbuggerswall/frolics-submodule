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
	public class HexGrid<T> : Grid<T> where T : CircleCell {
		private readonly Dictionary<AxialCoord, T> cellsByAxialCoord = new();
		private readonly Dictionary<T, AxialCoord> axialCoordsByCell = new();

		protected HexGrid(CellFactory<T> cellFactory, Vector2Int gridSize, float cellDiameter) {
			this.cellDiameter = cellDiameter;
			this.gridSize = gridSize;
			this.gridLength = GetFittingGridSize(gridSize);

			this.cells = GenerateCells(cellFactory, gridSize);
			this.centerPoint = CalculateGridCenterPoint();
		}


		private T[] GenerateCells(CellFactory<T> cellFactory, Vector2Int gridSize) {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			T[] cells = new T[gridSize.x * gridSize.y + evenRowCount];

			for (int y = 0; y < gridSize.y; y++) {
				int rowSizeInCells = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;

				for (int x = 0; x < rowSizeInCells; x++) {
					AxialCoord axialCoord = new OffsetOddRCoord(x, -y).ToAxial();
					Vector3 worldPosition = AxialCoord.AxialToWorld(axialCoord, cellDiameter);

					int evenRowsPassed = Mathf.CeilToInt(y / 2f);
					int positionIndex = x + y * gridSize.x + evenRowsPassed;
					T cell = cellFactory.Create(worldPosition, cellDiameter);
					cells[positionIndex] = cell;

					cellsByAxialCoord.Add(axialCoord, cell);
					axialCoordsByCell.Add(cell, axialCoord);
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

		public AxialCoord GetAxialCoordinates(T cell) {
			return axialCoordsByCell.GetValueOrDefault(cell);
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
