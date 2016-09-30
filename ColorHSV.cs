using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorToneVector_Console_
{
    class ColorHSV
    {
        public float H;
        public float S;
        public float V;

        public ColorHSV()
        {
            H = 0;
            S = 0;
            V = 0;
        }

        public ColorHSV(float h, float s, float v)
        {
            H = h;
            S = s;
            V = v;
        }

    }
}
