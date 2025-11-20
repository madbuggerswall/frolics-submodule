using System;
using System.Collections;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	/// <summary>
	/// A double-ended queue (Deque) implementation using a circular buffer.
	/// Provides O(1) amortized operations for adding/removing at both ends.
	/// </summary>
	public class Deque<T> : IDeque<T>, IEnumerable<T> {
		private T[] buffer;
		private int head; // Index of the first element
		private int tail; // Index after the last element (next free slot)
		private int count;

		public int Count => count;
		public int Capacity => buffer.Length;

		public Deque(int capacity = 16) {
			if (capacity < 1)
				throw new ArgumentException("Capacity must be positive.");

			buffer = new T[capacity];
			head = 0;
			tail = 0;
			count = 0;
		}

		/// <summary>Adds an item to the front of the deque.</summary>
		public void PushFront(T item) {
			EnsureCapacity(count + 1);
			head = (head - 1 + Capacity) % Capacity;
			buffer[head] = item;
			count++;
		}

		/// <summary>Adds an item to the back of the deque.</summary>
		public void PushBack(T item) {
			EnsureCapacity(count + 1);
			buffer[tail] = item;
			tail = (tail + 1) % Capacity;
			count++;
		}

		/// <summary>Removes and returns the item at the front.</summary>
		public T PopFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			T item = buffer[head];
			head = (head + 1) % Capacity;
			count--;
			return item;
		}

		/// <summary>Removes and returns the item at the back.</summary>
		public T PopBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			tail = (tail - 1 + Capacity) % Capacity;
			T item = buffer[tail];
			count--;
			return item;
		}

		/// <summary>Peeks at the front item without removing it.</summary>
		public T PeekFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			return buffer[head];
		}

		/// <summary>Peeks at the back item without removing it.</summary>
		public T PeekBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			return buffer[(tail - 1 + Capacity) % Capacity];
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// Index 0 corresponds to the front of the deque.
		/// </summary>
		public T this[int index] {
			get {
				if (index < 0 || index >= count)
					throw new ArgumentOutOfRangeException(nameof(index));

				return buffer[(head + index) % Capacity];
			}
			set {
				if (index < 0 || index >= count)
					throw new ArgumentOutOfRangeException(nameof(index));

				buffer[(head + index) % Capacity] = value;
			}
		}


		private void EnsureCapacity(int size) {
			if (size <= Capacity)
				return;

			int newCapacity = Capacity * 2;
			T[] newBuffer = new T[newCapacity];

			// Copy elements in order
			for (int i = 0; i < count; i++)
				newBuffer[i] = buffer[(head + i) % Capacity];

			buffer = newBuffer;
			head = 0;
			tail = count;
		}

		// IEnumerable<T>
		public IEnumerator<T> GetEnumerator() {
			for (int i = 0; i < count; i++)
				yield return buffer[(head + i) % Capacity];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
