using System;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class GridBase<T, TCoord> where T : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		[SerializeField] protected T[] cells;
		[SerializeField] protected GridPlane gridPlane;
		[SerializeField] protected Vector2 gridLength;
		[SerializeField] protected Vector2Int gridSize;
		[SerializeField] protected Vector3 centerPoint;
		[SerializeField] protected Vector3 pivotPoint;
		[SerializeField] protected float cellDiameter;

		protected readonly ICoordinateConverter<TCoord> coordinateConverter;
		protected CoordinateMapper<T, TCoord> coordinateMapper;

		public GridPlane GridPlane => gridPlane;
		public Vector2 GridLength => gridLength;
		public Vector2Int GridSize => gridSize;
		public float CellDiameter => cellDiameter;
		public Vector3 CenterPoint => centerPoint;
		public Vector3 PivotPoint => pivotPoint;

		protected GridBase(
			ICoordinateConverter<TCoord> coordinateConverter,
			Vector3 pivotPoint,
			Vector2Int gridSize,
			float cellDiameter,
			GridPlane gridPlane = GridPlane.XZ
		) {
			this.coordinateConverter = coordinateConverter;
			this.gridSize = gridSize;
			this.cellDiameter = cellDiameter;
			this.gridPlane = gridPlane;
			this.pivotPoint = pivotPoint;
			ValidateGrid();
		}

		private void ValidateGrid() {
			if (coordinateConverter is null)
				throw new ArgumentNullException(nameof(coordinateConverter));

			if (gridSize.x <= 0 || gridSize.y <= 0)
				throw new ArgumentException("Grid size must be positive", nameof(gridSize));

			if (cellDiameter <= 0)
				throw new ArgumentException("Cell diameter must be positive", nameof(cellDiameter));
		}

		protected Vector3 CalculateGridCenterPoint() {
			if (cells == null || cells.Length == 0)
				return Vector3.zero;

			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cells.Length; i++)
				positionSum += cells[i].Position;

			return positionSum / cells.Length;
		}

		public T[] GetCells() => cells ?? Array.Empty<T>();

		public virtual bool TryGetCell(Vector3 position, out T cell) {
			return coordinateMapper.TryGetCell(position, out cell);
		}

		public bool TryGetCell(TCoord coordinate, out T cell) {
			return coordinateMapper.TryGetCell(coordinate, out cell);
		}
	}
}
