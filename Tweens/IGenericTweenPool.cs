namespace Frolics.Tweens {
	public interface IGenericTweenPool {
		ITween Spawn();
		void Despawn(ITween tween);
	}
}