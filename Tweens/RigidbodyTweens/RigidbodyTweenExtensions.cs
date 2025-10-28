using Frolics.Tweens.Experimental;
using UnityEngine;

namespace Frolics.Tweens.RigidbodyTweens {
	public static class RigidbodyTweenExtensions {
		public static PropertyTween<Rigidbody, Vector3> PlayPosition(
			this Rigidbody rb,
			Vector3 target,
			float duration
		) {
			return new PropertyTween<Rigidbody, Vector3>(
				rb,
				getter: r => r.position,
				setter: (r, v) => r.MovePosition(v),
				end: target,
				duration: duration,
				lerp: Vector3.Lerp
			);
		}

		public static PropertyTween<Rigidbody, Quaternion> PlayRotation(
			this Rigidbody rb,
			Quaternion target,
			float duration
		) {
			return new PropertyTween<Rigidbody, Quaternion>(
				rb,
				getter: r => r.rotation,
				setter: (r, q) => r.MoveRotation(q),
				end: target,
				duration: duration,
				lerp: Quaternion.Lerp
			);
		}
	}
}
