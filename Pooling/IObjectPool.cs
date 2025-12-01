using UnityEngine;

namespace Frolics.Pooling {
	public interface IObjectPool<T> where T : MonoBehaviour {
		public T Spawn(T prefab);
		public T Spawn(T prefab, Transform parent);
		public void Despawn(T instance);
		public void Adopt(T instance, T prefab);
	}
}
