using System;

namespace Frolics.Contexts.Exceptions {
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