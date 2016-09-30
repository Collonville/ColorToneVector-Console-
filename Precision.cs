using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ColorToneVector_Console_
{
    class Precision
    {
        private Dictionary<string, int[]> toneVectoresWithPath = new Dictionary<string, int[]>();
        private Dictionary<string, double[]> normedToneVectoresWithPath = new Dictionary<string, double[]>();
        private Dictionary<string, double> precisionRateWithPath = new Dictionary<string, double>();

        private double[] queryNormHist = new double[14];

        public void CalcPrecision(string queryPath)
        {
            OpenDataSet();

            //クエリー画像の特徴ベクトルを正規化
            int[] queryVector = toneVectoresWithPath[queryPath];
            int sum = queryVector.Sum();
            for (int i = 0; i < 13; i++)
            {
                queryNormHist[i] = (double)queryVector[i] / (double)sum;
            }

            //全データセットの特徴ベクトルを正規化
            foreach (KeyValuePair<string, int[]> pair in toneVectoresWithPath)
            {
                double[] normedVector = new double[14];
                sum = pair.Value.Sum();

                for (int i = 0; i < 13; i++)
                {
                    normedVector[i] = (double)pair.Value[i] / (double)sum;
                }
                normedToneVectoresWithPath.Add(pair.Key, normedVector);
            }

            //Histogram Intersectionからクエリ画像との適合率を計算する
            foreach (KeyValuePair<string, double[]> pair in normedToneVectoresWithPath)
            {
                double querySum = queryNormHist.Sum();
                double numerator = 0.0;

                for (int i = 0; i < pair.Value.Count(); i++)
                {
                    numerator += Math.Min(queryNormHist[i], pair.Value[i]);
                }

                precisionRateWithPath.Add(pair.Key, numerator / querySum);
            }

            var vs1 = precisionRateWithPath.OrderByDescending((x) => x.Value).Take(10);
            foreach (var v in vs1)
            {
                Console.WriteLine(string.Format("{0}:{1}", v.Key, v.Value));
            }
        }

        private void OpenDataSet()
        {
            string ExcelBookFileName = "Tone-Vector";

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;
            Workbook wb = ExcelApp.Workbooks.Open(@"C:\Users\ht235_000\Documents\" + ExcelBookFileName,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
              Type.Missing);

            Worksheet ws1 = wb.Sheets[1];
            ws1.Select(Type.Missing);

            for (int i = 1; i <= 580; i++)
            {
                string path = ws1.Cells[i, 1].Value.ToString();
                int[] vector = new int[14];

                for(int j = 2; j < vector.Count() + 2 ; j++)
                {
                    vector[j - 2] = (int)ws1.Cells[i, j].Value;
                }

                toneVectoresWithPath.Add(path, vector);
            }

            wb.Close(false, Type.Missing, Type.Missing);
            ExcelApp.Quit();

            Console.WriteLine("Read Caluculated data Completed...");
        }
    }
}