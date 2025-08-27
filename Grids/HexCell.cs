using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	[Serializable]
	public class HexCell : CellBase<AxialCoord> {
		[SerializeField] private AxialCoord coordinate;

		public HexCell(AxialCoord coordinate, Vector3 position, float diameter) : base(position, diameter) {
			this.coordinate = coordinate;
		}

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			float radiusSquared = radius * radius;
			float distanceSquared = (position - point).sqrMagnitude;

			return distanceSquared <= radiusSquared;
		}

		public override AxialCoord GetCoord() {
			return coordinate;
		}
	}
}
