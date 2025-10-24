using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class GridBase<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		protected readonly Vector2Int gridSize;
		protected readonly float cellDiameter;

		private readonly GridPlane gridPlane;
		private readonly Vector3 pivotPoint;

		private readonly ICellFactory<TCell, TCoord> cellFactory;
		private readonly ICoordinateConverter<TCoord> converter;
		private readonly ICoordinateGenerator<TCoord> generator;
		private readonly ICellLookup<TCell, TCoord> lookup;

		private readonly TCell[] cells;

		protected GridBase(
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane,
			ICellFactory<TCell, TCoord> cellFactory,
			ICoordinateConverter<TCoord> converter,
			ICoordinateGenerator<TCoord> generator,
			ICellLookup<TCell, TCoord> lookup
		) {
			this.gridSize = gridSize;
			this.cellDiameter = cellDiameter;
			this.gridPlane = gridPlane;
			this.pivotPoint = pivotPoint;

			this.cellFactory = cellFactory;
			this.converter = converter;
			this.generator = generator;
			this.lookup = lookup;

			this.cells = GenerateCells();
		}

		private TCell[] GenerateCells() {
			Vector2 pivotPlanePosition = gridPlane.WorldToPlanePosition(pivotPoint);
			TCoord pivotCoord = converter.PlaneToCoord(pivotPlanePosition, cellDiameter);

			List<TCoord> cellCoords = generator.Generate(gridSize, pivotCoord);
			TCell[] cells = new TCell[cellCoords.Count];

			for (int i = 0; i < cellCoords.Count; i++)
				cells[i] = cellFactory.CreateCell(cellCoords[i]);

			return cells;
		}


		public Vector3 GetWorldPosition(TCell cell) {
			float planeHeight = gridPlane.GetOrthogonalCoordinate(pivotPoint);
			Vector2 planePosition = converter.CoordToPlane(cell.GetCoord(), cellDiameter);
			Vector3 worldPosition = gridPlane.PlaneToWorldPosition(planePosition, planeHeight);
			return worldPosition;
		}

		public bool TryGetCell(Vector3 worldPosition, out TCell cell) {
			Vector2 planePosition = gridPlane.WorldToPlanePosition(worldPosition);
			TCoord coord = converter.PlaneToCoord(planePosition, cellDiameter);
			return lookup.TryGetCell(coord, out cell);
		}

		public bool TryGetCell(TCoord coord, out TCell cell) {
			return lookup.TryGetCell(coord, out cell);
		}

		public abstract Vector2 GetGridLength();
		public abstract Vector2 GetCenterPoint();


		public GridPlane GetGridPlane() => gridPlane;
		public Vector2Int GetGridSize() => gridSize;
		public float GetCellDiameter() => cellDiameter;
		public Vector3 GetPivotPoint() => pivotPoint;
		public TCell[] GetCells() => cells;
	}
}
