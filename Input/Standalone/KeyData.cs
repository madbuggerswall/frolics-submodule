using UnityEngine.InputSystem.Controls;

namespace Frolics.Input.Standalone {
	public struct KeyData {
		public KeyControl KeyControl { get; }

		public KeyData(KeyControl keyControl) {
			this.KeyControl = keyControl;
		}
	}
}
