using System.Collections.Generic;

namespace Frolics.Tweens {
	public class GenericTweenPool<T> : ITweenPool<T> where T : ITween, new() {
		private readonly Stack<T> stack = new();

		public T Spawn() => stack.Count == 0 ? new T() : stack.Pop();
		public void Despawn(T tween) => stack.Push(tween);
	}
}