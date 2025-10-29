namespace Frolics.Tweens {
	internal interface IGenericTweenPool {
		internal Tween Spawn();
		internal void Despawn(Tween tween);
	}
}