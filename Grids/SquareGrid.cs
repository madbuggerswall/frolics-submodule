using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareGrid<T> : GridBase<T, SquareCoord> where T : SquareCell {
		private readonly ICellFactory<T, SquareCoord> cellFactory;

		public SquareGrid(
			ICellFactory<T, SquareCoord> cellFactory,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) : base(new SquareCoordinateConverter(), pivotPoint, gridSize, cellDiameter, gridPlane) {
			this.cellFactory = cellFactory;

			this.cells = GenerateCells();
			this.gridLength = CalculateGridLength();
			this.centerPoint = CalculateGridCenterPoint();
			this.coordinateMapper = new CoordinateMapper<T, SquareCoord>(this, coordinateConverter);
		}

		private T[] GenerateCells() {
			T[] cells = new T[gridSize.x * gridSize.y];
			SquareCoord pivotCoord = SquareCoord.WorldToSquareCoord(pivotPoint, cellDiameter);

			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					SquareCoord coordinate = new SquareCoord(x, y) + pivotCoord;
					Vector3 position = SquareCoord.SquareCoordToWorld(coordinate, cellDiameter);
					T cell = cellFactory.CreateCell(coordinate, position, cellDiameter);
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
