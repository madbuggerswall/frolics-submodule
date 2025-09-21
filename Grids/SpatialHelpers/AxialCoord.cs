using System;
using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	[Serializable]
	public struct AxialCoord : IEquatable<AxialCoord> {
		public static readonly AxialCoord[] DirectionVectors = {
			new(1, 0),  // East
			new(1, -1), // North East
			new(0, -1), // North West
			new(-1, 0), // West
			new(-1, 1), // South West
			new(0, 1)   // South East
		};

		public int q;
		public int r;

		public AxialCoord(int q, int r) {
			this.q = q;
			this.r = r;
		}

		public static int Distance(AxialCoord lhs, AxialCoord rhs) {
			CubeCoord lhsCube = lhs.ToCube();
			CubeCoord rhsCube = rhs.ToCube();
			return CubeCoord.Distance(lhsCube, rhsCube);
		}

		public static Vector2 AxialToPlane(AxialCoord axialCoord, float cellDiameter) {
			float cellRadius = cellDiameter / 2f;
			float size = 2f / Mathf.Sqrt(3f) * cellRadius;
			float x = Mathf.Sqrt(3) * axialCoord.q + Mathf.Sqrt(3) / 2 * axialCoord.r;
			float y = 3f / 2f * axialCoord.r;
			return new Vector2(x * size, -y * size);
		}

		public static AxialCoord PlaneToAxial(Vector2 worldPosition, float cellDiameter) {
			float cellRadius = cellDiameter / 2f;
			float size = 2f / Mathf.Sqrt(3f) * cellRadius;
			float x = worldPosition.x / size;
			float y = -worldPosition.y / size;

			// Cartesian to hex
			float fractionalQ = Mathf.Sqrt(3f) / 3f * x - 1f / 3f * y;
			float fractionalR = 2f / 3f * y;
			float fractionalS = -fractionalQ - fractionalR;

			CubeCoord cubeCoord = CubeCoord.Round(fractionalQ, fractionalR, fractionalS);
			return cubeCoord.ToAxial();
		}

		public override string ToString() => $"Axial({q}, {r})";

		// Operator overloads
		public static AxialCoord operator +(AxialCoord lhs, AxialCoord rhs) => new(lhs.q + rhs.q, lhs.r + rhs.r);
		public static AxialCoord operator -(AxialCoord lhs, AxialCoord rhs) => new(lhs.q - rhs.q, lhs.r - rhs.r);

		// IEquatable implementation
		public bool Equals(AxialCoord other) => q == other.q && r == other.r;
		public override bool Equals(object obj) => obj is AxialCoord other && Equals(other);
		public override int GetHashCode() => HashCode.Combine(q, r);

		public static bool operator ==(AxialCoord lhs, AxialCoord rhs) => lhs.Equals(rhs);
		public static bool operator !=(AxialCoord lhs, AxialCoord rhs) => !lhs.Equals(rhs);
	}
}
