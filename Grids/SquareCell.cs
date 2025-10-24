using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class SquareCell : CellBase<SquareCoord> {
		public SquareCell(SquareCoord coordinate) : base(coordinate) { }
	}
}
