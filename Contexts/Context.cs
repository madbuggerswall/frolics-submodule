using System;
using System.Collections.Generic;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Contexts {
	public abstract class Context : MonoBehaviour {
		private readonly Dictionary<Type, IInitializable> contextItems = new();

		protected abstract void ResolveContext();
		protected abstract void OnInitialized();

		protected void InitializeContext() {
			//.NET Standard 2.1 preserves dictionary insertion order which is vital
			foreach ((Type _, IInitializable initializable) in contextItems)
				initializable.Initialize();
		}

		public T Get<T>() where T : class {
			if (contextItems.TryGetValue(typeof(T), out IInitializable contextItem))
				return contextItem as T;

			throw new Exception($"Context item {typeof(T)} cannot be found in current context");
		}

		protected BindingBuilder<T> Resolve<T>() where T : IInitializable, new() {
			T dependency = typeof(MonoBehaviour).IsAssignableFrom(typeof(T))
				? GetComponentInChildren<T>(true) ?? throw new Exception("Dependency not found: " + typeof(T))
				: new T();

			if (!contextItems.TryAdd(typeof(T), dependency))
				Debug.LogWarning($"Dependency {typeof(T)} is already added to context");

			return new BindingBuilder<T>(this, dependency);
		}

		protected class BindingBuilder<T> where T : IInitializable {
			private readonly Context context;
			private readonly T instance;

			internal BindingBuilder(Context context, T instance) {
				this.context = context;
				this.instance = instance;
			}

			public BindingBuilder<T> To<TInterface>() {
				if (!typeof(TInterface).IsAssignableFrom(typeof(T)))
					throw new InvalidOperationException($"Type {typeof(T)} is not assignable to {typeof(TInterface)}");

				if (!context.contextItems.TryAdd(typeof(TInterface), instance))
					Debug.LogWarning($"Interface {typeof(TInterface)} already bound in context");

				return this;
			}
		}
	}
}
