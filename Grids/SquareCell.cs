using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	[Serializable]
	public class SquareCell : CellBase<SquareCoord> {
		[SerializeField] private SquareCoord coordinate;


		public SquareCell(SquareCoord coordinate, Vector3 position, float diameter) : base(position, diameter) {
			this.coordinate = coordinate;
		}

		public override bool IsInsideCell(Vector3 point) {
			float radius = diameter / 2f;
			return Mathf.Abs(position.x - point.x) <= radius
			    && Mathf.Abs(position.y - point.y) <= radius
			    && Mathf.Abs(position.z - point.z) <= radius;
		}

		public override SquareCoord GetCoord() => coordinate;
	}
}
