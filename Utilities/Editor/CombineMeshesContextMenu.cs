using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Frolics.Utilities.Editor {
	public static class CombineMeshesContextMenu {
		private const string MenuPath = "Assets/Create/Mesh/";

		[MenuItem(MenuPath + "Combine Meshes")]
		private static void CombineSelectedMeshesAndSave() => CombineSelectedMeshes(false);

		[MenuItem(MenuPath + "Combine Meshes and Replace")]
		private static void CombineSelectedMeshesAndReplace() => CombineSelectedMeshes(true);

		private static void CombineSelectedMeshes(bool replaceObjects) {
			if (Selection.gameObjects.Length == 0) {
				Debug.LogWarning("No GameObjects selected to combine.");
				return;
			}

			GameObject root = Selection.gameObjects[0];
			if (root.GetComponent<MeshFilter>() == null) {
				Debug.LogError("First selected GameObject must have a MeshFilter and MeshRenderer component.");
				return;
			}

			// Prompt user for save location
			string defaultName = $"{NameFormatter.ToPascalCase(root.name).Replace("Base", "")}Combined";
			string path = PromptUserForSaveLocation(defaultName);
			if (string.IsNullOrEmpty(path)) {
				Debug.Log("Mesh combination cancelled by user.");
				return;
			}

			string folder = Path.GetDirectoryName(path) ?? "Assets/";
			string fileName = Path.GetFileNameWithoutExtension(path);

			// Group submeshes by material
			Dictionary<Material, List<CombineInstance>> submeshesByMaterial = new();
			for (int i = 0; i < Selection.gameObjects.Length; i++)
				GroupSubmeshesByMaterial(Selection.gameObjects[i], submeshesByMaterial);

			if (submeshesByMaterial.Count == 0) {
				Debug.LogWarning("No valid meshes found in selection.");
				return;
			}

			// Combine each material group into its own mesh
			CombineMaterialGroups(submeshesByMaterial, out List<Mesh> groupMeshes, out List<Material> uniqueMaterials);

			// Stitch group meshes into one mesh with multiple submeshes
			Mesh finalMesh = StitchGroupMeshes(fileName, groupMeshes);

			// Save mesh
			string meshPath = Path.Combine(folder, fileName + ".asset");
			AssetDatabase.CreateAsset(finalMesh, meshPath);

			// Save prefab
			string prefabPath = Path.Combine(folder, fileName + ".prefab");
			GameObject prefab = SavePrefab(finalMesh, uniqueMaterials, prefabPath);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			if (replaceObjects)
				ReplaceCombinedMeshes(prefab);

			EditorGUIUtility.PingObject(prefab);
			Debug.Log($"Combined mesh and prefab saved ({path}).");
		}

		private static void ReplaceCombinedMeshes(GameObject prefab) {
			if (prefab == null)
				return;

			// Instantiate prefab in scene
			GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(prefab);

			// Match root transform
			GameObject root = Selection.gameObjects[0];
			instance.transform.SetParent(root.transform.parent, true);
			instance.transform.SetPositionAndRotation(root.transform.position, root.transform.rotation);
			instance.transform.localScale = root.transform.localScale;

			// Register undo
			Undo.RegisterCreatedObjectUndo(instance, "Instantiate Combined Prefab");

			// Destroy old selection objects
			for (int i = Selection.gameObjects.Length - 1; i >= 0; i--)
				if (Selection.gameObjects[i] != null)
					Undo.DestroyObjectImmediate(Selection.gameObjects[i]);

			// Select the new instance
			Selection.activeGameObject = instance;
		}

		private static GameObject SavePrefab(Mesh finalMesh, List<Material> uniqueMaterials, string path) {
			GameObject gameObject = new(finalMesh.name);
			MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			meshFilter.sharedMesh = finalMesh;
			meshRenderer.sharedMaterials = uniqueMaterials.ToArray();

			GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, path);
			Object.DestroyImmediate(gameObject);
			return prefab;
		}

		// Helper methods
		private static void GroupSubmeshesByMaterial(
			GameObject gameObject,
			Dictionary<Material, List<CombineInstance>> submeshesByMaterial
		) {
			MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			if (meshFilter == null || meshRenderer == null || meshFilter.sharedMesh == null)
				return;

			// Transform into root-local space instead of world space
			GameObject root = Selection.gameObjects[0];
			Matrix4x4 matrix = root.transform.worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;

			Material[] sharedMaterials = meshRenderer.sharedMaterials;
			Mesh mesh = meshFilter.sharedMesh;

			for (int i = 0; i < mesh.subMeshCount; i++) {
				if (i >= sharedMaterials.Length)
					continue;

				CombineInstance combineInstance = new() { mesh = mesh, subMeshIndex = i, transform = matrix };
				if (!submeshesByMaterial.TryGetValue(sharedMaterials[i], out List<CombineInstance> submeshes)) {
					submeshes = new List<CombineInstance>();
					submeshesByMaterial[sharedMaterials[i]] = submeshes;
				}

				submeshes.Add(combineInstance);
			}
		}

		private static void CombineMaterialGroups(
			Dictionary<Material, List<CombineInstance>> submeshesByMaterial,
			out List<Mesh> groupMeshes,
			out List<Material> uniqueMaterials
		) {
			groupMeshes = new List<Mesh>();
			uniqueMaterials = new List<Material>();
			foreach ((Material material, List<CombineInstance> combineInstances) in submeshesByMaterial) {
				Mesh groupMesh = new();
				groupMesh.CombineMeshes(combineInstances.ToArray(), true, true); // merge into one submesh
				groupMeshes.Add(groupMesh);
				uniqueMaterials.Add(material);
			}
		}

		private static Mesh StitchGroupMeshes(string meshName, List<Mesh> groupMeshes) {
			List<Vector3> vertices = new();
			List<Vector3> normals = new();
			List<Vector2> uvs = new();
			List<int[]> trianglesPerSubmesh = new();

			int vertexOffset = 0;
			for (int i = 0; i < groupMeshes.Count; i++) {
				Mesh groupMesh = groupMeshes[i];

				// Append vertex data
				vertices.AddRange(groupMesh.vertices);
				normals.AddRange(groupMesh.normals);
				uvs.AddRange(groupMesh.uv);

				// Offset triangles
				int[] triangles = groupMesh.GetTriangles(0);
				for (int t = 0; t < triangles.Length; t++)
					triangles[t] += vertexOffset;

				trianglesPerSubmesh.Add(triangles);
				vertexOffset += groupMesh.vertexCount;
			}

			Mesh finalMesh = new() { name = meshName, subMeshCount = groupMeshes.Count };
			finalMesh.SetVertices(vertices);

			if (normals.Count == vertices.Count)
				finalMesh.SetNormals(normals);

			if (uvs.Count == vertices.Count)
				finalMesh.SetUVs(0, uvs);

			for (int i = 0; i < trianglesPerSubmesh.Count; i++)
				finalMesh.SetTriangles(trianglesPerSubmesh[i], i);

			finalMesh.RecalculateBounds();
			finalMesh.RecalculateNormals();
			finalMesh.RecalculateTangents();
			return finalMesh;
		}


		private static string PromptUserForSaveLocation(string fileName) {
			const string title = "Combine Meshes";
			const string extension = "asset";
			const string message = "Choose a location to save the combined mesh and prefab.";

			return EditorUtility.SaveFilePanelInProject(title, fileName, extension, message);
		}
	}
}
#endif
