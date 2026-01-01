using System;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Frolics.Input.Mobile {
	public class TouchInputHandler : ITouchInputHandler {
		private const int MaxTouches = 1;

		public event Action<TouchData> TouchPressEvent;
		public event Action<TouchData> TouchDragEvent;
		public event Action<TouchData> TouchReleaseEvent;

		public TouchInputHandler() {
			UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
		}

		void IInputHandler.HandleInput() {
			ReadAllTouchInputs();
		}

		// TODO Rename
		private void ReadAllTouchInputs() {
			int touchCount = Mathf.Min(Touch.activeTouches.Count, MaxTouches);
			for (int touchIndex = 0; touchIndex < touchCount; touchIndex++)
				ReadTouchInput(Touch.activeTouches[touchIndex]);
		}

		private void ReadTouchInput(in Touch touch) {
			if (touch.began)
				TouchPressEvent?.Invoke(new TouchData(touch, touch.screenPosition));
			else if (touch.inProgress)
				TouchDragEvent?.Invoke(new TouchData(touch, touch.screenPosition));
			else if (touch.ended)
				TouchReleaseEvent?.Invoke(new TouchData(touch, touch.screenPosition));
		}
	}
}
