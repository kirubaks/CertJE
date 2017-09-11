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

      /*  public Object[][] ReadData(string path, string testName)
        {
            ExcelReaderUtility xlReader = new ExcelReaderUtility(path);
            Hashtable rowdata = new Hashtable();
            // Get test case row number and test data start row number
            int testRowNumber = getRowNumber(sheetName, testNameColumn, testName);
            //XSSFRow testRowNumberx = getRowNumber(sheetName, testNameColumn, testName);
            int testDataStartRow = testRowNumber + 1;

            // Calculate test data row count
            int testDataRows = 0;
            for (int i = testDataStartRow; GetCellData(sheetName, testNameColumn, i).equals(testName); i++)
            {
                testDataRows++;
            }

            // Calculate test data column count
            int testDataCols = getCellCount(sheetName, testRowNumber) - testDataStartColumn + 1;

            // Define 2 dimensional object array to hold test data sets
            Object[][] testCaseDataSets = new Object[testDataRows][testDataCols];
            //Object[][] testCaseDataSets = new Object[testDataRows][2];

            // Read test data cells from Excel file and assign into Object[][] testCaseDataSets
            for (int i = 0; i < testDataRows; i++)

            {
                for (int j = 0; j < testDataCols; j++)
                {
                    rowdata.put(getCellData(sheetName, testRowNumber, testDataStartColumn + j), getCellData(sheetName, testDataStartColumn + j, testDataStartRow + i));

                    testCaseDataSets[i][j] = getCellData(sheetName, testDataStartColumn + j, testDataStartRow + i);
                }

                rowdata.clear();
            }

            return testCaseDataSets;
        }*/
    }
}
