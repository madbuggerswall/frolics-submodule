using System.Collections.Generic;

namespace Frolics.Tweens.Pooling {
	internal class TweenPool : ITweenPool {
		private readonly Dictionary<System.Type, IGenericTweenPool> poolDictionary = new();

		T ITweenPool.Spawn<T>() {
			if (!poolDictionary.TryGetValue(typeof(T), out IGenericTweenPool pool)) {
				pool = new GenericTweenPool<T>();
				poolDictionary.Add(typeof(T), pool);
			}

			return (T) pool.Spawn();
		}

		void ITweenPool.Despawn<T>(T tween) {
			if (!poolDictionary.TryGetValue(typeof(T), out IGenericTweenPool pool)) {
				pool = new GenericTweenPool<T>();
				poolDictionary.Add(typeof(T), pool);
			}

			pool.Despawn(tween);
		}
	}
}
