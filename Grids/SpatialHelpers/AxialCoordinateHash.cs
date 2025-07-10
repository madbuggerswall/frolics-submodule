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
			new(1, 1),
			new(0, 1),
			new(-1, 0),
			new(-1, -1),
			new(0, -1),
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
			float fractionalQ = Mathf.Sqrt(3f) / 3f * x - 1f / 3f * y - 2;
			float fractionalR = 2f / 3f * y - 2;
			return AxialRound(fractionalQ, fractionalR);
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


	public struct AxialCoord {
		public int q;
		public int r;

		public static AxialCoord[] directionVectors = {
			new AxialCoord(1, 0), // East
			new AxialCoord(0, 1), // North East
			new AxialCoord(-1, 1), // North West
			new AxialCoord(-1, 0), // West
			new AxialCoord(0, -1), // South West
			new AxialCoord(1, -1) // South East
		};

		public AxialCoord(int q, int r) {
			this.q = q;
			this.r = r;
		}

		public AxialCoord GetDirection(int directionIndex) {
			return directionVectors[directionIndex];
		}

		public AxialCoord GetNeighbor(AxialCoord center, int neighborIndex) {
			return center + GetDirection(neighborIndex);
		}


		// Operator overloads
		public static AxialCoord operator +(AxialCoord lhs, AxialCoord rhs) => new(lhs.q + rhs.q, lhs.r + rhs.r);
		public static AxialCoord operator -(AxialCoord lhs, AxialCoord rhs) => new(lhs.q - rhs.q, lhs.r - rhs.r);


		// Optional: equality support
		public override bool Equals(object obj) => obj is AxialCoord other && q == other.q && r == other.r;
		public override int GetHashCode() => new Vector2Int(q, r).GetHashCode();
		public static bool operator ==(AxialCoord lhs, AxialCoord rhs) => lhs.q == rhs.q && lhs.r == rhs.r;
		public static bool operator !=(AxialCoord lhs, AxialCoord rhs) => !(lhs == rhs);

		// TODO Migrate this to an extension class
		public static CubeCoord ToCubeCoord(AxialCoord axialCoord) {
			int cubeQ = axialCoord.q;
			int cubeR = axialCoord.r;
			int cubeS = -axialCoord.q - axialCoord.r;
			return new CubeCoord(cubeQ, cubeR, cubeS);
		}
	}

	public struct DoubledCoord {
		public int column;
		public int row;


		public DoubledCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}


		public static AxialCoord DoubleHeightToAxial(DoubledCoord doubledCoord) {
			int q = doubledCoord.column;
			int r = (doubledCoord.row - doubledCoord.column) / 2;
			return new AxialCoord(q, r);
		}

		public static DoubledCoord AxialToDoubleHeight(AxialCoord axialCoord) {
			int column = axialCoord.q;
			int row = 2 * axialCoord.r + axialCoord.q;
			return new DoubledCoord(column, row);
		}

		public static AxialCoord DoubleWidthToAxial(DoubledCoord doubledCoord) {
			int q = (doubledCoord.column - doubledCoord.row) / 2;
			int r = doubledCoord.row;
			return new AxialCoord(q, r);
		}

		public static DoubledCoord AxialToDoubleWidth(AxialCoord axialCoord) {
			int column = 2 * axialCoord.q + axialCoord.r;
			int row = axialCoord.r;
			return new DoubledCoord(column, row);
		}
	}

	public struct CubeCoord {
		public int q;
		public int r;
		public int s;

		public static CubeCoord[] directionVectors = {
			new CubeCoord(1, 0, -1), // East
			new CubeCoord(0, 1, -1), // North East
			new CubeCoord(-1, 1, 0), // North West
			new CubeCoord(-1, 0, 1), // West
			new CubeCoord(0, -1, 1), // South West
			new CubeCoord(1, -1, 0) // South East
		};

		public CubeCoord(int q, int r, int s) {
			this.q = q;
			this.r = r;
			this.s = s;
		}

		public CubeCoord GetDirection(int directionIndex) {
			return directionVectors[directionIndex];
		}

		public CubeCoord GetNeighbor(CubeCoord center, int neighborIndex) {
			return center + GetDirection(neighborIndex);
		}

		// Operator overloads
		public static CubeCoord operator +(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q + rhs.q, lhs.r + rhs.r, lhs.s + rhs.s);
		}

		public static CubeCoord operator -(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q - rhs.q, lhs.r - rhs.r, lhs.s - rhs.s);
		}


		// Optional: equality support
		public override bool Equals(object obj) {
			return obj is CubeCoord other && q == other.q && r == other.r && s == other.s;
		}

		public override int GetHashCode() => new Vector3Int(q, r, s).GetHashCode();

		public static bool operator ==(CubeCoord lhs, CubeCoord rhs) {
			return lhs.q == rhs.q && lhs.r == rhs.r && lhs.s == rhs.s;
		}

		public static bool operator !=(CubeCoord lhs, CubeCoord rhs) => !(lhs == rhs);

		// TODO Migrate this to an extension class
		public static AxialCoord ToAxialCoord(CubeCoord cubeCoord) {
			int axialQ = cubeCoord.q;
			int axialR = cubeCoord.r;
			return new AxialCoord(axialQ, axialR);
		}
	}

	// Only odd-r
	public struct OffsetCoord {
		public int column;
		public int row;

		public OffsetCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		// TODO Migrate this to an extension class
		// *-r are pointy top; *-q are flat top.

		// Pointy-top, shoves odd rows by +1/2 column
		public static OffsetCoord AxialToOddR(AxialCoord axialCoord) {
			int parity = axialCoord.r & 1; // 0 if even, 1 if odd
			int column = axialCoord.q + (axialCoord.r - parity) / 2;
			int row = axialCoord.r;
			return new OffsetCoord(column, row);
		}

		public static AxialCoord OddRToAxial(OffsetCoord offsetCoord) {
			int parity = offsetCoord.row & 1;
			int q = offsetCoord.column - (offsetCoord.row - parity) / 2;
			int r = offsetCoord.row;
			return new AxialCoord(q, r);
		}

		// Pointy-top, shoves even rows by +1/2 column
		public static OffsetCoord AxialToEvenR(AxialCoord axialCoord) {
			int parity = axialCoord.r & 1; // 0 if even, 1 if odd
			int column = axialCoord.q + (axialCoord.r + parity) / 2;
			int row = axialCoord.r;
			return new OffsetCoord(column, row);
		}

		public static AxialCoord EvenRToAxial(OffsetCoord offsetCoord) {
			int parity = offsetCoord.row & 1;
			int q = offsetCoord.column - (offsetCoord.row + parity) / 2;
			int r = offsetCoord.row;
			return new AxialCoord(q, r);
		}


		// Flat-top, shoves odd columns by +1/2 column
		public static OffsetCoord AxialToOddQ(AxialCoord axialCoord) {
			int parity = axialCoord.q & 1; // 0 if even, 1 if odd
			int column = axialCoord.q;
			int row = axialCoord.r + (axialCoord.q - parity) / 2;
			return new OffsetCoord(column, row);
		}

		public static AxialCoord OddQToAxial(OffsetCoord offsetCoord) {
			int parity = offsetCoord.column & 1;
			int q = offsetCoord.column;
			int r = offsetCoord.row - (offsetCoord.column - parity) / 2;
			return new AxialCoord(q, r);
		}

		// Flat-top, shoves even columns by +1/2 column
		public static OffsetCoord AxialToEvenQ(AxialCoord axialCoord) {
			int parity = axialCoord.q & 1; // 0 if even, 1 if odd
			int column = axialCoord.q;
			int row = axialCoord.r + (axialCoord.q + parity) / 2;
			return new OffsetCoord(column, row);
		}

		public static AxialCoord EvenQToAxial(OffsetCoord offsetCoord) {
			int parity = offsetCoord.column & 1;
			int q = offsetCoord.column;
			int r = offsetCoord.row - (offsetCoord.column + parity) / 2;
			return new AxialCoord(q, r);
		}
	}
}
