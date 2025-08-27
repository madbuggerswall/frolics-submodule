using System;
using UnityEngine;

namespace Frolics.Grids {
	public interface ICoordinateConverter<TCoord> where TCoord : struct, IEquatable<TCoord> {
		TCoord WorldToCoord(Vector3 worldPosition, float cellDiameter);
		Vector3 CoordToWorld(TCoord coordinate, float cellDiameter);
	}
}
