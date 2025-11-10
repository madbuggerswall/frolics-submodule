using System;
using System.Collections.Generic;

namespace Frolics.Signals {
	public class GenericSignalRegistry<T> : IGenericSignalRegistry<T> where T : ISignal {
		private readonly HashSet<Action<T>> callbacks = new();

		void IGenericSignalRegistry<T>.Add(Action<T> callback) {
			callbacks.Add(callback);
		}

		void IGenericSignalRegistry<T>.Remove(Action<T> callback) {
			callbacks.Remove(callback);
		}

		void IGenericSignalRegistry<T>.Invoke(T signal) {
			foreach (Action<T> callback in callbacks)
				callback(signal);
		}

		void ISignalRegistry.Clear() => callbacks.Clear();
	}
}
