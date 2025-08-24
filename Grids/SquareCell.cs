using System;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareCell : Cell {
		protected SquareCoord squareCoord;

		public SquareCell(SquareCoord squareCoord, Vector3 position, float diameter) : base(position, diameter) {
			this.squareCoord = squareCoord;
		}

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			bool inHorizontally = position.x - radius <= point.x && point.x <= position.x + radius;
			bool inVertically = position.y - radius <= point.y && point.y <= position.y + radius;

			return inHorizontally && inVertically;
		}

		public SquareCoord GetSquareCoord() => this.squareCoord;
	}

	public struct SquareCoord : IEquatable<SquareCoord> {
		public static readonly SquareCoord[] Directions = {
			new(1, 0),   // Right
			new(1, 1),   // Up Right
			new(0, 1),   // Up
			new(-1, 1),  // Up Left
			new(-1, 0),  // Left
			new(-1, -1), // Down Left
			new(0, -1),  // Down
			new(0, 1)    // Down Right
		};

		public int x;
		public int y;

		public SquareCoord(int x, int y) {
			this.x = x;
			this.y = y;
		}

		// Operator overloads
		public static SquareCoord operator +(SquareCoord lhs, SquareCoord rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
		public static SquareCoord operator -(SquareCoord lhs, SquareCoord rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);

		public static SquareCoord WorldToSquareCoord(Vector3 worldPosition, float cellDiameter) {
			int x = Mathf.RoundToInt(worldPosition.x / cellDiameter);
			int y = Mathf.RoundToInt(worldPosition.z / cellDiameter); // using z for 2D grid in XZ plane
			return new SquareCoord(x, y);
		}

		// Convert square grid coordinate to world position (center of cell)
		public static Vector3 SquareCoordToWorldPosition(SquareCoord squareCoord, float cellDiameter) {
			float x = squareCoord.x * cellDiameter;
			float z = squareCoord.y * cellDiameter;
			return new Vector3(x, 0f, z); // y=0 for flat grid
		}

		// IEquatable
		public bool Equals(SquareCoord other) => x == other.x && y == other.y;
		public override bool Equals(object obj) => obj is SquareCoord other && Equals(other);
		public override int GetHashCode() => new Vector2Int(x, y).GetHashCode();
	}
}
