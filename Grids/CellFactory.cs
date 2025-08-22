using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class CellFactory<T> where T : Cell {
		public abstract T Create(Vector3 position, float diameter);
	}

	public abstract class HexCellFactory<T> where T : HexCell {
		public abstract T Create(AxialCoord axialCoord, Vector3 position, float diameter);
	}

	public abstract class SquareCellFactory<T> where T : SquareCell {
		public abstract T Create(SquareCoord squareCoord, Vector3 position, float diameter);
	}
}
