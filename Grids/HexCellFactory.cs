using Frolics.Grids.SpatialHelpers;

namespace Frolics.Grids {
	public abstract class HexCellFactory<T> : ICellFactory<T, AxialCoord> where T : HexCell {
		public abstract T CreateCell(AxialCoord coordinate);
	}
}
