using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	/// <summary>
	///	Represents a pointy-top hexagonal grid initialized with offset odd-r coords.
	/// </summary>
	public class HexGrid<T> : GridBase<T, AxialCoord> where T : HexCell {
		private readonly ICellFactory<T, AxialCoord> cellFactory;

		public HexGrid(
			ICellFactory<T, AxialCoord> cellFactory,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) : base(new AxialCoordinateConverter(), gridSize, cellDiameter, gridPlane) {
			this.cellFactory = cellFactory ?? throw new ArgumentNullException(nameof(cellFactory));

			this.cells = GenerateCells();
			this.gridLength = CalculateGridLength();
			this.centerPoint = CalculateGridCenterPoint();
			this.coordinateMapper = new CoordinateMapper<T, AxialCoord>(this, coordinateConverter);
		}

		private T[] GenerateCells() {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			T[] cells = new T[gridSize.x * gridSize.y + evenRowCount];

			for (int y = 0; y < gridSize.y; y++) {
				int rowSizeInCells = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;

				for (int x = 0; x < rowSizeInCells; x++) {
					AxialCoord axialCoord = new OffsetOddRCoord(x, -y).ToAxial();
					Vector3 positionXY = AxialCoord.AxialToWorld(axialCoord, cellDiameter);
					Vector3 position = gridPlane.ConvertPositionPlane(positionXY.x, positionXY.y);

					int evenRowsPassed = Mathf.CeilToInt(y / 2f);
					int positionIndex = x + y * gridSize.x + evenRowsPassed;
					T cell = cellFactory.CreateCell(axialCoord, position, cellDiameter);
					cells[positionIndex] = cell;
				}
			}

			return cells;
		}

		private Vector2 CalculateGridLength() {
			float width = gridSize.x * cellDiameter;
			float height = (gridSize.y - 1) * cellDiameter * Mathf.Cos(30 * Mathf.Deg2Rad);

			return new Vector2(width, height);
		}

		// Hex-specific operations using the enhanced SpatialHelpers
		public T[] GetCellsInRange(AxialCoord center, int range) {
			CubeCoord cubeCenter = center.ToCube();
			CubeCoord[] cubeCoords = CubeCoord.Range(cubeCenter, range);
			List<T> cells = new(cubeCoords.Length);

			for (int i = 0; i < cubeCoords.Length; i++)
				if (TryGetCell(cubeCoords[i].ToAxial(), out T cell))
					cells.Add(cell);

			return cells.ToArray();
		}

		public T[] GetCellsOnLine(AxialCoord start, AxialCoord end) {
			CubeCoord startCube = start.ToCube();
			CubeCoord endCube = end.ToCube();
			CubeCoord[] lineCoords = CubeCoord.Line(startCube, endCube);
			List<T> cells = new(lineCoords.Length);

			for (int i = 0; i < lineCoords.Length; i++)
				if (TryGetCell(lineCoords[i].ToAxial(), out T cell))
					cells.Add(cell);

			return cells.ToArray();
		}
	}
}
