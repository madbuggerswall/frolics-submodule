using Frolics.Grids.SpatialHelpers;

namespace Frolics.Grids {
	public class DefaultHexCellFactory : ICellFactory<HexCell, AxialCoord> {
		public HexCell CreateCell(AxialCoord coordinate) => new(coordinate);
	}
}
