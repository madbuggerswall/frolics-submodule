using Frolics.Grids.SpatialHelpers;

namespace Frolics.Grids {
	public abstract class SquareCellFactory<T> : ICellFactory<T, SquareCoord> where T : SquareCell {
		public abstract T CreateCell(SquareCoord coordinate);
	}
}
