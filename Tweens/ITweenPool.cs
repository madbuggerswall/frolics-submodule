namespace Frolics.Tweens {
	public interface ITweenPool {
		T Spawn<T>() where T : ITween, new();
		void Despawn<T>(T tween) where T : ITween, new();
	}
}