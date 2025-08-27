using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	public static class AxialConversionExtensions {
		// Axial conversions
		public static Vector2Int ToVector2Int(this AxialCoord axialCoord)
			=> new(axialCoord.q, axialCoord.r);

		public static CubeCoord ToCube(this AxialCoord axialCoord)
			=> new(axialCoord.q, axialCoord.r, -axialCoord.q - axialCoord.r);

		public static DoubleWidthCoord ToDoubleWidth(this AxialCoord axialCoord)
			=> new(2 * axialCoord.q + axialCoord.r, axialCoord.r);

		public static OffsetOddRCoord ToOddR(this AxialCoord axialCoord) {
			int parity = axialCoord.r & 1;
			int column = axialCoord.q + (axialCoord.r - parity) / 2;
			return new OffsetOddRCoord(column, axialCoord.r);
		}

		// Cube conversions
		public static Vector3Int ToVector3Int(this CubeCoord cubeCoord)
			=> new(cubeCoord.q, cubeCoord.r, cubeCoord.s);

		public static Vector3 ToVector3(this CubeCoord cubeCoord)
			=> new(cubeCoord.q, cubeCoord.r, cubeCoord.s);

		public static AxialCoord ToAxial(this CubeCoord cubeCoord)
			=> new(cubeCoord.q, cubeCoord.r);

		// Offset coordinate conversions
		public static AxialCoord ToAxial(this OffsetOddRCoord offsetOddRCoord) {
			int parity = offsetOddRCoord.row & 1;
			int q = offsetOddRCoord.column - (offsetOddRCoord.row - parity) / 2;
			return new AxialCoord(q, offsetOddRCoord.row);
		}

		public static AxialCoord ToAxial(this DoubleWidthCoord doubleWidthCoord)
			=> new((doubleWidthCoord.column - doubleWidthCoord.row) / 2, doubleWidthCoord.row);
	}
}
