using UnityEngine;

namespace Frolics.Utilities {
	public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
		private static T instance;

		protected virtual void Awake() {
			AssertSingleton();
		}

		protected virtual void AssertSingleton() {
			if (instance != null && instance != this) {
				Destroy(gameObject);
				return;
			}

			instance = this as T;
		}

		public static T GetInstance() => instance;

		protected void OnDestroy() {
			if (instance != this)
				return;

			instance = null;
		}
	}
}
