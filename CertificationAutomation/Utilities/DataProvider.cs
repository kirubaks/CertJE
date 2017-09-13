using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificationAutomation.Utilities
{
    public class DataProvider : ExcelReaderUtility
    {
        public static int testNameColumn = 1;
        public static int testDataStartColumn = 2;

        public DataProvider(string path) : base(path)
        {
        }

        public static Hashtable getData()
        {
            int rows = 0;
            ExcelReaderUtility xls = new ExcelReaderUtility("C:\\Test\\Temp.xlsx");
            String sheetName = "Sheet1";
            String testCaseName = "LoginTest";

            int testStartRowNum = 1;

            while (!xls.GetCellData(sheetName, 0, testStartRowNum).Equals(testCaseName))
            {
                testStartRowNum++;
            }

            Console.WriteLine("Test Starts from row - " + testStartRowNum);
            int colStartRowNum = testStartRowNum + 1;
            int dataStartRowNum = testStartRowNum + 2;

            while (!xls.GetCellData(sheetName, 0, dataStartRowNum + rows).Equals(""))
            {
                rows++;
            }

            //Calculate total number of cols
            int cols = 0;
            while (!xls.GetCellData(sheetName, cols, colStartRowNum).Equals(""))
            {
                cols++;
            }
            Console.WriteLine("Total cols are: " + cols);


            Object[,] data = new Object[rows, 1];

            int datarow = 0;

            Hashtable hashtable = null;

            for (int rnum = dataStartRowNum; rnum < dataStartRowNum + rows; rnum++)
            {
                hashtable = new Hashtable();
                for (int cnum = 0; cnum < cols; cnum++)
                {
                    String key = xls.GetCellData(sheetName, cnum, colStartRowNum);
                    String value = xls.GetCellData(sheetName, cnum, rnum);
                    hashtable.Add(key, value);
                    //data[datarow][cnum]=xls.getCellData(sheetName, cnum, rnum);
                }
                data[datarow,0] = hashtable;
                datarow++;
            }

            return hashtable;
        }
    }
}
