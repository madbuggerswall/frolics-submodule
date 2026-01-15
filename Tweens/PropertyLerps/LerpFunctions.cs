using Frolics.Tweens.Types;
using UnityEngine;

namespace Frolics.Tweens.PropertyLerps {
	internal struct Vector3Lerp : ILerp<Vector3> {
		public Vector3 Evaluate(Vector3 start, Vector3 end, float t) => Vector3.Lerp(start, end, t);
	}

	internal struct Vector2Lerp : ILerp<Vector2> {
		public Vector2 Evaluate(Vector2 start, Vector2 end, float t) => Vector2.Lerp(start, end, t);
	}

	internal struct FloatLerp : ILerp<float> {
		public float Evaluate(float start, float end, float t) => Mathf.Lerp(start, end, t);
	}

	internal struct QuaternionLerp : ILerp<Quaternion> {
		Quaternion ILerp<Quaternion>.Evaluate(Quaternion start, Quaternion end, float t) {
			return Quaternion.Lerp(start, end, t);
		}
	}

	internal struct ColorLerp : ILerp<Color> {
		public Color Evaluate(Color start, Color end, float t) => Color.Lerp(start, end, t);
	}
}
