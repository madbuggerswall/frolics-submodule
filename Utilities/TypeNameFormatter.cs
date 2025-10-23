using System;
using System.Text;
using JetBrains.Annotations;

namespace Frolics.Utilities {
	public static class TypeNameFormatter {
		[PublicAPI]
		public static string Nicify(Type type) {
			if (type == null)
				return "null";

			if (type.IsGenericType)
				return NicifyGenericType(type);

			if (type.IsArray)
				return Nicify(type.GetElementType()) + "[]";

			return type.Name;
		}

		private static string NicifyGenericType(Type type) {
			StringBuilder stringBuilder = new();

			// Append type name
			string name = type.Name;
			int backtickIndex = name.IndexOf('`');
			if (backtickIndex > 0)
				name = name.Substring(0, backtickIndex);

			stringBuilder.Append(name);
			stringBuilder.Append("<");

			// Append generics name
			Type[] args = type.GetGenericArguments();
			for (int i = 0; i < args.Length; i++) {
				if (i > 0)
					stringBuilder.Append(", ");

				stringBuilder.Append(Nicify(args[i]));
			}

			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}
	}
}
