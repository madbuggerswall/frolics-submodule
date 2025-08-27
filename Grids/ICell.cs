using Frolics.Grids.SpatialHelpers;
using UnityEngine;

namespace Frolics.Grids {
	public interface ICell {
		Vector3 Position { get; }
		float Diameter { get; }
		bool IsInsideCell(Vector3 point);
	}
}
