using System;
using System.Collections.Generic;
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


		private T ResolveFromContext<T>() where T : class {
			return contexts.TryGetValue(typeof(T), out DependencyContext context)
				? context.Resolve<T>()
				: throw new DependencyResolutionException(typeof(T));
		}

		internal static Context GetInstance() => instance ??= new Context();
	}
}
