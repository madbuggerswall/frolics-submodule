using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class CellFactory<T> where T : Cell {
		public abstract T Create(Vector3 position, float diameter);
	}
}
