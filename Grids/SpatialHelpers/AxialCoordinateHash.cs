using System;
using System.Collections.Generic;
using Frolics.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Frolics.Grids.SpatialHelpers {
	// NOTE Also do this for SquareCells
	// IDEA Rename to AxialCellMapper<T> or AxialGridHelper<T>
	// https://www.redblobgames.com/grids/hexagons/
	public class AxialCoordinateHash<T> where T : CircleCell {
		private readonly Vector2Int[] hexSelectionOffsets = {
			new(0, 0),
			new(1, 0),
			new(0, 1),
			new(1, -1),
			new(0, -1),
			new(-1, 0),
			new(-1, 1),
		};

		private readonly Dictionary<Vector2Int, T> axialMap;
		private readonly Dictionary<T, Vector2Int> axialCoordinatesByCells;

		private readonly float cellDiameter;
		private readonly T[] cells;

		public AxialCoordinateHash(CircleGrid<T> grid) {
			this.cellDiameter = grid.GetCellDiameter();
			this.cells = grid.GetCells();
			this.axialMap = MapCellsByAxialCoordinates();
			this.axialCoordinatesByCells = MapAxialCoordinatesByCells(axialMap);
		}

		private Dictionary<Vector2Int, T> MapCellsByAxialCoordinates() {
			Dictionary<Vector2Int, T> cellsByAxialCoordinates = new();

			foreach (T cell in cells) {
				Vector2Int axial = WorldToAxial(cell.GetWorldPosition().GetXY());
				if (!cellsByAxialCoordinates.TryAdd(axial, cell))
					Debug.Log("Oh no");
			}

			return cellsByAxialCoordinates;
		}

		private Dictionary<T, Vector2Int> MapAxialCoordinatesByCells(Dictionary<Vector2Int, T> map) {
			Dictionary<T, Vector2Int> invertedAxialMap = new();
			foreach ((Vector2Int axialCoordinate, T cell) in map)
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

		// Use World Position → Approximate Hex Coordinate Hash
		private Vector2Int WorldToAxial(Vector2 worldPosition) {
			float cellRadius = cellDiameter / 2;

			// Hexagon (pointy-top)
			float size = 2f / Mathf.Sqrt(3f) * cellRadius;
			float x = worldPosition.x / size;
			float y = worldPosition.y / size;

			// Cartesian to hex
			float q = Mathf.Sqrt(3f) / 3f * x - 1f / 3f * y;
			float r = 2f / 3f * y;
			return AxialRound(q, r);
		}

		private Vector2Int AxialRound(float fractionalQ, float fractionalR) {
			// Convert axial to cube coordinates
			float fractionalS = -fractionalQ - fractionalR;

			// Round each cube component
			int roundedQ = (int) Math.Round(fractionalQ, MidpointRounding.AwayFromZero);
			int roundedR = (int) Math.Round(fractionalR, MidpointRounding.AwayFromZero);
			int roundedS = (int) Math.Round(fractionalS, MidpointRounding.AwayFromZero);

			// Calculate differences from the original fractional values
			float deltaQ = Mathf.Abs(roundedQ - fractionalQ);
			float deltaR = Mathf.Abs(roundedR - fractionalR);
			float deltaS = Mathf.Abs(roundedS - fractionalS);

			// Adjust the component with the greatest rounding error
			if (deltaQ > deltaS && deltaQ > deltaR)
				roundedQ = -roundedS - roundedR;
			else if (deltaS > deltaR)
				roundedS = -roundedQ - roundedR;
			else
				roundedR = -roundedQ - roundedS;

			Assert.IsTrue(roundedQ + roundedR + roundedS == 0);
			return new Vector2Int(roundedQ, roundedR);
		}

		public Vector2Int AxialToOddR(Vector2Int hex) {
			int parity = hex.y & 1; // 0 if even, 1 if odd
			int col = hex.x + (hex.y - parity) / 2;
			int row = hex.y;
			return new Vector2Int(col, row);
		}

		public bool TryGetCell(Vector3 worldPosition, out T fieldCell) {
			Vector2Int centerAxial = WorldToAxial(worldPosition);

			// Search center + neighbors (hex rings of radius 1)
			foreach (Vector2Int offset in hexSelectionOffsets) {
				Vector2Int axial = centerAxial + offset;

				if (axialMap.TryGetValue(axial, out T candidate)) {
					if (candidate.IsInsideCell(worldPosition)) {
						fieldCell = candidate;
						return true;
					}
				}
			}

			fieldCell = null;
			return false;
		}

		public int GetAxialDistance(Vector2Int a, Vector2Int b) {
			int deltaQ = a.x - b.x;
			int deltaR = a.y - b.y;
			return (Mathf.Abs(deltaQ) + Mathf.Abs(deltaR) + Mathf.Abs(deltaQ + deltaR)) / 2;
		}

		public Vector2Int GetAxialCoordinates(T cell) {
			return axialCoordinatesByCells.GetValueOrDefault(cell);
		}
	}


	// // IDEA Rename to Axial
	// public struct Hex {
	// 	public static readonly Hex[] SelectionOffsets = {
	// 		new(0, 0),
	// 		new(1, 0),
	// 		new(1, 1),
	// 		new(0, 1),
	// 		new(-1, 0),
	// 		new(-1, -1),
	// 		new(0, -1)
	// 	};
	//
	// 	public int Q { get; private set; }
	// 	public int R { get; private set; }
	//
	// 	public Hex(int q, int r) {
	// 		Q = q;
	// 		R = r;
	// 	}
	//
	// 	public Hex(Vector2Int axial) {
	// 		Q = axial.x;
	// 		R = axial.y;
	// 	}
	//
	//
	// 	public static int GetAxialDistance(Hex a, Hex b) {
	// 		int deltaQ = a.Q - b.Q;
	// 		int deltaR = a.R - b.R;
	// 		return (Mathf.Abs(deltaQ) + Mathf.Abs(deltaR) + Mathf.Abs(deltaQ + deltaR)) / 2;
	// 	}
	//
	// 	public static Hex operator +(Hex a, Hex b) {
	// 		return new Hex(a.Q + b.Q, a.R + b.R);
	// 	}
	//
	// 	public static Hex operator -(Hex a, Hex b) {
	// 		return new Hex(a.Q - b.Q, a.R - b.R);
	// 	}
	// }
}
