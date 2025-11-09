using System;

namespace Frolics.Signals {
	public interface ISignalBus {
		public void Fire<T>(T signal) where T : ISignal;
		public void SubscribeTo<T>(Action<T> callback) where T : ISignal;
		public void UnsubscribeFrom<T>(Action<T> callback) where T : ISignal;
		public void ClearListeners<T>() where T : ISignal;
	}
}