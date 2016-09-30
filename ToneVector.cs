using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorToneVector_Console_
{
    public class ToneVector
    {
        private List<ColorTone> colorToneData;
        private int[] vector = new int[14];

        public ToneVector(List<ColorTone> colorToneData)
        {
            this.colorToneData = colorToneData;
        }

        public void doMethod1(float h, float s, float v)
        {

            List<ColorTone> colorTone = null;

            #region トーン名の大まかな分類
            if(s > 0.9)
            {
                colorTone = colorToneData.Where(x => x.toneNumber <= 3 && x.toneNumber >= 0).OrderBy(x => x.v).ToList();
            }
            else if (0.3 < s && 0.9 >= s)
            {
                colorTone = colorToneData.Where(x => x.toneNumber <= 8 && x.toneNumber >= 4).OrderBy(x => x.v).ToList();
            }
            else if (0.1 <= s && 0.3 >= s)
            {
                colorTone = colorToneData.Where(x => x.toneNumber <= 13 && x.toneNumber >= 9).OrderBy(x => x.v).ToList();
            }
            else
            {
                //白～黒のグレー間
            }
            #endregion

            if (colorTone != null)
            {
                foreach (var data in colorTone)
                {
                    if (Math.Abs(data.v - v * 100) < 30)
                    {
                        vector[data.toneNumber]++;
                    }
                }
            }
        }

        public void ShowVector()
        {
            int sum = vector.Sum();

            for (int i = 0; i < vector.Count(); i++)
            {
                Console.WriteLine("{0}:{1}", i, (vector[i] / (float)sum));
            }
        }

        public int[] GetVector()
        {
            return vector;
        }
    }
}
