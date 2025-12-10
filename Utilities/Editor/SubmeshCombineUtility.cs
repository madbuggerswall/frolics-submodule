using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Frolics.Utilities.Editor {
	public static class SubmeshCombineUtility {
		/// <summary>
		/// Converts a mesh into a single-submesh mesh by merging all submeshes.
		/// </summary>
		public static Mesh Combine(Mesh sourceMesh) {
			if (sourceMesh == null) {
				Debug.LogWarning("Source mesh is null.");
				return null;
			}

			Mesh singleMesh = new() {
				name = NameFormatter.ToPascalCase($"{sourceMesh.name}SingleSubmesh"),
				vertices = sourceMesh.vertices,
				normals = sourceMesh.normals,
				uv = sourceMesh.uv
			};

			// Merge all triangles into one submesh
			List<int> allTriangles = new List<int>();
			for (int i = 0; i < sourceMesh.subMeshCount; i++)
				allTriangles.AddRange(sourceMesh.GetTriangles(i));

			singleMesh.SetTriangles(allTriangles, 0);
			singleMesh.RecalculateBounds();
			singleMesh.RecalculateNormals();
			singleMesh.RecalculateTangents();

			return singleMesh;
		}

		/// <summary>
		/// Converts a mesh into a single-submesh mesh and saves it as an asset.
		/// </summary>
		public static Mesh CombineAndSave(Mesh sourceMesh) {
			Mesh combinedMesh = Combine(sourceMesh);
			string path = PromptUserForSaveLocation(combinedMesh.name);
			string fileName = Path.GetFileNameWithoutExtension(path);
			
			if (string.IsNullOrEmpty(path)) {
				Debug.Log("Mesh combination cancelled by user.");
				return combinedMesh;
			}
			
			combinedMesh.name = fileName;

			// Save mesh asset
			AssetDatabase.CreateAsset(combinedMesh, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			EditorGUIUtility.PingObject(combinedMesh);
			Debug.Log($"Combined mesh ({combinedMesh.name}) saved at {path}");

			return combinedMesh;
		}

		private static string PromptUserForSaveLocation(string fileName) {
			const string title = "Combine Submeshes";
			const string extension = "asset";
			const string message = "Choose a location to save the combined mesh.";

			return EditorUtility.SaveFilePanelInProject(title, fileName, extension, message);
		}
	}
}

#endif