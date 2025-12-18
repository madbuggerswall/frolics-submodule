using System.Collections.Generic;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Pooling {
	/// <summary>
	/// Generic object pool for both GameObjects and MonoBehaviours.
	/// Uses prefab reference as the key to avoid type collisions.
	/// </summary>
	public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour {
		private readonly Dictionary<GameObject, Stack<T>> poolDictionary = new();
		private readonly Dictionary<T, GameObject> instanceToPrefab = new(); // reverse lookup
		private readonly HashSet<T> pooledInstances = new();                 // explicit inactive tracking
		private readonly Transform root;

		public ObjectPool(Transform root) {
			this.root = root;
		}

		private T AddObject(T prefab) {
			T instance = Object.Instantiate(prefab, root, true);
			instance.gameObject.SetActive(false);
			instance.name = prefab.name + " (Pooled)";

			instanceToPrefab[instance] = prefab.gameObject;
			pooledInstances.Add(instance); // new instances start pooled

			return instance;
		}

		private T GetObject(T prefab) {
			if (!poolDictionary.TryGetValue(prefab.gameObject, out Stack<T> pool)) {
				pool = new Stack<T>();
				poolDictionary[prefab.gameObject] = pool;
			}

			T instance = pool.Count > 0 ? pool.Pop() : AddObject(prefab);
			pooledInstances.Remove(instance);
			return instance;
		}

		/// <summary>
		/// Spawns an object from the pool.
		/// </summary>
		T IObjectPool<T>.Spawn(T prefab) {
			T instance = GetObject(prefab);
			instance.transform.SetParent(root, false);
			instance.gameObject.SetActive(true);

			return instance;
		}

		T IObjectPool<T>.Spawn(T prefab, Transform parent) {
			T instance = GetObject(prefab);
			instance.transform.SetParent(parent, false);
			instance.gameObject.SetActive(true);

			return instance;
		}

		/// <summary>
		/// Returns an object to the pool.
		/// </summary>
		void IObjectPool<T>.Despawn(T instance) {
			if (!instanceToPrefab.TryGetValue(instance, out GameObject prefabKey)) {
				EditorLog.LogWarning($"Trying to despawn {instance.name}, but it was not pooled.");
				return;
			}

			// Guard against double-despawn
			if (pooledInstances.Contains(instance)) {
				EditorLog.LogWarning($"Instance {instance.name} already despawned!");
				return;
			}

			instance.gameObject.SetActive(false);
			instance.transform.SetParent(root, true);

			poolDictionary[prefabKey].Push(instance);
			pooledInstances.Add(instance); // mark inactive
		}

		/// <summary>
		/// Adopts an existing instance into the pool.
		/// Useful if the object was instantiated outside the pool.
		/// </summary>
		void IObjectPool<T>.Adopt(T instance, T prefab) {
			// If already tracked, ignore
			if (!instanceToPrefab.TryAdd(instance, prefab.gameObject))
				return;

			// Ensure prefab key exists in dictionary
			if (!poolDictionary.TryGetValue(prefab.gameObject, out Stack<T> pool)) {
				pool = new Stack<T>();
				poolDictionary[prefab.gameObject] = pool;
			}

			// Normalize state
			instance.name = prefab.name + " (Pooled)";
			instance.gameObject.SetActive(false);
			instance.transform.SetParent(root, true);

			// Push into pool
			pool.Push(instance);
		}
	}
}
