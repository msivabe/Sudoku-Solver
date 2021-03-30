using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver.DataProvider
{
    /// <summary>
    /// Factory model - To create Data provider instance for the application 
    /// </summary>
    public class DataProviderFactory
    {
        /// <summary>
        /// Create data provider instance
        /// </summary>
        /// <returns>Data provider</returns>
        public static IDataProvider Create()
        {
            return new FileDataProvider();
        }
    }
}
