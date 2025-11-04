using System;

namespace Frolics.Grids {
	public interface ICellFactory<out TCell, in TCoord>
		where TCell : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		TCell CreateCell(TCoord coordinate);
	}
}
