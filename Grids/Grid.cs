using UnityEngine;

namespace Frolics.Grids {
	public abstract class Grid<T> where T : Cell {
		protected T[] cells;

		protected Vector2 gridLength;
		protected Vector2Int gridSize;
		protected Vector3 centerPoint;
		protected float cellDiameter;

		// Getters
		public Vector2 GetGridLength() => gridLength;
		public Vector2Int GetGridSize() => gridSize;

		public float GetCellDiameter() => cellDiameter;
		public Vector3 GetCenterPoint() => centerPoint;

		public T GetCell(int index) => cells[index];
		public T[] GetCells() => cells;
	}
}
