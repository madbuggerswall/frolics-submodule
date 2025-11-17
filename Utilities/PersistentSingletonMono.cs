using UnityEngine;

namespace Frolics.Utilities {
	public abstract class PersistentSingletonMono<T> : SingletonMono<T> where T : MonoBehaviour {
		protected override void AssertSingleton() {
			base.AssertSingleton();
			DontDestroyOnLoad(gameObject);
		}
	}
}
