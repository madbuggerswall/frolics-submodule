using System;

namespace Frolics.Input.Standalone {
	public interface IKeyboardInputHandler : IInputHandler {
		public event Action<KeyData> KeyPressEvent;
		public event Action<KeyData> KeyPressHeldEvent;
		public event Action<KeyData> KeyReleaseEvent;
	}
}