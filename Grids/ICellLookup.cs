using System;

namespace Frolics.Grids {
	public interface ICellLookup<TCell, TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		void Add(GridBase<TCell, TCoord> grid);
		bool TryGetCell(TCoord coord, out TCell cell);
	}
}