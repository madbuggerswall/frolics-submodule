using System;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	public class UniqueList<T> {
		private readonly List<T> items;            // Contiguous storage
		private readonly Dictionary<T, int> index; // Maps item -> index in List

		public int Count => items.Count;
		public T this[int i] => items[i];

		public UniqueList(int capacity = 4) {
			items = new List<T>(capacity);
			index = new Dictionary<T, int>(capacity);
		}

		/// <summary> Adds a unique item. Returns true if added, false if already present. </summary>
		public bool Add(T value) {
			if (index.ContainsKey(value))
				return false;

			index[value] = items.Count;
			items.Add(value);
			return true;
		}

		/// <summary> Removes the item in O(1) time without preserving order. </summary>
		public bool Remove(T value) {
			if (!index.TryGetValue(value, out int removeIndex))
				return false;

			int lastIndex = items.Count - 1;
			T lastItem = items[lastIndex];

			// Swap with last if not already last
			if (removeIndex != lastIndex) {
				items[removeIndex] = lastItem;
				index[lastItem] = removeIndex;
			}

			// Remove last
			items.RemoveAt(lastIndex);
			index.Remove(value);

			return true;
		}

		/// <summary> Checks if the item exists. </summary>
		public bool Contains(T value) => index.ContainsKey(value);

		/// <summary> Returns the index of an item, or -1 if not found. </summary>
		public int IndexOf(T value) => index.GetValueOrDefault(value, -1);

		/// <summary> Returns the underlying list (read-only). </summary>
		public IReadOnlyList<T> AsReadOnly() => items;

		/// <summary> Clears all items. </summary>
		public void Clear() {
			items.Clear();
			index.Clear();
		}


		//  Set Operations
		/// <summary>
		/// Modifies this list so it contains only elements that are also in <paramref name="other"/>.
		/// </summary>
		public void IntersectWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			// Build a temporary HashSet for O(1) membership checks
			HashSet<T> keep = new HashSet<T>(other);

			// Iterate backwards so removal doesn’t mess up indices
			for (int i = items.Count - 1; i >= 0; i--)
				if (!keep.Contains(items[i]))
					Remove(items[i]);
		}

		/// <summary>
		/// Modifies this list so it contains all elements that are in itself or in <paramref name="other"/>.
		/// </summary>
		public void UnionWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other)
				Add(item);
		}

		/// <summary>
		/// Removes all elements in <paramref name="other"/> from this list.
		/// </summary>
		public void ExceptWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other)
				Remove(item);
		}

		/// <summary>
		/// Modifies this list so it contains only elements present in exactly one of the two sets.
		/// </summary>
		public void SymmetricExceptWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other)
				if (!Remove(item))
					Add(item);
		}
	}
}
