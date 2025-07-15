namespace Frolics.Grids.SpatialHelpers {
	public struct DoubledCoord {
		public int column;
		public int row;

		public static DoubledCoord[] doubleWidthDirections = {
			new DoubledCoord(2, 0),
			new DoubledCoord(1, -1),
			new DoubledCoord(-1, -1),
			new DoubledCoord(-2, 0),
			new DoubledCoord(-1, 1),
			new DoubledCoord(1, 1)
		};

		public DoubledCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		// Operator overloads
		public static DoubledCoord operator +(DoubledCoord lhs, DoubledCoord rhs) {
			return new DoubledCoord(lhs.column + rhs.column, lhs.row + rhs.row);
		}

		public static DoubledCoord operator -(DoubledCoord lhs, DoubledCoord rhs) {
			return new DoubledCoord(lhs.column - rhs.column, lhs.row - rhs.row);
		}

		public DoubledCoord GetDoubleWidthNeighbor(DoubledCoord center, int directionIndex) {
			return center + doubleWidthDirections[directionIndex];
		}


		// NOTE Redundant for pointy top
		public static AxialCoord DoubleHeightToAxial(DoubledCoord doubledCoord) {
			int q = doubledCoord.column;
			int r = (doubledCoord.row - doubledCoord.column) / 2;
			return new AxialCoord(q, r);
		}

		// NOTE Redundant for pointy top
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
}