using System;
using System.Collections.Generic;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Contexts {
	public abstract class DependencyContext : MonoBehaviour {
		private readonly Dictionary<Type, IInitializable> contextItems = new();

		protected abstract void BindContext();
		protected abstract void OnInitialized();

		protected void InitializeContext() {
			//.NET Standard 2.1 preserves dictionary insertion order which is vital
			foreach ((Type _, IInitializable initializable) in contextItems)
				initializable.Initialize();
		}

		public T Resolve<T>() where T : class {
			if (contextItems.TryGetValue(typeof(T), out IInitializable contextItem))
				return contextItem as T;

			throw new DependencyResolutionException(typeof(T), this);
		}

		protected BindingBuilder<T> Bind<T>() where T : IInitializable, new() {
			T dependency = typeof(MonoBehaviour).IsAssignableFrom(typeof(T)) ? FindMono<T>() : new T();

			RegisterDependency<T, T>(dependency);
			return new BindingBuilder<T>(this, dependency);
		}

		private T FindMono<T>() where T : IInitializable {
			T monoBehaviour = GetComponentInChildren<T>(true);
			if (monoBehaviour != null)
				return monoBehaviour;

			string message = $"MonoBehaviour ({typeof(T)}) cannot be found in the children of {this.GetType()}.";
			throw new DependencyResolutionException(message);
		}

		private void RegisterDependency<TAbstract, TConcrete>(TConcrete dependency) where TConcrete : IInitializable {
			if (contextItems.TryAdd(typeof(TAbstract), dependency))
				Context.GetInstance().RegisterContext<TAbstract>(this);
			else
				Debug.LogWarning($"Dependency ({typeof(TAbstract)}) is already registered to context ({GetType()})!");
		}

		protected class BindingBuilder<TConcrete> where TConcrete : IInitializable {
			private readonly DependencyContext dependencyContext;
			private readonly TConcrete instance;

			internal BindingBuilder(DependencyContext dependencyContext, TConcrete instance) {
				this.dependencyContext = dependencyContext;
				this.instance = instance;
			}

			public BindingBuilder<TConcrete> To<TAbstract>() {
				if (!typeof(TAbstract).IsAssignableFrom(typeof(TConcrete)))
					throw new DependencyBindingException(typeof(TAbstract), typeof(TConcrete));

				dependencyContext.RegisterDependency<TAbstract, TConcrete>(instance);
				return this;
			}
		}
	}

	public class DependencyBindingException : Exception {
		public DependencyBindingException(Type abstractType, Type concreteType) : base(
			$"Type {concreteType} is not assignable to {abstractType}"
		) { }
	}

	public class DependencyResolutionException : Exception {
		public DependencyResolutionException(string message) : base(message) { }

		public DependencyResolutionException(Type dependencyType, DependencyContext context) : base(
			$"Dependency ({dependencyType}) cannot be found in context ({context.GetType()})."
		) { }

		public DependencyResolutionException(Type dependencyType) : base(
			$"Dependency ({dependencyType}) cannot be found in in any context."
		) { }
	}
}
