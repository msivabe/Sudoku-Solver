using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Solver.DataProvider
{
    /// <summary>
    /// Data provider contract for the app
    /// </summary>
    public interface IDataProvider
    {
        Task<int[,]> GetData();
    }
}
