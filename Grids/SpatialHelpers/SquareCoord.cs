using System;
using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	[Serializable]
	public struct SquareCoord : IEquatable<SquareCoord> {
		public static readonly SquareCoord[] DirectionVectors = {
			new(1, 0),   // Right
			new(1, 1),   // Up Right  
			new(0, 1),   // Up
			new(-1, 1),  // Up Left
			new(-1, 0),  // Left
			new(-1, -1), // Down Left
			new(0, -1),  // Down
			new(1, -1)   // Down Right (FIXED!)
		};

		public int x;
		public int y;

		public SquareCoord(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public static SquareCoord Round(float fractionalX, float fractionalY) {
			return new SquareCoord(Mathf.RoundToInt(fractionalX), Mathf.RoundToInt(fractionalY));
		}


		public static SquareCoord[] Line(SquareCoord start, SquareCoord end) {
			int distance = Distance(start, end);
			if (distance == 0)
				return new[] { start };

			SquareCoord[] coords = new SquareCoord[distance + 1];
			for (int i = 0; i <= distance; i++) {
				(float x, float y) = Lerp(start, end, 1f / distance * i);
				coords[i] = Round(x, y);
			}

			return coords;
		}

		public static SquareCoord Line(SquareCoord start, SquareCoord end, int i) {
			int distance = Distance(start, end);
			if (distance == 0)
				return start;

			(float x, float y) = Lerp(start, end, 1f / distance * i);
			return Round(x, y);
		}

		// TODO Untested
		public static SquareCoord[] Range(SquareCoord center, int range) {
			if (range < 0)
				throw new ArgumentException("Range must be non-negative", nameof(range));

			// The number of tiles in a square of radius 'range' is (2n + 1)^2
			int coordCount = GetRangeCoordCount(range);

			SquareCoord[] results = new SquareCoord[coordCount];
			int i = 0;

			for (int x = -range; x <= range; x++) {
				for (int y = -range; y <= range; y++) {
					results[i++] = new SquareCoord(center.x + x, center.y + y);
				}
			}

			return results;
		}

		/// <summary>
		/// Returns a specific coordinate within a range based on an index.
		/// Index ranges from 0 to ((2 * range + 1)^2) - 1.
		/// </summary>
		public static SquareCoord Range(SquareCoord center, int range, int i) {
			if (range < 0)
				throw new ArgumentException("Range must be non-negative", nameof(range));

			int sideLength = 2 * range + 1;
			int totalTiles = sideLength * sideLength;

			if (i < 0 || i >= totalTiles)
				throw new IndexOutOfRangeException($"Index {i} is out of bounds for range {range}");

			// Calculate local x and y from the flat index i
			// This maps i to a grid starting at (-range, -range) and ending at (range, range)
			// The order is strictly bottom-to-top, left-to-right ,starting from the bottom-left corner of the range.
			int localX = i % sideLength - range;
			int localY = i / sideLength - range;

			return new SquareCoord(center.x + localX, center.y + localY);
		}

		// The number of tiles in a square of radius 'range' is (2n + 1)^2
		public static int GetRangeCoordCount(int range) {
			int sideLength = 2 * range + 1;
			return sideLength * sideLength;
		}

		public static (float x, float y) Lerp(SquareCoord start, SquareCoord end, float t) {
			return (Mathf.Lerp(start.x, end.x, t), Mathf.Lerp(start.y, end.y, t));
		}

		public static SquareCoord PlaneToSquareCoord(Vector2 position, float cellDiameter) {
			return Round(position.x / cellDiameter, position.y / cellDiameter);
		}

		public static Vector2 SquareCoordToPlane(SquareCoord squareCoord, float cellDiameter) {
			float x = squareCoord.x * cellDiameter;
			float y = squareCoord.y * cellDiameter;
			return new Vector2(x, y);
		}

		public static int Distance(SquareCoord from, SquareCoord to)
			=> Math.Max(Math.Abs(from.x - to.x), Math.Abs(from.y - to.y));

		// Operators
		public static SquareCoord operator +(SquareCoord lhs, SquareCoord rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
		public static SquareCoord operator -(SquareCoord lhs, SquareCoord rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);
		public static SquareCoord operator *(SquareCoord lhs, int rhs) => new(lhs.x * rhs, lhs.y * rhs);
		public static SquareCoord operator *(int lhs, SquareCoord rhs) => new(rhs.x * lhs, rhs.y * lhs);


		public bool Equals(SquareCoord other) => x == other.x && y == other.y;
		public override bool Equals(object obj) => obj is SquareCoord other && Equals(other);
		public override int GetHashCode() => HashCode.Combine(x, y);
		public override string ToString() => $"Square({x}, {y})";

		public static bool operator ==(SquareCoord lhs, SquareCoord rhs) => lhs.Equals(rhs);
		public static bool operator !=(SquareCoord lhs, SquareCoord rhs) => !lhs.Equals(rhs);
	}
}
