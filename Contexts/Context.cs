using System;
using System.Collections.Generic;
using Frolics.Contexts.Exceptions;
using UnityEngine;

namespace Frolics.Contexts {
	public class Context {
		private static Context instance;

		private readonly Dictionary<Type, DependencyContext> contexts = new();

		public static T Resolve<T>() where T : class {
			return GetInstance().ResolveFromContext<T>();
		}

		internal void RegisterContext<T>(DependencyContext dependencyContext) {
			if (!contexts.TryAdd(typeof(T), dependencyContext))
				Debug.LogWarning($"Dependency ({typeof(T)}) is already registered!");
		}
		
		// Panic
		internal void ClearContext() {
			contexts.Clear();
		}


		private T ResolveFromContext<T>() where T : class {
			return contexts.TryGetValue(typeof(T), out DependencyContext context)
				? context.Resolve<T>()
				: throw new DependencyResolutionException(typeof(T));
		}

		internal static Context GetInstance() => instance ??= new Context();
	}
}
