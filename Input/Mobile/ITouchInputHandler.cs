using System;

namespace Frolics.Input.Mobile {
	public interface ITouchInputHandler : IInputHandler {
		public event Action<TouchData> TouchPressEvent;
		public event Action<TouchData> TouchDragEvent;
		public event Action<TouchData> TouchReleaseEvent;
	}
}