using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ClosedXML.Excel;

namespace Definitions.Utilities
{
    public static class ExportFile
    {
        public const string excelFileExtension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static XLWorkbook ExportExcel(DataTable inputData, string fileName)
        {
            inputData.TableName = fileName;
            DataSet dsData = new DataSet(fileName);
            dsData.Tables.Add(inputData);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsData);

                for (int i = 0; i < dsData.Tables.Count; i++)
                {
                    var ws = wb.Worksheet(i + 1);
                    var header = ws.Row(1);
                    var col_1 = ws.Column(2);
                    var col_2 = ws.Column(4);
                    var col_3 = ws.Column(6);

                    header.Cells("1, 1:" + inputData.Columns.Count + "").Style.Fill.BackgroundColor = XLColor.LightGray;
                    header.Cells("1, 1:" + inputData.Columns.Count + "").Style.Font.FontColor = XLColor.RichBlack;
                    header.Cells("1, 1:" + inputData.Columns.Count + "").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false; //remove autofilter
                    ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Table(i).Theme = XLTableTheme.None;
                    col_1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    col_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    col_3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                }
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                //wb.Style.Font.FontColor = XLColor.Black;
                return wb;
            }
        }
    }
}
