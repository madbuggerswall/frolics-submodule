using System;
using UnityEngine;

namespace Frolics.Grids {
	public abstract class CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		protected Vector3 position;
		protected float diameter;

		public Vector3 Position => position;
		public float Diameter => diameter;

		protected CellBase(Vector3 position, float diameter) {
			this.position = position;
			this.diameter = diameter;
		}

		public abstract bool IsInsideCell(Vector3 point);
		public abstract TCoord GetCoord();
	}
}
