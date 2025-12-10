using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class CameraExtensions {
		/// <summary>
		/// Returns a Rect in world space that encloses the projection plane
		/// at the given distance from the camera.
		/// </summary>
		public static RectTransformDTO GetPlaneRect(this Camera camera, float planeDistance) {
			Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, planeDistance));
			Vector3 bottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, planeDistance));
			Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, planeDistance));
			Vector3 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, planeDistance));

			Vector3 center = (bottomLeft + bottomRight + topRight + topLeft) / 4f;
			Quaternion rotation = camera.transform.rotation;

			float minX = Mathf.Min(bottomLeft.x, bottomRight.x, topRight.x, topLeft.x);
			float maxX = Mathf.Max(bottomLeft.x, bottomRight.x, topRight.x, topLeft.x);
			float minY = Mathf.Min(bottomLeft.y, bottomRight.y, topRight.y, topLeft.y);
			float maxY = Mathf.Max(bottomLeft.y, bottomRight.y, topRight.y, topLeft.y);

			float halfHeight = planeDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
			float halfWidth = halfHeight * camera.aspect;
			Vector2 size = new(2 * halfWidth, 2 * halfHeight);

			return new RectTransformDTO(center, rotation, size);
		}
	}

	public struct RectTransformDTO {
		public Vector3 Position { get; }
		public Quaternion Rotation { get; }
		public Vector2 Size { get; }

		public RectTransformDTO(Vector3 position, Quaternion rotation, Vector2 size) {
			Position = position;
			Rotation = rotation;
			Size = size;
		}
	}
}
