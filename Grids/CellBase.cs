using System;
using UnityEngine;

namespace Frolics.Grids {
	[Serializable]
	public abstract class CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		[SerializeField] protected Vector3 position;
		[SerializeField] protected float diameter;

		public Vector3 Position => position;
		public float Diameter => diameter;

		protected CellBase(Vector3 position, float diameter) {
			if (diameter <= 0)
				throw new ArgumentException("Diameter must be positive", nameof(diameter));

			this.position = position;
			this.diameter = diameter;
		}

		public abstract bool IsInsideCell(Vector3 point);
		public abstract TCoord GetCoord();
	}
}
