using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ColorToneVector_Console_
{
    public static class ColorToneData
    {
        private static List<ColorTone> colorData = new List<ColorTone>();

        public static void OpenDataSet()
        {
            string ExcelBookFileName = "ColorToneDataSet";

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;
            Workbook wb = ExcelApp.Workbooks.Open(@"C:\Users\ht235_000\Documents\Visual Studio 2013\Projects\ColorToneVector(Console)\" + ExcelBookFileName,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing);

            Worksheet ws1 = wb.Sheets[1];
            ws1.Select(Type.Missing);

            for (int i = 1; i <= 192; i++)
            {
                string toneName = ws1.Cells[i, 2].Value.ToString();
                double r = ws1.Cells[i, 6].Value, g = ws1.Cells[i, 7].Value, b = ws1.Cells[i, 8].Value;
                double h = ws1.Cells[i, 13].Value, s = ws1.Cells[i, 14].Value, v = ws1.Cells[i, 15].Value;
                int toneNumber = (int)ws1.Cells[i, 16].Value;

                colorData.Add(new ColorTone(toneName, r, g, b, h, s, v, toneNumber));
            }

            wb.Close(false, Type.Missing, Type.Missing);
            ExcelApp.Quit();

            Console.WriteLine("Read Color Tone Data Completed...");
        }

        public static List<ColorTone> getDataSet()
        {
            return colorData;
        }
    }
}
