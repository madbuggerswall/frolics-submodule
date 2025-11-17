using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Grids {
	public interface ICoordinateGenerator<TCoord> where TCoord : struct, IEquatable<TCoord> {
		List<TCoord> Generate(Vector2Int gridSize);
	}
}