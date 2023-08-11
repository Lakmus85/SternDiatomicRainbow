using System;
using System.Drawing;

namespace SternDiatomicRainbow
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int size = 32000;
            
            // How stretched vertically should the resulting image be
            const int verticalScale = 8;
            
            var values = new int[size];
            PopulateDiatomicSequence(values, out var max);

            var bmp = new Bitmap(values.Length, (max + 1) * verticalScale);
            DrawBackground(bmp, size);
            DrawData(bmp, values, max, verticalScale);

            bmp.Save($"res_{values.Length}.png");
        }

        private static void DrawData(Bitmap bmp, int[] values, int max, int verticalScale)
        {
            using (var gr = Graphics.FromImage(bmp))
            {
                for (var i = 0; i < values.Length; i++)
                {
                    var xProportion = 1.0 * i / values.Length;

                    var color = ColorExtensions.ColorFromHSL(xProportion * 330 - 0 + 180, 1, 0.5);
                    Brush brush = new SolidBrush(color);

                    gr.FillRectangle(brush, i, (max - values[i]) * verticalScale, 2, values[i] * verticalScale);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="numberOfStepsInBackgroundGradient">
        /// The number of steps varies from 1 (just one color, not even a gradient)
        /// to the size of the sequence (the smoothest gradient)
        /// </param>
        private static void DrawBackground(Bitmap bmp, int numberOfStepsInBackgroundGradient)
        {
            using (var gr = Graphics.FromImage(bmp))
            {
                var gradientStepWidth = bmp.Width / numberOfStepsInBackgroundGradient;
                for (var i = 0; i <= bmp.Width / gradientStepWidth; i++)
                {
                    var xProportion = 1.0 * gradientStepWidth * i / bmp.Width;
                    var color = ColorExtensions.ColorFromHSL(xProportion * 330 - 0, 1, 0.5);
                    Brush brush = new SolidBrush(color);
                    gr.FillRectangle(brush, i * gradientStepWidth, 0, gradientStepWidth, bmp.Height);
                }
            }
        }

        private static void PopulateDiatomicSequence(int[] values, out int max)
        {
            values[0] = 0;
            values[1] = 1;
            max = 0;
            for (var i = 2; i < values.Length; i++)
            {
                if (i % 2 == 0)
                {
                    values[i] = values[i / 2];
                }
                else
                {
                    values[i] = values[i / 2] + values[i / 2 + 1];
                }

                max = Math.Max(max, values[i]);
            }
        }
    }
}