using System;
using Frolics.Input.Mobile;

namespace Frolics.Input.Standalone {
	public interface IMouseInputHandler : IInputHandler {
		public event Action<MouseData> MousePressEvent;
		public event Action<MouseData> MouseDragEvent;
		public event Action<MouseData> MouseReleaseEvent;
	}
}