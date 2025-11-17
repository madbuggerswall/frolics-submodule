using UnityEngine;

namespace Frolics.Contexts {
	// Curiously recurring template pattern
	[DefaultExecutionOrder(-24)]
	public abstract class SubContext<T> : DependencyContext where T : SubContext<T> {
		private static T instance;

		private void Awake() {
			AssertSingleton();
			BindContext();
			InitializeContext();
			OnInitialized();
		}

		// Singleton Operations
		private void AssertSingleton() {
			if (instance != null && instance != this) {
				Destroy(gameObject);
				return;
			}

			instance = this as T;
		}

		protected void OnDestroy() {
			if (instance == this)
				instance = null;
		}

		public static T GetInstance() => instance;
	}
}
