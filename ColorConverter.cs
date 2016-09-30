using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorToneVector_Console_
{
    class ColorConverter
    {
#region RGB<-->HSV
        // RGB から HSV へ変換する
        /// <summary>
        /// H:0~360, S,V:0.0~1.0
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ColorHSV RGB2HSV(byte r, byte g, byte b)
        {
            return ToHSV((float)r / 255f, (float)g / 255f, (float)b / 255f);
        }

        public static ColorHSV RGB2HSV(Color color)
        {
            float r = (float)color.R / 255f;
            float g = (float)color.G / 255f;
            float b = (float)color.B / 255f;

            return ToHSV(r, g, b);
        }

        private static ColorHSV ToHSV(float r, float g, float b)
        {
            var list = new float[] { r, g, b };
            var max = list.Max();
            var min = list.Min();

            float h, s, v;
            if (max == min)
                h = 0;
            else if (max == r)
                h = (60 * (g - b) / (max - min) + 360) % 360;
            else if (max == g)
                h = 60 * (b - r) / (max - min) + 120;
            else
                h = 60 * (r - g) / (max - min) + 240;

            if (max == 0)
                s = 0;
            else
                s = (max - min) / max;

            v = max;

            return new ColorHSV(h, s, v);
        }

        // HSV から RGB へ変換する
        public static Color HSV2RGB(ColorHSV hsv)
        {
            Color result = Color.FromArgb(0, 0, 0);

            if (hsv == null)
            {
                return result;
            }

            float h = hsv.H;
            float s = hsv.S;
            float v = hsv.V;

            float r = v;
            float g = v;
            float b = v;

            if (s > 0f)
            {
                h *= 6f;
                int i = (int)h;
                float f = h - (float)i;
                switch (i)
                {
                    default:
                    case 0:
                        g *= 1f - s * (1f - f);
                        b *= 1f - s;
                        break;
                    case 1:
                        r *= 1f - s * f;
                        b *= 1f - s;
                        break;
                    case 2:
                        r *= 1f - s;
                        b *= 1f - s * (1f - f);
                        break;
                    case 3:
                        r *= 1f - s;
                        g *= 1f - s * f;
                        break;
                    case 4:
                        r *= 1f - s * (1f - f);
                        g *= 1f - s;
                        break;
                    case 5:
                        g *= 1f - s;
                        b *= 1f - s * f;
                        break;
                }
            }

            r *= 255f;
            g *= 255f;
            b *= 255f;

            result = Color.FromArgb((int)r, (int)g, (int)b);
            return result;
        }
#endregion
    }

}
