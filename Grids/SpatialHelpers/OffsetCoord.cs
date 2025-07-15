namespace Frolics.Grids.SpatialHelpers {
	public struct OffsetCoord {
		public int column;
		public int row;

		public static OffsetCoord[,] directionsOddR = {
			// Even rows (row % 2 == 0)
			{
				new OffsetCoord(1, 0),
				new OffsetCoord(0, -1),
				new OffsetCoord(-1, -1),
				new OffsetCoord(-1, 0),
				new OffsetCoord(-1, 1),
				new OffsetCoord(0, 1)
			},
			// Odd rows (row % 2 == 1)
			{
				new OffsetCoord(1, 0),
				new OffsetCoord(1, -1),
				new OffsetCoord(0, -1),
				new OffsetCoord(-1, 0),
				new OffsetCoord(0, 1),
				new OffsetCoord(1, 1)
			}
		};

		public OffsetCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		public static int Distance(OffsetCoord lhs, OffsetCoord rhs) {
			
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
	}
}