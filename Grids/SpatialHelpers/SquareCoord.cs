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

			SquareCoord[] cubeCoords = new SquareCoord[distance + 1];
			for (int i = 0; i <= distance; i++) {
				(float x, float y) = Lerp(start, end, 1f / distance * i);
				cubeCoords[i] = Round(x, y);
			}

			return cubeCoords;
		}

		public static SquareCoord Line(SquareCoord start, SquareCoord end, int i) {
			int distance = Distance(start, end);
			if (distance == 0)
				return start;

			(float x, float y) = Lerp(start, end, 1f / distance * i);
			return Round(x, y);
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
