using System;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class GridBase<T, TCoord> where T : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		protected readonly GridPlane gridPlane;
		protected readonly Vector2Int gridSize;
		protected readonly Vector3 pivotPoint;
		protected float cellDiameter;

		protected Vector3 centerPoint;
		protected Vector2 gridLength;
		protected T[] cells;

		protected CoordinateMapper<T, TCoord> coordinateMapper;

		public GridPlane GridPlane => gridPlane;
		public Vector2 GridLength => gridLength;
		public Vector2Int GridSize => gridSize;
		public float CellDiameter => cellDiameter;
		public Vector3 CenterPoint => centerPoint;
		public Vector3 PivotPoint => pivotPoint;

		protected GridBase(
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) {
			this.gridSize = gridSize;
			this.cellDiameter = cellDiameter;
			this.gridPlane = gridPlane;
			this.pivotPoint = pivotPoint;
		}

		protected Vector3 CalculateGridCenterPoint() {
			if (cells == null || cells.Length == 0)
				return Vector3.zero;

			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cells.Length; i++)
				positionSum += cells[i].Position;

			return positionSum / cells.Length;
		}

		public T[] GetCells() => cells;

		public bool TryGetCell(Vector3 position, out T cell) => coordinateMapper.TryGetCell(position, out cell);

		public bool TryGetCell(TCoord coordinate, out T cell) => coordinateMapper.TryGetCell(coordinate, out cell);
	}
}
