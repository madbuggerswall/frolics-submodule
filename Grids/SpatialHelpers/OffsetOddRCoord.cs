using System;

namespace Frolics.Grids.SpatialHelpers {
	[Serializable]
	public struct OffsetOddRCoord : IEquatable<OffsetOddRCoord> {
		private static readonly OffsetOddRCoord[,] DirectionVectors = {
			// Even rows (row % 2 == 0)
			{ new(1, 0), new(0, -1), new(-1, -1), new(-1, 0), new(-1, 1), new(0, 1) },
			// Odd rows (row % 2 == 1)
			{ new(1, 0), new(1, -1), new(0, -1), new(-1, 0), new(0, 1), new(1, 1) }
		};

		public int column;
		public int row;

		public OffsetOddRCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		public bool Equals(OffsetOddRCoord other) => column == other.column && row == other.row;
		public override bool Equals(object obj) => obj is OffsetOddRCoord other && Equals(other);
		public override int GetHashCode() => HashCode.Combine(column, row);
		public override string ToString() => $"OffsetOddR({column}, {row})";
	}
}
