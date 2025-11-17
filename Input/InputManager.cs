using Frolics.Input.Common;
using Frolics.Input.Mobile;
using Frolics.Input.Standalone;
using Frolics.Utilities;
using UnityEngine;

namespace Frolics.Input {
	[DefaultExecutionOrder(-32)]
	public class InputManager : MonoBehaviour, IInitializable {
		public MobileInputHandler MobileInputHandler { get; private set; }
		public StandaloneInputHandler StandaloneInputHandler { get; private set; }
		public InputHandler CommonInputHandler { get; private set; }

		public void Initialize() {
			MobileInputHandler = new MobileInputHandler();
			StandaloneInputHandler = new StandaloneInputHandler();
			CommonInputHandler = GetPlatformDependentHandler();
		}

		private void Update() {
			MobileInputHandler.HandleInput();
			StandaloneInputHandler.HandleInput();
		}

		private InputHandler GetPlatformDependentHandler() {
			bool isMobile = Application.platform is RuntimePlatform.Android or RuntimePlatform.IPhonePlayer;
			return isMobile ? MobileInputHandler : StandaloneInputHandler;
		}
	}
}
