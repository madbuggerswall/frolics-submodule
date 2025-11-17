using System;

namespace Frolics.Contexts.Exceptions {
	public class DependencyBindingException : Exception {
		public DependencyBindingException(Type abstractType, Type concreteType) : base(
			$"Type {concreteType} is not assignable to {abstractType}"
		) { }
	}
}