using UnityEngine;

namespace Frolics.Grids {
	public static class GridPlaneExtensions {
		public static Vector3 ConvertPositionPlane(this GridPlane gridPlane, float posX, float posY) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(posX, posY),
				GridPlane.XZ => new Vector3(posX, 0, posY),
				GridPlane.YZ => new Vector3(0, posX, posY),
				_ => new Vector3(posX, posY)
			};
		}
	}
}