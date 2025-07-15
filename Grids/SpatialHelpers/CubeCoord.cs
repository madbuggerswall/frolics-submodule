using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	public struct CubeCoord {
		public int q;
		public int r;
		public int s;

		public static CubeCoord[] directionVectors = {
			new CubeCoord(1, 0, -1), // East
			new CubeCoord(0, 1, -1), // North East
			new CubeCoord(-1, 1, 0), // North West
			new CubeCoord(-1, 0, 1), // West
			new CubeCoord(0, -1, 1), // South West
			new CubeCoord(1, -1, 0) // South East
		};

		public CubeCoord(int q, int r, int s) {
			this.q = q;
			this.r = r;
			this.s = s;
		}


		public static int Distance(CubeCoord lhs, CubeCoord rhs) {
			CubeCoord vec = lhs - rhs;
			return (Mathf.Abs(vec.q) + Mathf.Abs(vec.r) + Mathf.Abs(vec.s)) / 2;
		}

		public CubeCoord GetDirection(int directionIndex) {
			return directionVectors[directionIndex];
		}

		public CubeCoord GetNeighbor(CubeCoord center, int neighborIndex) {
			return center + GetDirection(neighborIndex);
		}

		// Operator overloads
		public static CubeCoord operator +(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q + rhs.q, lhs.r + rhs.r, lhs.s + rhs.s);
		}

		public static CubeCoord operator -(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q - rhs.q, lhs.r - rhs.r, lhs.s - rhs.s);
		}


		// Optional: equality support
		public override bool Equals(object obj) {
			return obj is CubeCoord other && q == other.q && r == other.r && s == other.s;
		}

		public override int GetHashCode() => new Vector3Int(q, r, s).GetHashCode();

		public static bool operator ==(CubeCoord lhs, CubeCoord rhs) {
			return lhs.q == rhs.q && lhs.r == rhs.r && lhs.s == rhs.s;
		}

		public static bool operator !=(CubeCoord lhs, CubeCoord rhs) => !(lhs == rhs);

		// TODO Migrate this to an extension class
		public static AxialCoord ToAxialCoord(CubeCoord cubeCoord) {
			int axialQ = cubeCoord.q;
			int axialR = cubeCoord.r;
			return new AxialCoord(axialQ, axialR);
		}
	}
}