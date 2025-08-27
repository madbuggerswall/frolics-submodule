using System;
using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	[Serializable]
	public struct DoubleWidthCoord : IEquatable<DoubleWidthCoord> {
		public static readonly DoubleWidthCoord[] DirectionVectors = {
			new(2, 0),   // East
			new(1, -1),  // North East
			new(-1, -1), // North West
			new(-2, 0),  // West
			new(-1, 1),  // South West
			new(1, 1)    // South East
		};

		public int column;
		public int row;

		public DoubleWidthCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		public static int Distance(DoubleWidthCoord lhs, DoubleWidthCoord rhs) {
			int columnDiff = Mathf.Abs(lhs.column - rhs.column);
			int rowDiff = Mathf.Abs(lhs.row - rhs.row);
			return rowDiff + Mathf.Max(0, (columnDiff - rowDiff) / 2);
		}

		// Operator overloads
		public static DoubleWidthCoord operator +(DoubleWidthCoord lhs, DoubleWidthCoord rhs)
			=> new(lhs.column + rhs.column, lhs.row + rhs.row);

		public static DoubleWidthCoord operator -(DoubleWidthCoord lhs, DoubleWidthCoord rhs)
			=> new(lhs.column - rhs.column, lhs.row - rhs.row);

		public bool Equals(DoubleWidthCoord other) => column == other.column && row == other.row;
		public override bool Equals(object obj) => obj is DoubleWidthCoord other && Equals(other);
		public override int GetHashCode() => HashCode.Combine(column, row);
		public override string ToString() => $"DoubleWidth({column}, {row})";
	}
}
