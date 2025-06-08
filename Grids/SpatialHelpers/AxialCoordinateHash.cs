using System;
using System.Collections.Generic;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Grids.SpatialHelpers {
	// NOTE Also do this for SquareCells
	public class AxialCoordinateHash<T> where T : CircleCell {
		private static readonly Vector2Int[] HexNeighborOffsets = {
			new(0, 0),
			new(1, 0),
			new(1, -1),
			new(0, -1),
			new(-1, 0),
			new(-1, 1),
			new(0, 1),
		};

		private readonly Dictionary<Vector2Int, T> axialMap;

		private readonly float cellDiameter;
		private readonly T[] cells;

		public AxialCoordinateHash(CircleGrid<T> grid) {
			this.cellDiameter = grid.GetCellDiameter();
			this.cells = grid.GetCells();
			this.axialMap = MapCellsByAxialCoordinates();
		}

		private Dictionary<Vector2Int, T> MapCellsByAxialCoordinates() {
			Dictionary<Vector2Int, T> cellsByAxialCoordinates = new();

			foreach (T cell in cells) {
				Vector2Int axial = WorldToAxial(cell.GetWorldPosition().GetXY());
				if (!cellsByAxialCoordinates.TryAdd(axial, cell))
					Debug.Log("Oh no");
			}

			return cellsByAxialCoordinates;
		}

		// Use World Position → Approximate Hex Coordinate Hash
		private Vector2Int WorldToAxial(Vector2 worldPosition) {
			float worldX = worldPosition.x;
			float worldY = worldPosition.y;
			float cellRadius = cellDiameter / 2;

			// These formulas are for pointy-topped hex grids
			// float fractionalQ = (Mathf.Sqrt(3f) / 3f * worldX - 1f / 3f * worldY) / cellRadius;
			// float fractionalR = (2f / 3f * worldY) / cellRadius;

			// These formulas are for flat-topped hex grids
			float fractionalQ = (2f / 3f * worldY) / cellRadius;
			float fractionalR = (Mathf.Sqrt(3f) / 3f * worldX - 1f / 3f * worldY) / cellRadius;

			return HexRound(fractionalQ, fractionalR);
		}

		private Vector2Int HexRound(float fractionalQ, float fractionalR) {
			// Convert axial to cube coordinates
			float fractionalX = fractionalQ;
			float fractionalZ = fractionalR;
			float fractionalY = -fractionalX - fractionalZ;

			// Round each cube component
			// int roundedX = Mathf.RoundToInt(fractionalX);
			// int roundedY = Mathf.RoundToInt(fractionalY);
			// int roundedZ = Mathf.RoundToInt(fractionalZ);

			int roundedX = (int) Math.Round(fractionalX, MidpointRounding.AwayFromZero);
			int roundedY = (int) Math.Round(fractionalY, MidpointRounding.AwayFromZero);
			int roundedZ = (int) Math.Round(fractionalZ, MidpointRounding.AwayFromZero);

			// Calculate differences from the original fractional values
			float deltaX = Mathf.Abs(roundedX - fractionalX);
			float deltaY = Mathf.Abs(roundedY - fractionalY);
			float deltaZ = Mathf.Abs(roundedZ - fractionalZ);

			// Adjust the component with the greatest rounding error
			if (deltaX > deltaY && deltaX > deltaZ) {
				roundedX = -roundedY - roundedZ;
			} else if (deltaY > deltaZ) {
				roundedY = -roundedX - roundedZ;
			} else {
				roundedZ = -roundedX - roundedY;
			}

			// Convert back to axial (q = x, r = z)
			int axialQ = roundedX;
			int axialR = roundedZ;

			return new Vector2Int(axialQ, axialR);
		}

		public bool TryGetCell(Vector3 worldPosition, out T fieldCell) {
			Vector2Int centerAxial = WorldToAxial(worldPosition);

			// Search center + neighbors (hex rings of radius 1)
			foreach (Vector2Int offset in HexNeighborOffsets) {
				Vector2Int axial = centerAxial + offset;

				if (axialMap.TryGetValue(axial, out T candidate)) {
					if (candidate.IsInsideCell(worldPosition)) {
						fieldCell = candidate;
						return true;
					}
				}
			}

			fieldCell = null;
			return false;
		}
	}
}
