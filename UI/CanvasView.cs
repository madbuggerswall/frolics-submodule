using System;
using Frolics.UI.Utilities;
using Frolics.Utilities.Extensions;
using UnityEngine;

namespace Frolics.UI {
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class CanvasView : MonoBehaviour {
		private enum RenderMode { ScreenSpaceOverlay, ScreenSpaceCamera, WorldSpace }

		private enum UIScaleMode { ConstantPixelSize, ScaleWithScreenSize }

		// TODO Separate screenSpace Overlay and Camera modes
		// TODO To support multiple canvases in different mode.
		[SerializeField, HideInInspector] private Camera mainCamera;
		[SerializeField, HideInInspector] private ScreenSpaceCamera screenSpaceCamera;
		[SerializeField, HideInInspector] private WorldSpaceCamera worldSpaceCamera;
		[SerializeField, HideInInspector] private RectTransform rectTransform;

		[Header("Settings")]
		[SerializeField, Min(0.01f)] private float canvasScale = 1;
		[SerializeField] private RenderMode renderMode = RenderMode.ScreenSpaceOverlay;
		[SerializeField] private UIScaleMode scaleMode = UIScaleMode.ConstantPixelSize;

		[Header("Screen Space Camera Settings")]
		[SerializeField] private float planeDistance = 2;

		[Header("Scale With Screen Size Settings")]
		[SerializeField] private Vector2Int referenceSize = new(1080, 2400);
		[SerializeField, Range(0f, 1f)] private float contribution = 0f;

		private RenderMode currentRenderMode = RenderMode.ScreenSpaceOverlay;
		private float sizeScale = 1f;
		private Vector2Int currentScreenSize;


		private void Reset() {
			Initialize();
		}

		private void Initialize() {
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();

			if (rectTransform == null)
				rectTransform = gameObject.AddComponent<RectTransform>();

			if (mainCamera == null)
				mainCamera = Camera.main;

			if (screenSpaceCamera == null)
				screenSpaceCamera = FindAnyObjectByType<ScreenSpaceCamera>();

			if (worldSpaceCamera == null)
				worldSpaceCamera = FindAnyObjectByType<WorldSpaceCamera>();

			if (screenSpaceCamera == null) {
				GameObject screenSpaceCameraGO = new("ScreenSpaceCamera");
				screenSpaceCameraGO.transform.SetParent(transform.parent);
				screenSpaceCamera = screenSpaceCameraGO.AddComponent<ScreenSpaceCamera>();
			}

			if (worldSpaceCamera == null) {
				GameObject worldSpaceCameraGO = new("WorldSpaceCamera");
				worldSpaceCameraGO.transform.SetParent(transform.parent);
				worldSpaceCamera = worldSpaceCameraGO.AddComponent<WorldSpaceCamera>();
			}

			SwitchToScreenSpaceOverlay();
		}

		private void Update() {
			sizeScale = (float) Screen.width / referenceSize.x * (1 - contribution)
			          + (float) Screen.height / referenceSize.y * contribution;

			ListenScreenSizeChange();
			CanvasScaleStuff();
		}

		private void ListenScreenSizeChange() {
			if (currentScreenSize.x == Screen.width && currentScreenSize.y == Screen.height)
				return;

			currentScreenSize = new Vector2Int(Screen.width, Screen.height);
			OnScreenSizeChanged();
		}

		private void OnScreenSizeChanged() {
			switch (renderMode) {
				case RenderMode.ScreenSpaceOverlay:
					SwitchToScreenSpaceOverlay();
					break;
				case RenderMode.ScreenSpaceCamera:
					SwitchToScreenSpaceCamera();
					break;
				case RenderMode.WorldSpace:
					SwitchToWorldSpace();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void CanvasScaleStuff() {
			switch (renderMode) {
				case RenderMode.ScreenSpaceOverlay:
					HandleScreenSpaceOverlay();
					break;
				case RenderMode.ScreenSpaceCamera:
					HandleScreenSpaceCamera();
					break;
				case RenderMode.WorldSpace:
					HandleWorldSpace();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private float GetScale() {
			return scaleMode switch {
				UIScaleMode.ConstantPixelSize => canvasScale,
				UIScaleMode.ScaleWithScreenSize => sizeScale * canvasScale,
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		private void HandleScreenSpaceOverlay() {
			if (currentRenderMode is not RenderMode.ScreenSpaceOverlay)
				SwitchToScreenSpaceOverlay();

			// Is using camera.pixelWidth and camera.PixelHeight more appropriate
			float width = Screen.width / GetScale();
			float height = Screen.height / GetScale();

			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
			rectTransform.localScale = Vector3.one * GetScale();
		}

		private void HandleScreenSpaceCamera() {
			if (currentRenderMode is not RenderMode.ScreenSpaceCamera)
				SwitchToScreenSpaceCamera();

			Transform cameraTransform = mainCamera.transform;
			screenSpaceCamera.transform.SetPositionAndRotation(cameraTransform.position, cameraTransform.rotation);

			RectTransformDTO rectTransformDTO = mainCamera.GetPlaneRect(planeDistance);
			rectTransform.SetPositionAndRotation(rectTransformDTO.Position, rectTransformDTO.Rotation);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / GetScale());
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / GetScale());

			float scale = rectTransformDTO.Size.x / Screen.width;
			rectTransform.localScale = Vector3.one * (scale * GetScale());
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
		}

		private void HandleWorldSpace() {
			if (currentRenderMode is not RenderMode.WorldSpace)
				SwitchToWorldSpace();

			rectTransform.localScale = Vector3.one * GetScale();
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
		}


		private void SwitchToScreenSpaceOverlay() {
			Vector3 canvasPosition = new(Screen.width / 2f, Screen.height / 2f, 0);
			screenSpaceCamera.ConvertToOverlay(canvasPosition);

			rectTransform.SetPositionAndRotation(canvasPosition, Quaternion.identity);
			rectTransform.hideFlags = HideFlags.NotEditable;

			currentRenderMode = RenderMode.ScreenSpaceOverlay;
		}

		private void SwitchToScreenSpaceCamera() {
			screenSpaceCamera.ConvertToCamera(mainCamera);
			rectTransform.hideFlags = HideFlags.NotEditable;
			currentRenderMode = RenderMode.ScreenSpaceCamera;
		}

		private void SwitchToWorldSpace() {
			rectTransform.hideFlags = HideFlags.None;
			currentRenderMode = RenderMode.WorldSpace;
		}
	}
}
