using Frolics.Input.Mobile;
using Frolics.Input.Standalone;

namespace Frolics.Input {
	public interface IInputManager {
		public ITouchInputHandler TouchInputHandler { get; }
		public IKeyboardInputHandler KeyboardInputHandler { get; }
		public IMouseInputHandler MouseInputHandler { get; }
	}
}
