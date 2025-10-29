namespace Frolics.Tweens {
	public interface IGenericTweenPool {
		Tween Spawn();
		void Despawn(Tween tween);
	}
}