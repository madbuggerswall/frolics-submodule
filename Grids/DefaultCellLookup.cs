using System;
using System.Collections.Generic;

namespace Frolics.Grids {
	// DictionaryLookup (default), SparseLookup, BidirectionalLookup (opt-in reverse map).
	public class DefaultCellLookup<TCell, TCoord> : ICellLookup<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		private readonly Dictionary<TCoord, TCell> cellsByCoord = new();

		public void Add(TCell cell) {
			cellsByCoord.Add(cell.GetCoord(), cell);
		}

		public bool TryGetCell(TCoord coordinate, out TCell cell) {
			return cellsByCoord.TryGetValue(coordinate, out cell);
		}
	}
}
