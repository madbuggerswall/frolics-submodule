using System;

namespace Frolics.Signals {
	internal interface IGenericSignalRegistry<T> : ISignalRegistry where T : ISignal {
		internal void Add(Action<T> callback);
		internal void Remove(Action<T> callback);
		internal void Invoke(T signal);
	}
}