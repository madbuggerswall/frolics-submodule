using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class GridBase<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		protected readonly Vector2Int gridSize;
		protected readonly float cellDiameter;
		protected readonly GridPlane gridPlane;

		private readonly ICellFactory<TCell, TCoord> cellFactory;
		private readonly ICoordinateConverter<TCoord> converter;
		private readonly ICoordinateGenerator<TCoord> generator;
		private readonly ICellLookup<TCell, TCoord> lookup;

		private readonly TCell[] cells;

		protected GridBase(
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

			this.cellFactory = cellFactory;
			this.converter = converter;
			this.generator = generator;
			this.lookup = lookup;

			this.cells = GenerateCells();

			// Initialize lookup
			for (int i = 0; i < cells.Length; i++)
				lookup.Add(cells[i]);
		}

		private TCell[] GenerateCells() {
			// Vector2 pivotPlanePosition = gridPlane.WorldToPlanePosition(GetPivotPoint());
			// TCoord pivotCoord = converter.PlaneToCoord(pivotPlanePosition, cellDiameter);

			List<TCoord> cellCoords = generator.Generate(gridSize);
			TCell[] cells = new TCell[cellCoords.Count];

			for (int i = 0; i < cellCoords.Count; i++)
				cells[i] = cellFactory.CreateCell(cellCoords[i]);

			return cells;
		}

		public Vector3 GetCenterPoint() {
			Vector2 pivotPlanePos = gridPlane.WorldToPlanePosition(GetPivotPoint()) - Vector2.one * cellDiameter * 0.5f;
			Vector2 centerPlanePos = pivotPlanePos + GetGridLength() * 0.5f;
			float planeHeight = gridPlane.GetOrthogonalCoordinate(GetPivotPoint());
			return gridPlane.PlaneToWorldPosition(centerPlanePos, planeHeight);
		}

		public Vector3 GetWorldPosition(TCell cell) {
			float planeHeight = gridPlane.GetOrthogonalCoordinate(GetPivotPoint());
			Vector2 pivotPlanePos = gridPlane.WorldToPlanePosition(GetPivotPoint());
			Vector2 planePosition = pivotPlanePos + converter.CoordToPlane(cell.GetCoord(), cellDiameter);
			Vector3 worldPosition = gridPlane.PlaneToWorldPosition(planePosition, planeHeight);
			return worldPosition;
		}

		public bool TryGetCell(Vector3 worldPosition, out TCell cell) {
			Vector2 planePosition = gridPlane.WorldToPlanePosition(worldPosition);
			Vector2 pivotPlanePos = gridPlane.WorldToPlanePosition(GetPivotPoint());
			Vector2 relativePlanePos = planePosition - pivotPlanePos;
			TCoord coord = converter.PlaneToCoord(relativePlanePos, cellDiameter);
			return lookup.TryGetCell(coord, out cell);
		}


		public bool TryGetCell(TCoord coord, out TCell cell) {
			return lookup.TryGetCell(coord, out cell);
		}

		public abstract Vector2 GetGridLength();
		public abstract Vector3 GetPivotPoint();

		public GridPlane GetGridPlane() => gridPlane;
		public Vector2Int GetGridSize() => gridSize;
		public float GetCellDiameter() => cellDiameter;
		public TCell[] GetCells() => cells;
	}
}
