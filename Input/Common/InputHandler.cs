using System;
using UnityEngine;

namespace Frolics.Input.Common {
	// IDEA This might be IPointerInputHandler
	public abstract class InputHandler {
		public Action<PointerData> PressEvent;
		public Action<PointerData> DragEvent;
		public Action<PointerData> ReleaseEvent;

		public abstract void HandleInput();
	}
}
