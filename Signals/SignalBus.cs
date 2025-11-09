using System;
using System.Collections.Generic;
using Frolics.Contexts;

namespace Frolics.Signals {
	public class SignalBus : ISignalBus, IInitializable {
		private readonly Dictionary<Type, ISignalRegistry> registries = new();

		public void Initialize() => registries.Clear();

		public void Fire<T>(T signal) where T : ISignal {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				((IGenericSignalRegistry<T>) signalRegistry).Invoke(signal);
		}

		public void SubscribeTo<T>(Action<T> callback) where T : ISignal {
			if (!registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry)) {
				signalRegistry = new GenericSignalRegistry<T>();
				registries[typeof(T)] = signalRegistry;
			}

			((IGenericSignalRegistry<T>) signalRegistry).Add(callback);
		}

		public void UnsubscribeFrom<T>(Action<T> callback) where T : ISignal {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				((IGenericSignalRegistry<T>) signalRegistry).Remove(callback);
		}

		public void ClearListeners<T>() where T : ISignal {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				signalRegistry.Clear();
		}
	}
}
