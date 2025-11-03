using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class VectorExtensions {
		public static Vector2 GetXY(this Vector3 vector) => new(vector.x, vector.y);
		public static Vector2 GetXZ(this Vector3 vector) => new(vector.x, vector.z);
		public static Vector2 GetYZ(this Vector3 vector) => new(vector.y, vector.z);

		public static Vector3 WithX(this Vector3 vector, float x) => new(x, vector.y, vector.z);
		public static Vector3 WithY(this Vector3 vector, float y) => new(vector.x, y, vector.z);
		public static Vector3 WithZ(this Vector3 vector, float z) => new(vector.x, vector.y, z);

		public static Vector2 WithX(this Vector2 vector, float x) => new(x, vector.y);
		public static Vector2 WithY(this Vector2 vector, float y) => new(vector.x, y);
		public static Vector3 WithZ(this Vector2 vector, float z) => new(vector.x, vector.y, z);


		public static Vector2Int GetXY(this Vector3Int vector) => new(vector.x, vector.y);
		public static Vector2Int GetXZ(this Vector3Int vector) => new(vector.x, vector.z);
		public static Vector2Int GetYZ(this Vector3Int vector) => new(vector.y, vector.z);

		public static Vector3Int WithX(this Vector3Int vector, int x) => new(x, vector.y, vector.z);
		public static Vector3Int WithY(this Vector3Int vector, int y) => new(vector.x, y, vector.z);
		public static Vector3Int WithZ(this Vector3Int vector, int z) => new(vector.x, vector.y, z);

		public static Vector2Int WithX(this Vector2Int vector, int x) => new(x, vector.y);
		public static Vector2Int WithY(this Vector2Int vector, int y) => new(vector.x, y);
		public static Vector3Int WithZ(this Vector2Int vector, int z) => new(vector.x, vector.y, z);
	}
}
