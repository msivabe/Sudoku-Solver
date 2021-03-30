using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.DataProvider
{
    /// <summary>
    /// Data provider for the application - Reading data from the file
    /// </summary>
    internal class FileDataProvider : IDataProvider
    {
        /// <summary>
        /// File path of the data to read
        /// </summary>
        private readonly string filePath;

        public FileDataProvider(string filePath=null)
        {
            this.filePath = filePath;
        }

        #region "Interface methods"

        /// <summary>
        /// Read data from the file
        /// </summary>
        ///<exception cref = "InvalidDataException" >Throws when Invalid data read from the file </ exception >
        /// <returns>data as 2d array</returns>
        public async Task<int[,]> GetData()
        {
            var path = string.Empty;
            if(!File.Exists(filePath))
            {
                path = GetDataFilePath();
            }
            else
             path = this.filePath;

            var lines = await Task.Run<string[]>(()=> System.IO.File.ReadAllLines(path));
            if (lines.Length != 9)
            {
                throw new InvalidDataException();
            }

            return Int2DMatrixFromStringArray(lines);
        }

        #endregion "Interface methods"

        #region "Private methods"

        /// <summary>
        /// Get file path to read data
        /// </summary>
        ///<exception cref = "InvalidDataException" >Throws when data file missing</ exception >
        /// <returns>File path</returns>
        private string GetDataFilePath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path= Path.Combine(path, "data.txt");
            if (!File.Exists(path))
            {
                throw new DataProviderException("data file missing");
            }
            return path;
        }

        /// <summary>
        /// Int matrix from array
        /// </summary>
        /// <param name="dataArray">data array</param>
        /// ///<exception cref = "InvalidDataException" >Throws when issue in parsing</ exception >
        /// <returns>int matrix</returns>
        private int[,] Int2DMatrixFromStringArray(string[] dataArray)
        {
            var data = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                var strArr = dataArray[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArr.Length != 9)
                {
                    throw new InvalidDataException();
                }

                for (int j = 0; j < 9; j++)
                {
                    if (!int.TryParse(strArr[j], out var cellData))
                    {
                        throw new InvalidDataException();
                    }
                    data[i, j] = cellData;
                }
            }
            return data;
        }

        #endregion "Private methods"
    }
}
