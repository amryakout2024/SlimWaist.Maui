using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using ClosedXML;
//using ClosedXML.Excel;
using SlimWaist.Models;
//using DocumentFormat.OpenXml.InkML;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace SlimWaist.Helpers
{
    public class ReadExcel
    {

        public static void ReadExcelSheet()
        {

            string fileName = @"D:\Csharp\Foods.txt";

            //using (StreamWriter fs = new StreamWriter(fileName))
            //{
            //    using (XLWorkbook workbook = new XLWorkbook(@"D:\Csharp\n.xlsx"))
            //    {

            //        int FoodId = 1;

            //        foreach (var sheet in workbook.Worksheets)
            //        {
            //            foreach (var row in sheet.RowsUsed().Skip(2))
            //            {
            //                fs.WriteLine("new Food" + "{"
            //                    + "FoodId=" + FoodId.ToString() + ","
            //                    + "FoodCategory=" + sheet.Name + ","
            //                    + "FoodName=" + row.Cell(1).Value.ToString() + ","
            //                    + "FoodCalories=" + row.Cell(2).Value.ToString() + ","
            //                    + "FoodProtien=" + row.Cell(3).Value.ToString() + ","
            //                    + "FoodFat=" + row.Cell(4).Value.ToString() + ","
            //                    + "FoodFibers=" + row.Cell(5).Value.ToString() + ","
            //                    + "FoodCarb=" + row.Cell(6).Value.ToString() + ","
            //                    + "},");
            //                FoodId++;
            //            }

            //        }
            //    }


            //}


        }
    }
}
