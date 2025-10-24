using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareGridCoordinateGenerator : ICoordinateGenerator<SquareCoord> {
		public List<SquareCoord> Generate(Vector2Int gridSize, SquareCoord origin) {
			List<SquareCoord> coords = new(gridSize.x * gridSize.y);

			for (int y = 0; y < gridSize.y; y++)
				for (int x = 0; x < gridSize.x; x++)
					coords.Add(new SquareCoord(x, y) + origin);

			return coords;
		}
	}
}
