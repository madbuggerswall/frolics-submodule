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

		public static SquareCoord PlaneToSquareCoord(Vector2 position, float cellDiameter) {
			int x = Mathf.RoundToInt(position.x / cellDiameter);
			int y = Mathf.RoundToInt(position.y / cellDiameter); // using z for 2D grid in XZ plane
			return new SquareCoord(x, y);
		}

		public static Vector2 SquareCoordToPlane(SquareCoord squareCoord, float cellDiameter) {
			float x = squareCoord.x * cellDiameter;
			float y = squareCoord.y * cellDiameter;
			return new Vector2(x, y);
		}

		public static int Distance(SquareCoord from, SquareCoord to)
			=> Math.Max(Math.Abs(from.x - to.x), Math.Abs(from.y - to.y));

		public static SquareCoord operator +(SquareCoord lhs, SquareCoord rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
		public static SquareCoord operator -(SquareCoord lhs, SquareCoord rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);

		public bool Equals(SquareCoord other) => x == other.x && y == other.y;
		public override bool Equals(object obj) => obj is SquareCoord other && Equals(other);
		public override int GetHashCode() => HashCode.Combine(x, y);
		public override string ToString() => $"Square({x}, {y})";

		public static bool operator ==(SquareCoord lhs, SquareCoord rhs) => lhs.Equals(rhs);
		public static bool operator !=(SquareCoord lhs, SquareCoord rhs) => !lhs.Equals(rhs);
	}
}
