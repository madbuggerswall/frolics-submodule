using UnityEngine;

namespace Frolics.Contexts {
	[DefaultExecutionOrder(-28)]
	public abstract class SceneContext : Context {
		private static SceneContext instance;

		private void Awake() {
			AssertSingleton();
			ResolveContext();
			InitializeContext();
			OnInitialized();
		}

		// Singleton Operations
		private void AssertSingleton() {
			if (instance is not null)
				Destroy(instance);

			instance = this;
		}

		public static SceneContext GetInstance() => instance;
	}
}
