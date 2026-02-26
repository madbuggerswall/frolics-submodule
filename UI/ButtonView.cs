using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frolics.UI {
	public class ButtonView : MonoBehaviour, IPointerClickHandler {
		[SerializeField, HideInInspector] private new BoxCollider2D collider;
		[SerializeField, HideInInspector] private RectTransform rectTransform;

		private event Action Callback;

		private void Reset() => Initialize();
		
		private void Initialize() {
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();

			if (collider == null)
				collider = GetComponent<BoxCollider2D>();

			if (rectTransform == null)
				rectTransform = gameObject.AddComponent<RectTransform>();

			if (collider == null)
				collider = gameObject.AddComponent<BoxCollider2D>();

			collider.hideFlags = HideFlags.NotEditable;
		}

		public void OnPointerClick(PointerEventData eventData) => Callback?.Invoke();
		public void AddListener(Action callback) => Callback += callback;
		public void ClearListeners() => Callback = null;

		public RectTransform GetRectTransform() => rectTransform;
		public BoxCollider2D GetCollider() => collider;
	}
}
