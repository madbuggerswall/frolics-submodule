using System.Collections.Generic;
using UnityEngine;

namespace Frolics.Pooling {
	/// <summary>
	/// Generic object pool for both GameObjects and MonoBehaviours.
	/// Uses prefab reference as the key to avoid type collisions.
	/// </summary>
	public class ObjectPool<T> where T : Component {
		private readonly Dictionary<T, Stack<T>> poolDictionary = new();
		private readonly Dictionary<T, T> instanceToPrefab = new(); // reverse lookup
		private readonly Transform root;

		public ObjectPool(Transform root) {
			this.root = root;
		}

		private T AddObject(T prefab, Stack<T> pool) {
			T instance = Object.Instantiate(prefab, root, true);
			instance.gameObject.SetActive(false);
			instance.name = prefab.name + " (Pooled)";

			pool.Push(instance);
			instanceToPrefab[instance] = prefab;

			return instance;
		}

		private T GetObject(T prefab) {
			if (poolDictionary.TryGetValue(prefab, out Stack<T> pool))
				return pool.Count > 0 ? pool.Pop() : AddObject(prefab, pool);

			pool = new Stack<T>();
			poolDictionary[prefab] = pool;

			return pool.Count > 0 ? pool.Pop() : AddObject(prefab, pool);
		}

		/// <summary>
		/// Spawns an object from the pool.
		/// </summary>
		public T Spawn(T prefab, Transform parent = null) {
			T instance = GetObject(prefab);
			instance.transform.SetParent(parent ?? root, false);
			instance.gameObject.SetActive(true);

			return instance;
		}

		/// <summary>
		/// Returns an object to the pool.
		/// </summary>
		public void Despawn(T instance) {
			if (!instanceToPrefab.TryGetValue(instance, out T prefab)) {
				Debug.LogWarning($"Trying to despawn {instance.name}, but it was not pooled.");
				return;
			}

			instance.gameObject.SetActive(false);
			instance.transform.SetParent(root, false);
			poolDictionary[prefab].Push(instance);
		}
	}
}
