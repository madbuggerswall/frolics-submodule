namespace Frolics.Tweens {
	public interface ITweenPool {
		T Spawn<T>() where T : Tween, new();
		void Despawn<T>(T tween) where T : Tween, new();
	}
}
