using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	public class AxialCoordinateConverter : ICoordinateConverter<AxialCoord> {
		public AxialCoord WorldToCoord(Vector3 worldPosition, float cellDiameter)
			=> AxialCoord.WorldToAxial(worldPosition, cellDiameter);

		public Vector3 CoordToWorld(AxialCoord coordinate, float cellDiameter)
			=> AxialCoord.AxialToWorld(coordinate, cellDiameter);
	}
}
