using System;
using System.Collections.Generic;
using Core.FieldGrids;
using Frolics.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Frolics.Grids.SpatialHelpers {
	// NOTE Also do this for SquareCells
	// IDEA Rename to AxialCellMapper<T> or AxialGridHelper<T>
	// https://www.redblobgames.com/grids/hexagons/
	public class AxialCoordinateHash<T> where T : CircleCell {
		private readonly Dictionary<AxialCoord, T> axialMap;
		private readonly Dictionary<T, AxialCoord> axialCoordinatesByCells;

		private readonly float cellDiameter;
		private readonly T[] cells;

		public AxialCoordinateHash(CircleGrid<T> grid) {
			this.cellDiameter = grid.GetCellDiameter();
			this.cells = grid.GetCells();
			this.axialMap = MapCellsByAxialCoordinates();
			this.axialCoordinatesByCells = MapAxialCoordinatesByCells(axialMap);
		}

		private Dictionary<AxialCoord, T> MapCellsByAxialCoordinates() {
			Dictionary<AxialCoord, T> cellsByAxialCoordinates = new();

			foreach (T cell in cells) {
				AxialCoord axial = AxialCoord.WorldToAxial(cell.GetWorldPosition().GetXY(), cellDiameter);
				if (!cellsByAxialCoordinates.TryAdd(axial, cell))
					Debug.Log("Oh no");
			}

			return cellsByAxialCoordinates;
		}

		private Dictionary<T, AxialCoord> MapAxialCoordinatesByCells(Dictionary<AxialCoord, T> map) {
			Dictionary<T, AxialCoord> invertedAxialMap = new();
			foreach ((AxialCoord axialCoordinate, T cell) in map)
				invertedAxialMap.TryAdd(cell, axialCoordinate);

			return invertedAxialMap;
		}

		private void AxialStudy() {
			float cellRadius = cellDiameter / 2;

			// Hexagon (pointy-top)
			float size = 2f / Mathf.Sqrt(3f) * cellRadius;
			float width = 2f * cellRadius;
			float height = 2f * size;

			float horizontalSpacing = width;
			float verticalSpacing = 3f / 4f * height;
		}

		public bool TryGetCell(Vector3 worldPosition, out T fieldCell) {
			AxialCoord centerAxial = AxialCoord.WorldToAxial(worldPosition, cellDiameter);
			return axialMap.TryGetValue(centerAxial, out fieldCell);
		}

		public AxialCoord GetAxialCoordinates(T cell) {
			return axialCoordinatesByCells.GetValueOrDefault(cell);
		}

		public T GetCell(AxialCoord axialCoord) {
			return axialMap.GetValueOrDefault(axialCoord);
		}
	}
}
