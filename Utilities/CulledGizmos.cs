using UnityEditor;
using UnityEngine;

namespace Frolics.Utilities {
	public static class CulledGizmos {
		#if UNITY_EDITOR
		private static Plane[] frustumPlanes;
		private static int lastFrame = -1;

		/// <summary>
		/// Update frustum planes once per frame from the Scene view camera.
		/// </summary>
		private static void UpdateFrustum() {
			int frame = Time.frameCount;
			if (lastFrame == frame)
				return; // already updated this frame

			Camera cam = SceneView.lastActiveSceneView == null ? null : SceneView.lastActiveSceneView.camera;
			frustumPlanes = cam != null ? GeometryUtility.CalculateFrustumPlanes(cam) : null;
			lastFrame = frame;
		}

		private static bool IsVisible(Bounds bounds) {
			UpdateFrustum();
			return frustumPlanes == null || GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);
		}
		#endif

		public static void DrawCube(Vector3 center, Vector3 size, Color color) {
			#if UNITY_EDITOR
			Bounds bounds = new(center, size);
			if (!IsVisible(bounds))
				return;
			#endif

			Gizmos.color = color;
			Gizmos.DrawCube(center, size);
		}

		public static void DrawWireCube(Vector3 center, Vector3 size, Color color) {
			#if UNITY_EDITOR
			Bounds bounds = new(center, size);
			if (!IsVisible(bounds))
				return;
			#endif

			Gizmos.color = color;
			Gizmos.DrawWireCube(center, size);
		}

		public static void DrawSphere(Vector3 center, float radius, Color color) {
			#if UNITY_EDITOR
			Bounds bounds = new(center, Vector3.one * radius * 2f);
			if (!IsVisible(bounds))
				return;
			#endif

			Gizmos.color = color;
			Gizmos.DrawSphere(center, radius);
		}

		public static void DrawLine(Vector3 from, Vector3 to, Color color) {
			#if UNITY_EDITOR
			Vector3 center = (from + to) * 0.5f;
			Vector3 size = new Vector3(Mathf.Abs(from.x - to.x), Mathf.Abs(from.y - to.y), Mathf.Abs(from.z - to.z));
			Bounds bounds = new(center, size);

			if (!IsVisible(bounds))
				return;
			#endif

			Gizmos.color = color;
			Gizmos.DrawLine(from, to);
		}

		// Extend with other Gizmos methods as needed:
		// DrawRay, DrawIcon, etc.
	}
}
