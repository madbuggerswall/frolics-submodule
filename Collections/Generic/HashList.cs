using System;
using System.Collections;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	// TODO IHashList interface
	public class HashList<T> : IEnumerable<T> {
		private readonly HashSet<T> hashSet;
		private readonly List<T> list;

		public int Count => list.Count;
		public T this[int index] => list[index];

		public HashList(int capacity = 4) {
			hashSet = new HashSet<T>(capacity);
			list =  new List<T>(capacity);
		}

		public bool TryAdd(T item) {
			if (!hashSet.Add(item))
				return false;

			list.Add(item);
			return true;
		}

		public bool TryRemove(T item) {
			if (!hashSet.Remove(item))
				return false;

			// Remove from list (O(n))
			list.Remove(item);
			return true;
		}

		public void Clear() {
			hashSet.Clear();
			list.Clear();
		}

		// --- Set Operations ---
		public void IntersectWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			hashSet.IntersectWith(other);
			list.RemoveAll(item => !hashSet.Contains(item));
		}

		public void UnionWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other)
				TryAdd(item);
		}

		public void ExceptWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other)
				TryRemove(item);
		}

		public void SymmetricExceptWith(IEnumerable<T> other) {
			if (other is null)
				throw new ArgumentNullException(nameof(other));

			foreach (T item in other) {
				if (!TryRemove(item))
					TryAdd(item);
			}
		}

		public bool Contains(T item) => hashSet.Contains(item);
		public int IndexOf(T item) => list.IndexOf(item);

		public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
