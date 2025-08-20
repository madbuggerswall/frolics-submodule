using Frolics.Grids.SpatialHelpers;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Grids {
	/// <summary>
	///	Represents a pointy-top hexagonal grid cell.
	/// </summary>
	public class HexCell : Cell {
		protected AxialCoord axialCoord;

		protected HexCell(AxialCoord axialCoord, Vector3 position, float diameter) : base(position, diameter) {
			this.axialCoord = axialCoord;
		}

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			float radiusSquared = radius * radius;

			// Disregard Z since it's a 2D operation
			float distanceSquared = (position.GetXY() - point.GetXY()).sqrMagnitude;
			return distanceSquared <= radiusSquared;
		}

		public void SetAxialCoord(AxialCoord axialCoord) => this.axialCoord = axialCoord;
		public AxialCoord GetAxialCoord() => this.axialCoord;
	}
}
