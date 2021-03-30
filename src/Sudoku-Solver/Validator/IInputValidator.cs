using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver.Validator
{
    /// <summary>
    /// Input validator interface
    /// </summary>
    public interface IInputValidator
    {
        void Validate(int[,] graph);

        void ValidateBlocksDuplicate(Sudoku_Solver.SubMatrix3X3[] subMatrix3X3s);
    }
}
