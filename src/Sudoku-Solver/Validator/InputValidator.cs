using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace Sudoku_Solver.Validator
{
    /// <summary>
    /// Input validator 
    /// </summary>
    internal class InputValidator:IInputValidator
    {

        #region "Interface methods"

        /// <summary>
        /// Validate input data
        /// </summary>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        /// <param name="graph">input data</param>
        public void Validate(int[,] graph)
        {
            InputMatrix9X9IndexCheck(graph);
            MandatoryDataCheck(graph);
            InvalidNumberCheck(graph);
            DuplicateCheckInRow(graph);
            DuplicateCheckInColumn(graph);
            Serilog.Log.Information("data validation passed");
        }


        #endregion "Interface methods"

        #region "Private methods"

        /// <summary>
        /// Input data matrix index check
        /// </summary>
        /// <param name="inputGraph">Input data</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        private void InputMatrix9X9IndexCheck(int[,] inputGraph)
        {
            if (!(inputGraph.GetLength(0) == 9))
            {
                throw new InputValidatorException("input graph index 9X9 not matching");
            }
        }

        /// <summary>
        /// Min value in the matrix check - Its reference value
        /// </summary>
        /// <param name="inputGraph">input data</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        private void MandatoryDataCheck(int[,] inputGraph)
        {
           if(inputGraph.Cast<int>().Count(x => (x > 0 && x < 10))<3)
           {
                throw new InputValidatorException("input data not having mandatory numbers in the matrix");
           }
        }

        /// <summary>
        /// Invalid number check in the input data
        /// </summary>
        /// <param name="inputGraph">input data</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        private void InvalidNumberCheck(int[,] inputGraph)
        {
            if (inputGraph.Cast<int>().Count(x => (x < 0 || x >= 10)) >0)
            {
                throw new InputValidatorException("Invalid data in the input data");
            }
        }

        /// <summary>
        /// Duplicate value validation based on the column
        /// </summary>
        /// <param name="inputGraph">input data</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        private void DuplicateCheckInColumn(int[,] inputGraph)
        {
            List<int> tmp = new List<int>();
            for (int col = 0; col < inputGraph.GetLength(1); col++)
            {
                tmp.Clear();
                for (int row = 0; row < inputGraph.GetLength(0); row++)
                {
                    if(inputGraph[row, col]>0 && tmp.Contains(inputGraph[row,col]))
                    {
                        throw new InputValidatorException($"Duplicate entry in col:{col})");
                    }
                    else
                    {
                        tmp.Add(inputGraph[row, col]);
                    }
                }
            }
         }


        /// <summary>
        /// Duplicate value check in the row
        /// </summary>
        /// <param name="inputGraph">input data to check</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        private void DuplicateCheckInRow(int[,] inputGraph)
        {
            var tmp = new List<int>();
            for (int row = 0; row < inputGraph.GetLength(0); row++)
            {
                tmp.Clear();
                for (int col = 0; col < inputGraph.GetLength(0); col++)
                {
                    if (inputGraph[row, col] > 0 && tmp.Contains(inputGraph[row, col]))
                    {
                        throw new InputValidatorException($"Duplicate entry in row:{row})");
                    }
                    else
                    {
                        tmp.Add(inputGraph[row, col]);
                    }
                }
            }
        }

        /// <summary>
        /// Validate input data based on the matrix block
        /// </summary>
        /// <param name="subMatrix3X3s">sub matrix array</param>
        /// <exception cref="InputValidatorException">Throws when input data validation fails</exception>
        public void ValidateBlocksDuplicate(SubMatrix3X3[] subMatrix3X3s)
        {
            var tmp = new List<int>();
            foreach (var block in subMatrix3X3s)
            {
                var blockMatrix = block.GetBlockMatrix();
                tmp.Clear();
                for (int row = 0; row < blockMatrix.GetLength(0); row++)
                {
                    for (int col = 0; col < blockMatrix.GetLength(0); col++)
                    {
                        if (blockMatrix[row, col] == 0)
                            continue;
                        if (!tmp.Contains(blockMatrix[row, col]))
                            tmp.Add(blockMatrix[row, col]);
                        else
                            throw new Sudoku_Solver.Validator.InputValidatorException("Duplicate value inside block");
                    }
                }
            }
        }

        #endregion "Interface methods"
    }
}
