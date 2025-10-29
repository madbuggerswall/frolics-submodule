using System.Collections.Generic;

namespace Frolics.Tweens {
	public class TweenPool {
		private readonly Dictionary<System.Type, ITweenPool<Tween>> poolDictionary = new();

		public T Spawn<T>() where T : Tween {
			if (!poolDictionary.TryGetValue(typeof(T), out ITweenPool<Tween> pool)) {
				pool = new GenericTweenPool<Tween>();
				poolDictionary.Add(typeof(T), pool);
			}

			return pool.Spawn() as T;
		}

		public void Despawn<T>(T tween) where T : Tween {
			if (!poolDictionary.TryGetValue(typeof(T), out ITweenPool<Tween> pool)) {
				pool = new GenericTweenPool<Tween>();
				poolDictionary.Add(typeof(T), pool);
			}

			pool.Despawn(tween);
		}
	}
}