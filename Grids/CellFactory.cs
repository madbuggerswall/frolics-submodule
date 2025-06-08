using UnityEngine;

namespace Frolics.Grids {
	public abstract class CellFactory<T> where T : Cell {
		public abstract T Create(Vector2 cellPosition, float diameter);
	}
}
