using System.Collections.Generic;
using Frolics.Tweens.Core;

namespace Frolics.Tweens.Pooling {
	internal class GenericTweenPool<T> : IGenericTweenPool where T : Tween, new() {
		private readonly Stack<T> stack = new();

		Tween IGenericTweenPool.Spawn() => stack.Count == 0 ? new T() : stack.Pop();
		void IGenericTweenPool.Despawn(Tween tween) => stack.Push((T) tween);
	}
}
