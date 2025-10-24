using UnityEngine;

namespace Frolics.Grids {
	public static class GridPlaneExtensions {
		public static Vector3 PlaneToWorldPosition(this GridPlane gridPlane, Vector2 planePosition, float height) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(planePosition.x, planePosition.y, height),
				GridPlane.XZ => new Vector3(planePosition.x, height, planePosition.y),
				GridPlane.YZ => new Vector3(height, planePosition.x, planePosition.y),
				_ => new Vector3(planePosition.x, planePosition.y, height)
			};
		}

		public static Vector2 WorldToPlanePosition(this GridPlane gridPlane, Vector3 worldPosition) {
			return gridPlane switch {
				GridPlane.XY => new Vector2(worldPosition.x, worldPosition.y),
				GridPlane.XZ => new Vector2(worldPosition.x, worldPosition.z),
				GridPlane.YZ => new Vector2(worldPosition.y, worldPosition.z),
				_ => new Vector2(worldPosition.x, worldPosition.y)
			};
		}

		public static float GetOrthogonalCoordinate(this GridPlane gridPlane, Vector3 pivotPosition) {
			return gridPlane switch {
				GridPlane.XY => pivotPosition.z,
				GridPlane.XZ => pivotPosition.y,
				GridPlane.YZ => pivotPosition.x,
				_ => pivotPosition.z
			};
		}
	}
}
