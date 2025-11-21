using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace Frolics.Input.Standalone {
	// IDEA IKeyboardInputHandler & IMouseInputHandler

	public class KeyboardInputHandler : IKeyboardInputHandler {
		public event Action<KeyData> KeyPressEvent;
		public event Action<KeyData> KeyPressHeldEvent;
		public event Action<KeyData> KeyReleaseEvent;

		private ReadOnlyArray<KeyControl> allKeys = Keyboard.current.allKeys;

		public void HandleInput() {
			ReadKeyboardButtonInput();
		}

		private void ReadKeyboardButtonInput() {
			for (int keyIndex = 0; keyIndex < allKeys.Count; keyIndex++)
				if (allKeys[keyIndex] is not null && !allKeys[keyIndex].synthetic)
					ReadKeyboardButtonInput(allKeys[keyIndex]);
		}

		private void ReadKeyboardButtonInput(KeyControl keyControl) {
			bool pressStarted = keyControl.wasPressedThisFrame;
			bool pressHeld = keyControl.isPressed;
			bool pressReleased = keyControl.wasReleasedThisFrame;

			if (pressStarted)
				KeyPressEvent?.Invoke(new KeyData(keyControl));
			else if (pressHeld)
				KeyPressHeldEvent?.Invoke(new KeyData(keyControl));
			else if (pressReleased)
				KeyReleaseEvent?.Invoke(new KeyData(keyControl));
		}
	}
}
