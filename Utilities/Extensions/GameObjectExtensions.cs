using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class GameObjectExtensions {
		public static Bounds CalculateMeshBounds(this GameObject gameObject) {
			MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

			if (meshRenderers.Length == 0)
				return new Bounds(gameObject.transform.position, Vector3.zero);

			Bounds combinedBounds = meshRenderers[0].bounds;
			for (int i = 1; i < meshRenderers.Length; i++)
				combinedBounds.Encapsulate(meshRenderers[i].bounds);

			return combinedBounds;
		}
	}
}
