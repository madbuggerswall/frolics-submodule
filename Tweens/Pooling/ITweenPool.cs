using Frolics.Tweens.Core;

namespace Frolics.Tweens.Pooling {
	internal interface ITweenPool {
		internal T Spawn<T>() where T : Tween, new();
		internal void Despawn<T>(T tween) where T : Tween, new();
	}
}
