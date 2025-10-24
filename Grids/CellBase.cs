using System;
using UnityEngine;

namespace Frolics.Grids {
	// Might be a ICell interface
	public abstract class CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		private readonly TCoord coordinate;

		protected CellBase(TCoord coordinate) {
			this.coordinate = coordinate;
		}

		public TCoord GetCoord() => coordinate;
	}
}
