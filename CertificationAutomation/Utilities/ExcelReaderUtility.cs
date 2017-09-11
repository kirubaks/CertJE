using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;

namespace CertificationAutomation.Utilities
{
    public class ExcelReaderUtility
    {
        public static string fileName = "";
        public string path;
        public FileStream fis = null;
        public FileStream fileOut = null;
        private XSSFWorkbook workbook = null;
        private ISheet sheet = null;
        private IRow row = null;
        private NPOI.SS.UserModel.ICell cell = null;

        public ExcelReaderUtility(string path)
        {
            this.path = path;

            try
            {
                fis = new FileStream(path, FileMode.Open, FileAccess.Read);
                workbook = new XSSFWorkbook(fis);
                sheet = workbook.GetSheetAt(0);
                fis.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int GetRowCount(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
                return 0;
            else
            {
                sheet = workbook.GetSheetAt(index);
                int number = sheet.LastRowNum + 1;
                return number;
            }
        }

        //Returns the data from a cell
        public string GetCellData(string sheetName, string colName, int rowNum)
        {
            try
            {
                if (rowNum <= 0)
                    return "";

                int index = workbook.GetSheetIndex(sheetName);
                int col_Num = -1;
                if (index == -1)
                    return "";

                sheet = workbook.GetSheetAt(index);
                row = sheet.GetRow(0);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (row.GetCell(i).StringCellValue.Trim().Equals(colName.Trim()))
                    {
                        Console.WriteLine(row.GetCell(i).StringCellValue.Trim());
                        col_Num = i;
                    }
                }

                if (col_Num == -1)
                {
                    return "";
                }

                sheet = workbook.GetSheetAt(index);
                row = sheet.GetRow(rowNum - 1);
                if (row == null)
                    return "";
                cell = row.GetCell(col_Num);

                if (cell == null)
                {
                    return "";
                }

                else
                {
                    return cell.StringCellValue;
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return "Row " + rowNum + " or Column " + colName + " does not exist in XLS";
            }
        }

        public string GetCellData(string sheetName, int colNum, int rowNum)
        {
            try
            {
                if (rowNum <= 0)
                    return "";

                int index = workbook.GetSheetIndex(sheetName);
                if (index == -1)
                    return "";

                sheet = workbook.GetSheetAt(index);
                row = sheet.GetRow(rowNum - 1);
                if (row == null)
                    return "";

                var cell = row.GetCell(colNum);

                if (cell == null)
                {
                    return "";
                }

                switch (cell.CellType)
                {
                    case CellType.String:
                        return cell.StringCellValue;
                    case CellType.Numeric:
                        return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                    case CellType.Formula:
                        return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                    case CellType.Boolean:
                        return cell.BooleanCellValue.ToString(CultureInfo.InvariantCulture);
                    case CellType.Blank:
                        return "";
                    case CellType.Unknown:
                        return "Invalid Type";
                    default:
                        return cell.StringCellValue;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return "Row " + rowNum + " or Column " + colNum + " does not exist in XLS";
            }
        }

        //Check if sheet exists
        public bool IsSheetExist(string sheetName)
        {
            int index = workbook.GetSheetIndex(sheetName);
            if (index == -1)
            {
                index = workbook.GetSheetIndex(sheetName.ToUpper());
                if (index == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }

        //Get number of columns in a sheet
        public int GetColumnCount(string sheetName)
        {
            //check if the sheet exists
            if (!IsSheetExist(sheetName))
                return -1;
            sheet = workbook.GetSheet(sheetName);
            row = sheet.GetRow(0);

            if (row == null)
                return -1;

            return row.LastCellNum;
        }

        public int GetCellRowNum(string sheetName, int colName, string cellValue)
        {
            for (int i = 2; i < GetRowCount(sheetName); i++)
            {
                if (GetCellData(sheetName, colName, i).Equals(cellValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

   
    }
}
