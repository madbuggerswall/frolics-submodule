using System;

namespace Frolics.Grids {
	public interface ICellLookup<TCell, in TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		void Add(TCell cell);
		bool TryGetCell(TCoord coord, out TCell cell);
	}
}
