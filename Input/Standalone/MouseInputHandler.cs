using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Frolics.Input.Standalone {
	public class MouseInputHandler : IMouseInputHandler {
		public event Action<MouseData> MousePressEvent;
		public event Action<MouseData> MouseDragEvent;
		public event Action<MouseData> MouseReleaseEvent;

		public void HandleInput() {
			ReadMouseButtonInput(Mouse.current.leftButton);
			ReadMouseButtonInput(Mouse.current.rightButton);
		}

		private void ReadMouseButtonInput(ButtonControl buttonControl) {
			bool pressStarted = buttonControl.wasPressedThisFrame;
			bool pressHeld = buttonControl.isPressed;
			bool pressReleased = buttonControl.wasReleasedThisFrame;
			Vector2 currentPosition = Mouse.current.position.ReadValue();

			if (pressStarted)
				MousePressEvent?.Invoke(new MouseData(buttonControl, currentPosition));
			else if (pressHeld)
				MouseDragEvent?.Invoke(new MouseData(buttonControl, currentPosition));
			else if (pressReleased)
				MouseReleaseEvent?.Invoke(new MouseData(buttonControl, currentPosition));
		}
	}
}