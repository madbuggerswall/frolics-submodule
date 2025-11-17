using System;
using System.Collections;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	public class FixedDeque<T> : IEnumerable<T> {
		// --- Internal state ---
		private readonly T[] buffer; // backing array (circular buffer)
		private int head;            // index of the first (front) element
		private int tail;            // index AFTER the last (back) element
		private int count;           // number of active elements

		public int Count => count;            // current number of elements
		public int Capacity => buffer.Length; // fixed maximum capacity

		public bool OverwriteWhenFull { get; }

		public FixedDeque(int capacity, bool overwriteWhenFull = true) {
			if (capacity < 1)
				throw new ArgumentException("Capacity must be positive.", nameof(capacity));

			buffer = new T[capacity];
			head = 0;  // initially empty, so head = 0
			tail = 0;  // tail also = 0 (points to next free slot)
			count = 0; // no elements yet
			OverwriteWhenFull = overwriteWhenFull;
		}


		public void PushFront(T item) {
			if (count == Capacity) {
				if (!OverwriteWhenFull)
					throw new InvalidOperationException("Deque is full.");

				// Overwrite mode:
				// Move head backwards, overwrite slot, and adjust tail to follow head.
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
		
		public void PushBack(T item) {
			if (count == Capacity) {
				if (!OverwriteWhenFull)
					throw new InvalidOperationException("Deque is full.");

				// Overwrite mode:
				// Write item at tail, advance tail, and move head forward (drop oldest).
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

		public T PopFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			T item = buffer[head];
			buffer[head] = default!;      // clear slot
			head = (head + 1) % Capacity; // advance head
			count--;
			return item;
		}


		public T PopBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			tail = (tail - 1 + Capacity) % Capacity; // move tail backwards
			T item = buffer[tail];
			buffer[tail] = default!; // clear slot
			count--;
			return item;
		}

		public T PeekFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			return buffer[head];
		}

		public T PeekBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			int lastIndex = (tail - 1 + Capacity) % Capacity;
			return buffer[lastIndex];
		}

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

		public void Clear() {
			Array.Clear(buffer, 0, buffer.Length);
			head = 0;
			tail = 0;
			count = 0;
		}

		public IEnumerator<T> GetEnumerator() {
			for (int i = 0; i < count; i++)
				yield return buffer[(head + i) % Capacity];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
