using System;
using System.Collections.Generic;
using Frolics.Utilities;

namespace Frolics.Signals {
	public class SignalBus : ISignalBus, IInitializable {
		private readonly Dictionary<Type, ISignalRegistry> registries = new();

		void IInitializable.Initialize() => registries.Clear();

		void ISignalBus.Fire<T>(T signal) {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				((IGenericSignalRegistry<T>) signalRegistry).Invoke(signal);
		}

		void ISignalBus.SubscribeTo<T>(Action<T> callback) {
			if (!registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry)) {
				signalRegistry = new GenericSignalRegistry<T>();
				registries[typeof(T)] = signalRegistry;
			}

			((IGenericSignalRegistry<T>) signalRegistry).Add(callback);
		}

		void ISignalBus.UnsubscribeFrom<T>(Action<T> callback) {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				((IGenericSignalRegistry<T>) signalRegistry).Remove(callback);
		}

		void ISignalBus.ClearListeners<T>() {
			if (registries.TryGetValue(typeof(T), out ISignalRegistry signalRegistry))
				signalRegistry.Clear();
		}
	}
}
