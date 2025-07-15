using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
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

		// No matter which way you write it, axial hex distance is derived from the Manhattan distance on cubes.
		public int Distance(AxialCoord lhs, AxialCoord rhs) {
			CubeCoord lhsCube = ToCubeCoord(lhs);
			CubeCoord rhsCube = ToCubeCoord(rhs);
			return CubeCoord.Distance(lhsCube, rhsCube);
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
}