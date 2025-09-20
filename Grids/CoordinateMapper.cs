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

		public CoordinateMapper(GridBase<TCell, TCoord> grid, ICoordinateConverter<TCoord> converter) {
			this.converter = converter;
			this.cellDiameter = grid.CellDiameter;

			TCell[] cells = grid.GetCells();
			this.cellsByCoord = new Dictionary<TCoord, TCell>(cells.Length);
			this.coordsByCell = new Dictionary<TCell, TCoord>(cells.Length);

			for (int i = 0; i < cells.Length; i++) {
				TCell cell = cells[i];
				coordsByCell[cell] = cell.GetCoord();
				cellsByCoord[cell.GetCoord()] = cell;
			}
		}

		public bool TryGetCell(Vector3 worldPosition, out TCell cell)
			=> cellsByCoord.TryGetValue(converter.WorldToCoord(worldPosition, cellDiameter), out cell);

		public bool TryGetCell(TCoord coordinate, out TCell cell)
			=> cellsByCoord.TryGetValue(coordinate, out cell);

		public bool TryGetCoordinate(TCell cell, out TCoord coordinate)
			=> coordsByCell.TryGetValue(cell, out coordinate);

		public TCell GetCell(TCoord coordinate)
			=> cellsByCoord.GetValueOrDefault(coordinate);

		public TCoord GetCoordinate(TCell cell)
			=> coordsByCell.GetValueOrDefault(cell);
	}
}
