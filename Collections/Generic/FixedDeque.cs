using System;
using System.Collections;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	/// <summary>
	/// A double-ended queue (Deque) implementation using a circular buffer, with limited capacity.
	/// </summary>
	public class FixedDeque<T> : IDeque<T>, IEnumerable<T> {
		private readonly T[] buffer;
		private int head;
		private int tail;
		private int count;

		public int Count => count;
		public int Capacity => buffer.Length;

		/// <summary>
		/// Determines whether new items overwrite the oldest ones when the deque is full.
		/// </summary>
		public bool OverwriteWhenFull { get; }

		/// <summary>
		/// Initializes a new instance with the specified capacity and overwrite behavior.
		/// </summary>
		public FixedDeque(int capacity, bool overwriteWhenFull = true) {
			if (capacity < 1)
				throw new ArgumentException("Capacity must be positive.", nameof(capacity));

			buffer = new T[capacity];
			head = 0;  // initially empty, so head = 0
			tail = 0;  // tail also = 0 (points to next free slot)
			count = 0; // no elements yet
			OverwriteWhenFull = overwriteWhenFull;
		}

		/// <summary>Adds an item to the front of the deque.</summary>
		public void PushFront(T item) {
			if (count == Capacity) {
				if (!OverwriteWhenFull)
					throw new InvalidOperationException("Deque is full.");

				// Overwrite mode: Move head backwards, overwrite slot, and adjust tail to follow head.
				head = (head - 1 + Capacity) % Capacity;
				buffer[head] = item;
				tail = head; // tail must follow head in overwrite mode
				return;
			}

			// Normal mode:
			head = (head - 1 + Capacity) % Capacity; // move head backwards circularly
			buffer[head] = item;                     // place item at new head
			count++;
		}

		/// <summary>Adds an item to the back of the deque.</summary>
		public void PushBack(T item) {
			if (count == Capacity) {
				if (!OverwriteWhenFull)
					throw new InvalidOperationException("Deque is full.");

				// Overwrite mode: Write item at tail, advance tail, and move head forward (drop oldest).
				buffer[tail] = item;
				tail = (tail + 1) % Capacity;
				head = tail; // oldest element is dropped
				return;
			}

			// Normal mode:
			buffer[tail] = item;          // place item at tail
			tail = (tail + 1) % Capacity; // advance tail circularly
			count++;
		}

		/// <summary>Removes and returns the item at the front.</summary>
		public T PopFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			T item = buffer[head];
			buffer[head] = default!;      // clear slot
			head = (head + 1) % Capacity; // advance head
			count--;
			return item;
		}

		/// <summary>Removes and returns the item at the back.</summary>
		public T PopBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			tail = (tail - 1 + Capacity) % Capacity; // move tail backwards
			T item = buffer[tail];
			buffer[tail] = default!; // clear slot
			count--;
			return item;
		}

		/// <summary>Peeks at the front item without removing it.</summary>
		public T PeekFront() {
			return count == 0 ? throw new InvalidOperationException("Deque is empty.") : buffer[head];
		}

		/// <summary>Peeks at the back item without removing it.</summary>
		public T PeekBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			int lastIndex = (tail - 1 + Capacity) % Capacity;
			return buffer[lastIndex];
		}

		/// <summary>
		/// Gets or sets the element at the specified index. Index 0 corresponds to the front of the deque.
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

		/// <summary>Clears all elements from the deque.</summary>
		public void Clear() {
			Array.Clear(buffer, 0, buffer.Length);
			head = 0;
			tail = 0;
			count = 0;
		}

		/// <summary>Returns an enumerator that iterates through the deque.</summary>
		public IEnumerator<T> GetEnumerator() {
			for (int i = 0; i < count; i++)
				yield return buffer[(head + i) % Capacity];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
