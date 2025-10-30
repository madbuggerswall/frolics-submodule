using Frolics.Tweens.Core;

namespace Frolics.Tweens.Pooling {
	internal interface IGenericTweenPool {
		internal Tween Spawn();
		internal void Despawn(Tween tween);
	}
}