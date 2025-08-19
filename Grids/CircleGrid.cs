using System.Collections.Generic;
using Frolics.Grids.NeighborHelpers;
using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Grids {
	public class CircleGrid<T> : Grid<T> where T : CircleCell {
		// private readonly CircleGridNeighborHelper<T> neighborHelper;

		private readonly Dictionary<AxialCoord, T> cellsByAxialCoord;
		private readonly Dictionary<T, AxialCoord> axialCoordsByCell;

		// NOTE HexGrid<CircleCell> HexGrid<HexCell> initialized via AxialCoordinates (Doubled or Offset)
		protected CircleGrid(CellFactory<T> cellFactory, Vector2Int gridSize, float cellDiameter) {
			this.cellDiameter = cellDiameter;
			this.gridSize = gridSize;
			this.gridSizeInLength = GetFittingGridSize(gridSize);

			Vector2[] cellPositions = GenerateCellPositions(gridSize);
			this.centerPoint = CalculateGridCenterPoint(cellPositions);
			this.cells = GenerateCells(cellFactory, cellPositions);

			// this.neighborHelper = new CircleGridNeighborHelper<T>(this);
		}

		private Vector2 GetFittingGridSize(Vector2Int gridSizeInCells) {
			float sizeX = gridSizeInCells.x * cellDiameter;
			float sizeY = (gridSizeInCells.y - 1) * cellDiameter * Mathf.Cos(30 * Mathf.Deg2Rad);
			return new Vector2(sizeX, sizeY);
		}


		private Vector2[] GenerateCellPositions(Vector2Int gridSize) {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			Vector2[] cellPositions = new Vector2[gridSize.x * gridSize.y + evenRowCount];

			for (int y = 0; y < gridSize.y; y++) {
				int rowSizeInCells = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;

				for (int x = 0; x < rowSizeInCells; x++) {
					Debug.Log($"x,y:{x},{y}");
					OffsetOddRCoord oddRCoord = new(x, -y);
					Vector2 worldPosition = AxialCoord.AxialToWorld(oddRCoord.ToAxial(), cellDiameter);

					int evenRowsPassed = Mathf.CeilToInt(y / 2f);
					int positionIndex = x + y * gridSize.x + evenRowsPassed;
					cellPositions[positionIndex] = worldPosition;
				}
			}

			return cellPositions;
		}

		private T[] GenerateCells(CellFactory<T> cellFactory, Vector2Int gridSize) {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			T[] cells = new T[gridSize.x * gridSize.y + evenRowCount];

			for (int y = 0; y < gridSize.y; y++) {
				int rowSizeInCells = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;

				for (int x = 0; x < rowSizeInCells; x++) {
					OffsetOddRCoord oddRCoord = new(x, -y);
					AxialCoord axialCoord = oddRCoord.ToAxial();
					Vector2 worldPosition = AxialCoord.AxialToWorld(axialCoord, cellDiameter);

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


		private Vector3 CalculateGridCenterPoint(Vector2[] cellPositions) {
			Vector2 positionSum = Vector3.zero;

			for (int i = 0; i < cellPositions.Length; i++)
				positionSum += cellPositions[i];

			return (positionSum / cellPositions.Length).WithZ(0f);
		}
		//
		// public T[] GetNeighbors(T cell) {
		// 	return neighborHelper.GetCellNeighbors(cell);
		// }

		// Redundant
		private T GetCell(Vector2Int index) {
			bool isEvenRow = index.y % 2 == 0;
			int clampedX = Mathf.Clamp(index.x, 0, gridSize.x - 1);
			int clampedY = Mathf.Clamp(index.y, 0, gridSize.y - (isEvenRow ? 1 : 2));

			// Odd rows has 1 cell less than even rows, because of centering strategy
			Vector2Int clampedIndex = new Vector2Int(clampedX, clampedY);
			int oddRowCount = Mathf.FloorToInt(index.y / 2f);

			return cells[clampedIndex.x + clampedIndex.y * gridSize.x - oddRowCount] as T;
		}
	}
}
