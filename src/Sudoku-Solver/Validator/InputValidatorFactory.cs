using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_Solver.Validator
{
    /// <summary>
    /// Input validator factory
    /// </summary>
    public class InputValidatorFactory
    {
        /// <summary>
        /// Create input validator instance
        /// </summary>
        /// <returns></returns>
        public static IInputValidator Create()
        {
            return new InputValidator();
        }
    }
}
