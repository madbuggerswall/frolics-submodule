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
			if (instance is not null)
				Destroy(instance);

			instance = this;
		}

		public static ProjectContext GetInstance() => instance;
	}
}
