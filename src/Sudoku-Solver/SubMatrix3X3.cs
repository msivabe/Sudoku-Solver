using System;
using System.Collections.Generic;

namespace Sudoku_Solver
{
    /// <summary>
    /// Sub matrix blocks for sudoku solver
    /// </summary>
    public class SubMatrix3X3
    {
        /// <summary>
        /// Sub matrix
        /// </summary>
        private readonly int[,] blockMatrix;

        /// <summary>
        /// Numbers in the sub matrix
        /// </summary>
        private readonly List<int> blockNumbers = new List<int>();

       public SubMatrix3X3(int[,] graph)
            =>   this.blockMatrix = graph;
       
        public void SetMatrixValue(ValueTuple<int,int> rowCol , int data)
        {
            blockMatrix[rowCol.Item1,rowCol.Item2] = data;
            blockNumbers.Add(data);
        }

        /// <summary>
        /// Get block matrix
        /// </summary>
        /// <returns>Submatrix </returns>
        public int[,] GetBlockMatrix()
           => this.blockMatrix;

        /// <summary>
        /// Add number to matrix
        /// </summary>
        /// <param name="num">Number to add</param>
        public void AddNum(int num)
            =>  blockNumbers.Add(num);
        
        /// <summary>
        /// Remove number from the matrix
        /// </summary>
        /// <param name="num">Number to remove</param>
        public void RemoveNum(int num)
            => blockNumbers.Remove(num);

        /// <summary>
        /// Check number exist in the matrix
        /// </summary>
        /// <param name="num">Number to check</param>
        /// <returns>result bool value</returns>
        public bool CheckNumber(int num)
            => blockNumbers.Contains(num);
    }
}
