using Frolics.Input.Mobile;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Input {
	// TODO Do not handle standalone input on mobile platforms
	[DefaultExecutionOrder(-32)]
	public class InputManager : MonoBehaviour, IInitializable, IInputManager {
		public ITouchInputHandler TouchInputHandler { get; private set; }
		public IKeyboardInputHandler KeyboardInputHandler { get; private set; }
		public IMouseInputHandler MouseInputHandler { get; private set; }

		void IInitializable.Initialize() {
			TouchInputHandler = new TouchInputHandler();
			KeyboardInputHandler = new KeyboardInputHandler();
			MouseInputHandler = new MouseInputHandler();
		}

		private void Update() {
			TouchInputHandler.HandleInput();
			KeyboardInputHandler.HandleInput();
			MouseInputHandler.HandleInput();
		}
	}
}