using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Grids {
	/// <summary>
	/// Generic coordinate mapping system that can work with any coordinate type and cell type.
	/// </summary>
	public class CoordinateMapper<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		private readonly Dictionary<TCoord, TCell> cellsByCoord;
		private readonly Dictionary<TCell, TCoord> coordsByCell;
		private readonly ICoordinateConverter<TCoord> converter;

		private readonly float cellDiameter;
		private readonly GridPlane gridPlane;

		public CoordinateMapper(GridBase<TCell, TCoord> grid, ICoordinateConverter<TCoord> converter) {
			this.converter = converter;

			this.cellDiameter = grid.CellDiameter;
			this.gridPlane = grid.GridPlane;

			TCell[] cells = grid.GetCells();
			this.cellsByCoord = new Dictionary<TCoord, TCell>(cells.Length);
			this.coordsByCell = new Dictionary<TCell, TCoord>(cells.Length);

			for (int i = 0; i < cells.Length; i++) {
				TCell cell = cells[i];
				coordsByCell[cell] = cell.GetCoord();
				cellsByCoord[cell.GetCoord()] = cell;
			}
		}

		public bool TryGetCell(Vector3 worldPosition, out TCell cell) {
			Vector2 planePosition = gridPlane.WorldToPlanePosition(worldPosition);
			TCoord coord = converter.PlaneToCoord(planePosition, cellDiameter);
			return cellsByCoord.TryGetValue(coord, out cell);
		}

		public bool TryGetCell(TCoord coordinate, out TCell cell) {
			return cellsByCoord.TryGetValue(coordinate, out cell);
		}

		// TODO Methods below are redundant
		public bool TryGetCoordinate(TCell cell, out TCoord coordinate) {
			return coordsByCell.TryGetValue(cell, out coordinate);
		}

		public TCell GetCell(TCoord coordinate) {
			return cellsByCoord.GetValueOrDefault(coordinate);
		}

		public TCoord GetCoordinate(TCell cell) {
			return coordsByCell.GetValueOrDefault(cell);
		}
	}
}
