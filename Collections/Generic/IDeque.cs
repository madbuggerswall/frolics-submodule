namespace Frolics.Collections.Generic {
	public interface IDeque<T> {
		public int Count { get; }
		public int Capacity { get; }
		public T this[int index] { get; set; }

		public T PeekFront();
		public T PeekBack();
		public void PushFront(T item);
		public void PushBack(T item);
		public T PopFront();
		public T PopBack();
	}
}
