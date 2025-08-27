using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class SquareCellFactory<T> : ICellFactory<T, SquareCoord> where T : SquareCell
	{
		public abstract T CreateCell(SquareCoord coordinate, Vector3 position, float diameter);
	}
}