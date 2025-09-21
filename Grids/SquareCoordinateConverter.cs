using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareCoordinateConverter : ICoordinateConverter<SquareCoord> {
		public SquareCoord PlaneToCoord(Vector2 planePosition, float cellDiameter)
			=> SquareCoord.PlaneToSquareCoord(planePosition, cellDiameter);

		public Vector2 CoordToPlane(SquareCoord coordinate, float cellDiameter)
			=> SquareCoord.SquareCoordToPlane(coordinate, cellDiameter);
	}
}
