using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Definitions.Utilities
{
    public class ExcelReader
    {
        public DataTable ReadDataFromFile(Stream stream)
        {
            DataSet dsExcelInfo;
            DataTable dtInfo = new DataTable();

            using (var memoryStream = new MemoryStream())
            {
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                dsExcelInfo = excelReader.AsDataSet(conf);
                excelReader.Close();

                dtInfo = dsExcelInfo.Tables[0];
            }

            return dtInfo;
        }
    }
}
