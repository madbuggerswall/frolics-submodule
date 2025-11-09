using System;

namespace Frolics.Signals {
	public interface IGenericSignalRegistry<T> : ISignalRegistry where T : ISignal {
		void Add(Action<T> callback);
		void Remove(Action<T> callback);
		void Invoke(T signal);
	}
}