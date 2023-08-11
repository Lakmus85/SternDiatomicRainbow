using System.Drawing;

namespace SternDiatomicRainbow
{
    // The following code has been adapted from the following website
    // https://www.programmingalgorithms.com/algorithm/hsl-to-rgb/
    public static class ColorExtensions
    {
        public static Color ColorFromHSL(double H, double S, double L)
        {
            byte r, g, b;

            if (S == 0)
            {
                r = g = b = (byte)(L * 255);
            }
            else
            {
                double v1, v2;
                var hue = H / 360;

                v2 = L < 0.5 ? L * (1 + S) : L + S - L * S;
                v1 = 2 * L - v2;

                r = (byte)(255 * HueToRGB(v1, v2, hue + 1.0f / 3));
                g = (byte)(255 * HueToRGB(v1, v2, hue));
                b = (byte)(255 * HueToRGB(v1, v2, hue - 1.0f / 3));
            }

            return Color.FromArgb(r, g, b);
        }

        private static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
                vH += 1;

            if (vH > 1)
                vH -= 1;

            if (6 * vH < 1)
                return v1 + (v2 - v1) * 6 * vH;

            if (2 * vH < 1)
                return v2;

            if (3 * vH < 2)
                return v1 + (v2 - v1) * (2.0f / 3 - vH) * 6;

            return v1;
        }
    }
}