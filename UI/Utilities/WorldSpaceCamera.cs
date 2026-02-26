using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Frolics.UI.Utilities {
	[ExecuteAlways]
	[RequireComponent(typeof(Camera))]
	public class WorldSpaceCamera : MonoBehaviour {
		[SerializeField, HideInInspector] private new Camera camera;
		[SerializeField, HideInInspector] private Camera mainCamera;

		private void Reset() {
			Initialize();
		}

		private void Update() {
			MimicCamera(mainCamera);
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

			mainCamera = Camera.main;
			if (mainCamera == null)
				return;

			List<Camera> cameraStack = mainCamera.GetUniversalAdditionalCameraData().cameraStack;
			if (!cameraStack.Contains(camera))
				cameraStack.Add(camera);
		}

		private void MimicCamera(Camera camera) {
			this.camera.orthographic = camera.orthographic;
			this.camera.orthographicSize = camera.orthographicSize;
			this.camera.fieldOfView = camera.fieldOfView;
			this.camera.nearClipPlane = camera.nearClipPlane;
			this.camera.farClipPlane = camera.farClipPlane;
			
			Transform cameraTransform = camera.transform;
			this.camera.transform.SetPositionAndRotation(cameraTransform.position, cameraTransform.rotation);
		}
	}
}
