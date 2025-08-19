using UnityEngine;

namespace Frolics.Grids {
	public enum GridPlane { XY, XZ, YZ }

	public abstract class Grid<T> where T : Cell {
		public struct CellParams {
			public CellFactory<T> CellFactory { get; set; }
			public float CellDiameter { get; set; }
		}

		public struct GridParams {
			public Vector2Int GridSize { get; set; }
			public GridPlane GridPlane { get; set; }
		}


		protected T[] cells;

		protected GridPlane gridPlane;
		protected Vector2 gridLength;
		protected Vector2Int gridSize;
		protected Vector3 centerPoint;
		protected float cellDiameter;

		// Getters
		public GridPlane GetGridPlane() => gridPlane;
		public Vector2 GetGridLength() => gridLength;
		public Vector2Int GetGridSize() => gridSize;

		public float GetCellDiameter() => cellDiameter;
		public Vector3 GetCenterPoint() => centerPoint;

		public T GetCell(int index) => cells[index];
		public T[] GetCells() => cells;

		// Static
		protected static Vector3 ConvertPositionPlane(float posX, float posY, GridPlane gridPlane) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(posX, posY),
				GridPlane.XZ => new Vector3(posX, 0, posY),
				GridPlane.YZ => new Vector3(0, posX, posY),
				_ => new Vector3(posX, posY)
			};
		}
	}
}
