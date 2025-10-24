using System;
using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	// DictionaryLookup (default), SparseLookup, BidirectionalLookup (opt-in reverse map).
	public class DefaultCellLookup<TCell, TCoord> : ICellLookup<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		private readonly Dictionary<TCoord, TCell> cellsByCoord = new();

		public void Add(GridBase<TCell, TCoord> grid) {
			TCell[] cells = grid.GetCells();
			for (int i = 0; i < cells.Length; i++)
				cellsByCoord.Add(cells[i].GetCoord(), cells[i]);
		}

		public bool TryGetCell(TCoord coordinate, out TCell cell) {
			return cellsByCoord.TryGetValue(coordinate, out cell);
		}
	}
}
