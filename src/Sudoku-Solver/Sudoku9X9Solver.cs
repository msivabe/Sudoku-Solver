using System;
using Sudoku_Solver.Validator;
using System.Threading.Tasks;

namespace Sudoku_Solver
{
   /// <summary>
   /// Sudoku solver main flow
   /// </summary>
    public class Sudoku9X9Solver
    {
        /// <summary>
        /// Input validator
        /// </summary>
        private readonly IInputValidator inputValidator;
        /// <summary>
        /// Sub matrix array
        /// </summary>
        private readonly SubMatrix3X3[] blocks = new SubMatrix3X3[9];

        public Sudoku9X9Solver(IInputValidator inputValidator)
        {
            this.inputValidator = inputValidator;
        }

        #region "Public methods"

        /// <summary>
        /// Perform sudoku solve
        /// </summary>
        /// <param name="graph">Input data to perform sudoku solve</param>
        /// <returns>Result of the process - Bool and Updated matrix</returns>
        public async Task<(bool,int[,])> Process(int[,] graph)
        {
            inputValidator.Validate(graph);
            ParseMatrixBlocks(graph);
            inputValidator.ValidateBlocksDuplicate(blocks);
             var res = await AttainSoln(graph).ConfigureAwait(false);
            return (res, graph);
        }

        /// <summary>
        /// Parse submatrix from the main data based on row and col range
        /// </summary>
        /// <param name="graph">Input data</param>
        /// <param name="rowRange">Row range to parse</param>
        /// <param name="colRange">Column range to parse</param>
        /// <returns>Sub matrix</returns>
        public SubMatrix3X3 SplitMatrixTo3X3(int[,] graph, ValueTuple<int, int> rowRange, ValueTuple<int, int> colRange)
        {
            var tmp = new int[3, 3];
            var rIndex = 0;

            for (int i = rowRange.Item1; i <= rowRange.Item2; i++)
            {
                var cIndex = 0;
                for (int j = colRange.Item1; j <= colRange.Item2; j++)
                {
                    tmp[rIndex, cIndex] = graph[i, j];
                    cIndex++;

                }
                rIndex++;
            }
            return new SubMatrix3X3(tmp);
        }

        #endregion "Public methods"

        #region "Private methods"

        /// <summary>
        /// Parse main matrix into sub block matrix array
        /// </summary>
        /// <param name="graph">input data</param>
        private void ParseMatrixBlocks(int[,] graph)
        {
            blocks[0] = SplitMatrixTo3X3(graph, (0, 2), (0, 2));
            blocks[1] = SplitMatrixTo3X3(graph, (0, 2), (3, 5));
            blocks[2] = SplitMatrixTo3X3(graph, (0, 2), (6, 8));
            blocks[3] = SplitMatrixTo3X3(graph, (3, 5), (0, 2));
            blocks[4] = SplitMatrixTo3X3(graph, (3, 5), (3, 5));
            blocks[5] = SplitMatrixTo3X3(graph, (3, 5), (6, 8));
            blocks[6] = SplitMatrixTo3X3(graph, (6, 8), (0, 2));
            blocks[7] = SplitMatrixTo3X3(graph, (6, 8), (3, 5));
            blocks[8] = SplitMatrixTo3X3(graph, (6, 8), (6, 8));
        }

        /// <summary>
        /// Perform recursive sudoku solver
        /// </summary>
        /// <param name="graph">input data</param>
        /// <returns>result of the process</returns>
        private async Task<bool> AttainSoln(int[,] graph)
        {
            var emptyCheck = IsGraphHasEmptyCell(graph);

            if (!emptyCheck.Item1)
                return true;


            for (int assignNum = 1; assignNum < 10; assignNum++)
            {
                var blockIndex = IdentifyBlock(emptyCheck.Item2, emptyCheck.Item3);
                if (ValidateNumber(graph,emptyCheck.Item2, emptyCheck.Item3, blockIndex, assignNum))
                {
                    graph[emptyCheck.Item2, emptyCheck.Item3] = assignNum;
                    blocks[blockIndex].AddNum(assignNum);

                    var res = await AttainSoln(graph);

                    if (res)
                    {
                        return true;
                    }
                    else
                    {
                        graph[emptyCheck.Item2, emptyCheck.Item3] = 0;
                        blocks[blockIndex].RemoveNum(assignNum);
                    }
                }
            }

            return false;
        }

      /// <summary>
      /// Empty cell check in the matrix
      /// </summary>
      /// <param name="graph">input data</param>
      /// <returns>result with empty row and column index</returns>
        private (bool,int,int) IsGraphHasEmptyCell(int[,] graph)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {

                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (IsEmptyCell(graph[i, j]))
                    {
                        return (true,i,j);
                    }
                }
            }
            return (false,0,0);
        }

        /// <summary>
        /// Empty cell check
        /// </summary>
        /// <param name="cellVal">Cell value</param>
        /// <returns>Result</returns>
        private bool IsEmptyCell(int cellVal)
             => (cellVal == 0);

        /// <summary>
        /// Validate number to assign in the cell
        /// </summary>
        /// <param name="graph">input data</param>
        /// <param name="row">Row to assign</param>
        /// <param name="col">Column to assign</param>
        /// <param name="blockIndex">Block of the row and col</param>
        /// <param name="num">Number to assign to the cell</param>
        /// <returns>Result of the validation</returns>
        private bool ValidateNumber(int[,] graph,int row, int col, int blockIndex,int num)
        {
            var rCheck = CheckRow(graph,row, num);
            var cCheck = CheckColumn(graph,col, num);
            var bCheck =  blocks[blockIndex].CheckNumber(num);
            return (rCheck && cCheck && (!bCheck));
        }

        /// <summary>
        /// Check row for the cell value 
        /// </summary>
        /// <param name="graph">input data</param>
        /// <param name="row">row index</param>
        /// <param name="num">Number to check</param>
        /// <returns>result</returns>
        private bool CheckRow(int[,] graph,int row, int num)
        {
            for(int col=0;col<9;col++)
            {
                if (graph[row,col] == num)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check column for the cell value
        /// </summary>
        /// <param name="graph">input data</param>
        /// <param name="col">column index</param>
        /// <param name="num">Number to check</param>
        /// <returns>result</returns>
        private bool CheckColumn(int[,] graph,int col, int num)
        {
            for (var row = 0; row < 9; row++)
            {
                if (graph[row, col] == num)
                    return false;
            }
            return true;
        }
      
        /// <summary>
        /// Identify submatrix block based on row and column index
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="col">Column index</param>
        /// <returns>Block index</returns>
        private int IdentifyBlock(int row, int col)
        {
            if(row<3)
            {
                if(col<=2)
                {
                    return 0;
                }
                else if(col>2 && col <=5)
                {
                    return 1;
                }
                else 
                {
                    return 2;
                }
            }
            else if(row>=3 && row <6)
            {
                if (col <= 2)
                {
                    return 3;
                }
                else if (col > 2 && col <= 5)
                {
                    return 4;
                }
                else 
                {
                    return 5;
                }

            }
            else 
            {

                if (col <= 2)
                {
                    return 6;
                }
                else if (col > 2 && col <= 5)
                {
                    return 7;
                }
                else 
                {
                    return 8;
                }
            }
        }

        #endregion "Private methods"
    }
}
