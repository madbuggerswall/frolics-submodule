using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	public class AxialCoordinateConverter : ICoordinateConverter<AxialCoord> {
		public AxialCoord PlaneToCoord(Vector2 planePosition, float cellDiameter)
			=> AxialCoord.PlaneToAxial(planePosition, cellDiameter);

		public Vector2 CoordToPlane(AxialCoord coordinate, float cellDiameter)
			=> AxialCoord.AxialToPlane(coordinate, cellDiameter);
	}
}
