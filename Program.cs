using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ColorToneVector_Console_
{
    class Program
    {
        private static Calculation calculation;
        private static List<int[]> toneVectores = new List<int[]>();
        private static Dictionary<string, int[]> toneVectoresWithPath = new Dictionary<string, int[]>();

        static void Main(string[] args)
        {
           // calculation = new Calculation();
            //toneVectoresWithPath = calculation.CalcAllDataSet();

            Precision pre = new Precision();
            pre.CalcPrecision(@"C:\Users\ht235_000\Documents\Laboratory\ColorWheel\Dataset\ToneDataSet\s1279_3.jpg");

            Console.ReadLine();
        }
    }
}
