using System.Collections.Generic;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class HexGridCoordGenerator : ICoordinateGenerator<AxialCoord> {
		public List<AxialCoord> Generate(Vector2Int gridSize, AxialCoord origin) {
			int evenRowCount = Mathf.CeilToInt(gridSize.y / 2f);
			List<AxialCoord> coords = new(gridSize.x * gridSize.y + evenRowCount);

			for (int y = 0; y < gridSize.y; y++) {
				int rowSize = y % 2 == 0 ? (gridSize.x + 1) : gridSize.x;
				for (int x = 0; x < rowSize; x++)
					coords.Add(new OffsetOddRCoord(x, -y).ToAxial() + origin);
			}

			return coords;
		}
	}
}
