using System;
using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public class HexCell : CellBase<AxialCoord> {
		public HexCell(AxialCoord coordinate) : base(coordinate) { }
	}
}
