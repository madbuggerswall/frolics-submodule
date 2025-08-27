using UnityEngine;

namespace Frolics.Grids {
	public interface IGrid<T> where T : ICell {
		GridPlane GridPlane { get; }
		Vector2 GridLength { get; }
		Vector2Int GridSize { get; }
		float CellDiameter { get; }
		Vector3 CenterPoint { get; }
		Vector3 PivotPoint { get; }

		T[] GetCells();
		bool TryGetCell(Vector3 position, out T cell);
	}
}
