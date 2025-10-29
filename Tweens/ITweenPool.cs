namespace Frolics.Tweens {
	public interface ITweenPool<T> where T : ITween {
		T Spawn();
		void Despawn(T tween);
	}
}