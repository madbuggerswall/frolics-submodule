using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace Frolics.UI.Utilities {
	[ExecuteAlways]
	[RequireComponent(typeof(Camera), typeof(Physics2DRaycaster))]
	public class ScreenSpaceCamera : MonoBehaviour {
		[SerializeField, HideInInspector] private new Camera camera;
		[SerializeField, HideInInspector] private Physics2DRaycaster raycaster;

		private void Reset() {
			Initialize();
		}

		private void Initialize() {
			if (camera == null)
				camera = GetComponent<Camera>();

			UniversalAdditionalCameraData cameraData = camera.GetUniversalAdditionalCameraData();
			cameraData.renderType = CameraRenderType.Overlay;
			cameraData.renderPostProcessing = false;
			cameraData.renderShadows = false;
			cameraData.requiresDepthOption = CameraOverrideOption.Off;

			int layerMask = LayerMask.GetMask("UI");
			camera.cullingMask = layerMask;
			camera.useOcclusionCulling = false;

			if (raycaster == null)
				raycaster = GetComponent<Physics2DRaycaster>();

			raycaster.eventMask = layerMask;

			Camera mainCamera = Camera.main;
			if (mainCamera == null)
				return;

			List<Camera> cameraStack = mainCamera.GetUniversalAdditionalCameraData().cameraStack;
			if (!cameraStack.Contains(camera))
				cameraStack.Add(camera);
		}

		public void ConvertToOverlay(Vector3 canvasPosition) {
			camera.orthographic = true;
			camera.orthographicSize = (float) Screen.height / 2;
			camera.nearClipPlane = .1f;
			camera.farClipPlane = 2f;
			camera.transform.SetPositionAndRotation(canvasPosition - Vector3.forward, Quaternion.identity);
		}

		public void ConvertToCamera(Camera camera) {
			camera.orthographic = camera.orthographic;
			camera.orthographicSize = camera.orthographicSize;
			camera.fieldOfView = camera.fieldOfView;
			camera.nearClipPlane = camera.nearClipPlane;
			camera.farClipPlane = camera.farClipPlane;
		}
	}
}
