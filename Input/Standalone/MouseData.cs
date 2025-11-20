using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Frolics.Input.Standalone {
	public struct MouseData {
		public ButtonControl ButtonControl { get; private set; }
		public Vector2 MousePosition { get; private set; }

		public MouseData(ButtonControl buttonControl, Vector2 mousePosition) {
			this.ButtonControl = buttonControl;
			this.MousePosition = mousePosition;
		}
	}
}
