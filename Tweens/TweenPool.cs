using System.Collections.Generic;

namespace Frolics.Tweens {
	public class TweenPool : ITweenPool {
		private readonly Dictionary<System.Type, IGenericTweenPool> poolDictionary = new();

		public T Spawn<T>() where T : Tween, new() {
			if (!poolDictionary.TryGetValue(typeof(T), out IGenericTweenPool pool)) {
				pool = new GenericTweenPool<T>();
				poolDictionary.Add(typeof(T), pool);
			}

			return (T) pool.Spawn();
		}

		public void Despawn<T>(T tween) where T : Tween, new() {
			if (!poolDictionary.TryGetValue(typeof(T), out IGenericTweenPool pool)) {
				pool = new GenericTweenPool<T>();
				poolDictionary.Add(typeof(T), pool);
			}

			pool.Despawn(tween);
		}
	}
}
