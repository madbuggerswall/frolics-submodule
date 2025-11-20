using System;
using System.Diagnostics;
using Frolics.Input.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using Debug = UnityEngine.Debug;

namespace Frolics.Input.Standalone {
	public class StandaloneInputHandler : InputHandler {
		public event Action<MouseData> MousePressEvent;
		public event Action<MouseData> MouseDragEvent;
		public event Action<MouseData> MouseReleaseEvent;

		public event Action<KeyData> KeyPressEvent;
		public event Action<KeyData> KeyPressHeldEvent;
		public event Action<KeyData> KeyReleaseEvent;

		private ReadOnlyArray<KeyControl> allKeys = Keyboard.current.allKeys;

		public override void HandleInput() {
			ReadMouseButtonInput(Mouse.current.leftButton);
			ReadMouseButtonInput(Mouse.current.rightButton);
			ReadKeyboardButtonInput();
		}

		private void ReadMouseButtonInput(ButtonControl buttonControl) {
			bool pressStarted = buttonControl.wasPressedThisFrame;
			bool isPressHeld = buttonControl.isPressed;
			bool pressReleased = buttonControl.wasReleasedThisFrame;
			Vector2 currentPosition = Mouse.current.position.ReadValue();

			if (pressStarted)
				MousePressEvent?.Invoke(new MouseData(buttonControl, currentPosition));
			else if (isPressHeld)
				MouseDragEvent?.Invoke(new MouseData(buttonControl, currentPosition));
			else if (pressReleased)
				MouseReleaseEvent?.Invoke(new MouseData(buttonControl, currentPosition));

			// Common events
			if (pressStarted && buttonControl == Mouse.current.leftButton)
				PressEvent?.Invoke(new PointerData(currentPosition));
			else if (isPressHeld && buttonControl == Mouse.current.leftButton)
				DragEvent?.Invoke(new PointerData(currentPosition));
			else if (pressReleased && buttonControl == Mouse.current.leftButton)
				ReleaseEvent?.Invoke(new PointerData(currentPosition));
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
