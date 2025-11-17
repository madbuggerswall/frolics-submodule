using UnityEngine;

namespace Frolics.Contexts {
	[DefaultExecutionOrder(-28)]
	public abstract class ProjectContext : DependencyContext {
		private static ProjectContext instance;

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

			instance = this;
		}

		protected void OnDestroy() {
			if (instance == this)
				instance = null;
		}

		public static ProjectContext GetInstance() => instance;
	}
}
