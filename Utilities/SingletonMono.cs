using System;
using UnityEngine;

namespace Frolics.Utilities {
	public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
		private static T instance;

		protected virtual void Awake() {
			AssertSingleton();
		}

		private void AssertSingleton() {
			if (instance != null && instance != this) {
				Destroy(gameObject);
				return;
			}

			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}

		public static T GetInstance() => instance;

		private void OnDestroy() {
			if (instance != this)
				return;

			instance = null;
		}
	}
}
