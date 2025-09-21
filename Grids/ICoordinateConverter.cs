using System;
using UnityEngine;

namespace Frolics.Grids {
	public interface ICoordinateConverter<TCoord> where TCoord : struct, IEquatable<TCoord> {
		TCoord PlaneToCoord(Vector2 planePosition, float cellDiameter);
		Vector2 CoordToPlane(TCoord coordinate, float cellDiameter);
	}
}
