using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class ColorExtensions {
		public static Color WithAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
		public static Color WithRed(this Color color, float red) => new(color.r, color.g, red, color.a);
		public static Color WithGreen(this Color color, float green) => new(color.r, green, color.b, color.a);
		public static Color WithBlue(this Color color, float blue) => new(color.r, color.g, blue, color.a);

		public static Color WithHue(this Color color, float hue) {
			Color.RGBToHSV(color, out _, out float s, out float v);
			return Color.HSVToRGB(hue, s, v).WithAlpha(color.a);
		}

		public static Color WithSaturation(this Color color, float saturation) {
			Color.RGBToHSV(color, out float h, out _, out float v);
			return Color.HSVToRGB(h, saturation, v).WithAlpha(color.a);
		}

		public static Color WithValue(this Color color, float value) {
			Color.RGBToHSV(color, out float h, out float s, out _);
			return Color.HSVToRGB(h, s, value).WithAlpha(color.a);
		}
	}
}
