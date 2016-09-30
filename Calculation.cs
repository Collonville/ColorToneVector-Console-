using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace ColorToneVector_Console_
{
    class Calculation
    {
        private List<string> dateSetPath = new List<string>();
        private List<int[]> toneVectores = new List<int[]>();
        private Dictionary<string, int[]> toneVectoresWithPath = new Dictionary<string, int[]>();

        public Calculation()
        {
            ColorToneData.OpenDataSet();
        }

        public Dictionary<string, int[]> CalcAllDataSet()
        {
            OpenAllDataSet();

            Console.WriteLine("Start Calculate Tone Vector...");
            foreach (var path in dateSetPath.Select((v, i) => new { v, i }))
            {
                ToneVector toneVector = new ToneVector(ColorToneData.getDataSet());

                Bitmap bitmap = new Bitmap(path.v);
                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int bytes = bitmap.Width * bitmap.Height * 4;
                for (int i = 0; i < bytes; i += 4)
                {
                    Int32 value = Marshal.ReadInt32(data.Scan0, i);

                    byte r = (byte)(value & 0xff);
                    byte g = (byte)((value >> 8) & 0xff);
                    byte b = (byte)((value >> 16) & 0xff);

                    ColorHSV hsv = ColorConverter.RGB2HSV(r, g, b);

                    toneVector.doMethod1(hsv.H, hsv.S, hsv.V);
                }
                bitmap.UnlockBits(data);

                toneVectores.Add(toneVector.GetVector());
                toneVectoresWithPath.Add(path.v, toneVector.GetVector());

                Console.WriteLine(path.i);

                /*
                if (path.i == 5)
                    break;*/
            }
            Console.WriteLine("Finish!!");

            Write2Excel();

            return toneVectoresWithPath;
        }

        private void Write2Excel()
        {
            string ExcelBookFileName = "Tone-Vector";

            Console.WriteLine("Start to write to Excel...");
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;
            Workbook wb = ExcelApp.Workbooks.Add();

            Worksheet ws1 = wb.Sheets[1];
            ws1.Select(Type.Missing);

            for (int i = 1; i < toneVectores.Count() + 1; i++)
            {
                int[] fch = toneVectores[i - 1];
                Range rgn = ws1.Cells[i, 1];
                rgn.Value2 = dateSetPath[i - 1];

                for (int j = 2; j < 14 + 2; j++)
                {
                    Range rgnVal = ws1.Cells[i, j];
                    rgnVal.Value2 = fch[j - 2];
                }
            }

            wb.SaveAs(ExcelBookFileName);
            wb.Close(false);
            ExcelApp.Quit();

            Console.WriteLine("Finish!!");
        }

        private void OpenAllDataSet()
        {
            // ファイル名に「Hoge」を含み、拡張子が「.txt」のファイルを最下層まで検索し取得する
            string[] stFilePathes = GetFilesMostDeep(@"C:\Users\ht235_000\Documents\Laboratory\ColorWheel\Dataset\ToneDataSet\", "**.jpg");
            string stPrompt = string.Empty;

            // 取得したファイル名を列挙する
            foreach (string stFilePath in stFilePathes)
            {
                //stPrompt += stFilePath + System.Environment.NewLine;
                dateSetPath.Add(stFilePath);
            }
        }

        /// ---------------------------------------------------------------------------------------
        /// <summary>
        ///     指定した検索パターンに一致するファイルを最下層まで検索しすべて返します。</summary>
        /// <param name="stRootPath">
        ///     検索を開始する最上層のディレクトリへのパス。</param>
        /// <param name="stPattern">
        ///     パス内のファイル名と対応させる検索文字列。</param>
        /// <returns>
        ///     検索パターンに一致したすべてのファイルパス。</returns>
        /// ---------------------------------------------------------------------------------------
        private string[] GetFilesMostDeep(string stRootPath, string stPattern)
        {
            System.Collections.Specialized.StringCollection hStringCollection = (
                new System.Collections.Specialized.StringCollection()
            );

            foreach (string stFilePath in System.IO.Directory.GetFiles(stRootPath, stPattern))
            {
                hStringCollection.Add(stFilePath);
            }

            foreach (string stDirPath in System.IO.Directory.GetDirectories(stRootPath))
            {
                string[] stFilePathes = GetFilesMostDeep(stDirPath, stPattern);

                if (stFilePathes != null)
                {
                    hStringCollection.AddRange(stFilePathes);
                }
            }

            string[] stReturns = new string[hStringCollection.Count];
            hStringCollection.CopyTo(stReturns, 0);

            return stReturns;
        }
    }
}