using Frolics.Grids.SpatialHelpers;

namespace Frolics.Grids {
	public class DefaultSquareCellFactory : ICellFactory<SquareCell, SquareCoord> {
		public SquareCell CreateCell(SquareCoord coordinate) => new(coordinate);
	}
}
