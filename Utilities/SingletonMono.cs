using UnityEngine;

namespace Frolics.Utilities {
	public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
		private static T instance;

		protected virtual void Awake() {
			AssetSingleton();
		}

		private void AssetSingleton() {
			if (instance != null && instance != this) {
				Destroy(gameObject);
				return;
			}

			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}

		public static T GetInstance() => instance;
		private void OnDestroy() {}
	}
}
