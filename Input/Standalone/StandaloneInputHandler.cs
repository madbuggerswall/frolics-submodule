using System;
using Frolics.Input.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Frolics.Input.Standalone {
	public class StandaloneInputHandler : InputHandler {
		public Vector2 MousePressPosition { get; private set; }
		public Vector2 MouseDragPosition { get; private set; }
		public Vector2 MouseReleasePosition { get; private set; }

		public Action<MouseData> MousePressEvent { get; }
		public Action<MouseData> MouseDragEvent { get; }
		public Action<MouseData> MouseReleaseEvent { get; }

		public Action<KeyData> KeyPressEvent { get; }
		public Action<KeyData> KeyReleaseEvent { get; }

		public StandaloneInputHandler() : base() {
			MousePressEvent = delegate { };
			MouseDragEvent = delegate { };
			MouseReleaseEvent = delegate { };

			KeyPressEvent = delegate { };
			KeyReleaseEvent = delegate { };
		}

		public override void HandleInput() {
			ReadMouseButtonInput(Mouse.current.leftButton);
			ReadMouseButtonInput(Mouse.current.rightButton);
			ReadKeyboardButtonInput(Keyboard.current.spaceKey);
		}

		private void ReadMouseButtonInput(ButtonControl buttonControl) {
			bool pressStarted = buttonControl.wasPressedThisFrame;
			bool isPressHeld = buttonControl.isPressed;
			bool pressReleased = buttonControl.wasReleasedThisFrame;

			// Common
			Vector2 currentPosition = Mouse.current.position.ReadValue();
			PointerPosition = currentPosition;

			// Mouse Events
			if (pressStarted) {
				MousePressPosition = MouseDragPosition = MouseReleasePosition = currentPosition;
				MousePressEvent.Invoke(new MouseData(buttonControl));
			} else if (isPressHeld) {
				MouseDragPosition = currentPosition;
				MouseDragEvent.Invoke(new MouseData(buttonControl));
			} else if (pressReleased) {
				MouseReleasePosition = currentPosition;
				MouseReleaseEvent.Invoke(new MouseData(buttonControl));
			}

			// Common events
			if (pressStarted && buttonControl == Mouse.current.leftButton)
				PressEvent.Invoke(new PointerData(PointerPosition));
			else if (isPressHeld && buttonControl == Mouse.current.leftButton)
				DragEvent.Invoke(new PointerData(PointerPosition));
			else if (pressReleased && buttonControl == Mouse.current.leftButton)
				ReleaseEvent.Invoke(new PointerData(PointerPosition));
		}

		private void ReadKeyboardButtonInput(KeyControl keyControl) {
			bool pressStarted = keyControl.wasPressedThisFrame;
			bool pressReleased = keyControl.wasReleasedThisFrame;

			if (pressStarted) {
				KeyPressEvent.Invoke(new KeyData(keyControl));
			} else if (pressReleased) {
				KeyReleaseEvent.Invoke(new KeyData(keyControl));
			}
		}
	}
}
