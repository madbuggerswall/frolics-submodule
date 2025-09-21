using UnityEngine;

namespace Frolics.Grids {
	public static class GridPlaneExtensions {
		public static Vector3 PlaneToWorldPosition(this GridPlane gridPlane, Vector2 position, float height) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(position.x, position.y, height),
				GridPlane.XZ => new Vector3(position.x, height, position.y),
				GridPlane.YZ => new Vector3(height, position.x, position.y),
				_ => new Vector3(position.x, position.y, height)
			};
		}

		public static Vector2 WorldToPlanePosition(this GridPlane gridPlane, Vector3 planePosition) {
			return gridPlane switch {
				GridPlane.XY => new Vector2(planePosition.x, planePosition.y),
				GridPlane.XZ => new Vector2(planePosition.x, planePosition.z),
				GridPlane.YZ => new Vector2(planePosition.y, planePosition.z),
				_ => new Vector2(planePosition.x, planePosition.y)
			};
		}

		public static float GetPlaneHeight(this GridPlane gridPlane, Vector3 pivotPosition) {
			return gridPlane switch {
				GridPlane.XY => pivotPosition.z,
				GridPlane.XZ => pivotPosition.y,
				GridPlane.YZ => pivotPosition.x,
				_ => pivotPosition.z
			};
		}
	}
}
