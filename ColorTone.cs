using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorToneVector_Console_
{
    public class ColorTone
    {
        public string toneName;
        public double r, g, b;
        public double h, s, v;
        public int toneNumber;

        public ColorTone(string toneName, double r, double g, double b, double h, double s, double v, int toneNumber)
        {
            this.toneName = toneName;

            this.r = r;
            this.g = g;
            this.b = b;

            this.h = h;
            this.s = s;
            this.v = v;

            this.toneNumber = toneNumber;
        }
    }
}
