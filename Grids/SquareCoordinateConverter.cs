using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareCoordinateConverter : ICoordinateConverter<SquareCoord> {
		public SquareCoord WorldToCoord(Vector3 worldPosition, float cellDiameter)
			=> SquareCoord.WorldToSquareCoord(worldPosition, cellDiameter);

		public Vector3 CoordToWorld(SquareCoord coordinate, float cellDiameter)
			=> SquareCoord.SquareCoordToWorld(coordinate, cellDiameter);
	}
}
