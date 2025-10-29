using System.Collections.Generic;

namespace Frolics.Tweens {
	public class GenericTweenPool<T> : IGenericTweenPool where T : ITween, new() {
		private readonly Stack<T> stack = new();

		public ITween Spawn() {
			return stack.Count == 0 ? new T() : stack.Pop();
		}

		public void Despawn(ITween tween) {
			tween.Reset();
			stack.Push((T) tween);
		}
	}
}