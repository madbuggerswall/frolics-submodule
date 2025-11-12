using System;
using System.Collections;
using System.Collections.Generic;

namespace Frolics.Collections.Generic {
	/// <summary>
	/// A fixed-size double-ended queue (deque) backed by a circular buffer.
	/// <list>
	/// <item> Capacity is constant (set at construction).</item>
	/// <item> Supports O(1) push/pop operations at both ends.</item>
	/// <item> When full, behavior depends on <c>OverwriteWhenFull</c>.</item>
	/// </list>
	/// </summary>
	public class FixedDeque<T> : IEnumerable<T> {
		// --- Internal state ---
		private readonly T[] buffer; // backing array (circular buffer)
		private int head;            // index of the first (front) element
		private int tail;            // index AFTER the last (back) element
		private int count;           // number of active elements

		// --- Public properties ---
		public int Count => count;            // current number of elements
		public int Capacity => buffer.Length; // fixed maximum capacity

		/// <summary>
		/// If true, pushing when full overwrites the oldest element.
		/// <br/>If false, pushing when full throws an exception.
		/// </summary>
		public bool OverwriteWhenFull { get; }

		///<summary>
		/// <param name="overwriteWhenFull">
		/// When capacity is full:
		/// <list>
		/// <item> true  → overwrite the oldest element automatically.</item>
		/// <item> false → throw InvalidOperationException.</item>
		/// </list>
		/// </param>
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

		/// <summary>
		/// Adds an item to the <b>front</b> of the deque.
		///	<list>
		/// <item> Moves head backwards (circularly).</item>
		/// <item> Writes item at new <c>head</c>.</item>
		/// <item> Increments <c>count</c>.</item>
		///	</list>
		/// </summary>
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

		/// <summary>
		/// Adds an item to the <b>back</b> of the deque.
		/// <list>
		/// <item> Writes item at <c>tail</c>.</item>
		/// <item> Moves <c>tail</c> forward (circularly).</item>
		/// <item> Increments <c>count</c>.</item>
		/// </list>
		/// </summary>
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

		/// <summary>
		/// Removes and returns the item at the <b>front</b>.
		/// <list>
		/// <item> Reads item at <c>head</c>.</item>
		/// <item> Clears slot for GC friendliness.</item>
		/// <item> Moves <c>head</c> forward.</item>
		/// <item> Decrements <c>count</c>.</item>
		/// </list>
		/// </summary>
		public T PopFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			T item = buffer[head];
			buffer[head] = default!;      // clear slot
			head = (head + 1) % Capacity; // advance head
			count--;
			return item;
		}

		/// <summary>
		/// Removes and returns the item at the <b>back</b>.
		/// <list>
		/// <item> Moves <c>tail</c> backwards.</item>
		/// <item> Reads <c>item</c> at new <c>tail</c>.</item>
		/// <item> Clears slot.</item>
		/// <item> Decrements <c>count</c>.</item>
		/// </list>
		/// </summary>
		public T PopBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			tail = (tail - 1 + Capacity) % Capacity; // move tail backwards
			T item = buffer[tail];
			buffer[tail] = default!; // clear slot
			count--;
			return item;
		}

		/// <summary>
		/// Peeks at the <b>front</b> item without removing it.
		/// </summary>
		public T PeekFront() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			return buffer[head];
		}

		/// <summary>
		/// Peeks at the <b>back</b> item without removing it.
		/// </summary>
		public T PeekBack() {
			if (count == 0)
				throw new InvalidOperationException("Deque is empty.");

			int lastIndex = (tail - 1 + Capacity) % Capacity;
			return buffer[lastIndex];
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// <br/>Index 0 corresponds to the <b>front</b> of the deque.
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

		/// <summary>
		/// Clears all elements and resets indices.
		/// </summary>
		public void Clear() {
			Array.Clear(buffer, 0, buffer.Length);
			head = 0;
			tail = 0;
			count = 0;
		}

		/// <summary>
		/// Iterates through elements in logical order (front → back).
		/// </summary>
		public IEnumerator<T> GetEnumerator() {
			for (int i = 0; i < count; i++)
				yield return buffer[(head + i) % Capacity];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
