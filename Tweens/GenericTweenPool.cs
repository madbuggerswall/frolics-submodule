using System.Collections.Generic;

namespace Frolics.Tweens {
	public class GenericTweenPool<T> : IGenericTweenPool where T : Tween, new() {
		private readonly Stack<T> stack = new();

		public Tween Spawn() => stack.Count == 0 ? new T() : stack.Pop();
		public void Despawn(Tween tween) => stack.Push((T) tween);
	}
}
