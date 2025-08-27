using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class HexCellFactory<T> : ICellFactory<T, AxialCoord> where T : HexCell
	{
		public abstract T CreateCell(AxialCoord coordinate, Vector3 position, float diameter);
	}
}