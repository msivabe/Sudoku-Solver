using Sudoku_Solver.DataProvider;
using Sudoku_Solver.Validator;

using FakeItEasy;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sudoku_Solver_test
{
    [TestFixture]
    public class SudokuResolverTest
    {
        [Test]
        public async Task Input_Process()
        {
            var fakeDataProvider = A.Fake<IDataProvider>();
            A.CallTo(() => fakeDataProvider.GetData()).Returns(InputData());
            var dataProvider = DataProviderFactory.Create();
            var resolver = new Sudoku_Solver.Sudoku9X9Solver(
                                InputValidatorFactory.Create());
            var (result,outputMatrix) = await resolver.Process(await fakeDataProvider.GetData());
            Assert.IsTrue(result);
            Assert.IsTrue(MatchMatricResult(outputMatrix, ExpectedOutput()));
        }

        [Test]
        public async Task Input_Process_OutputNotExpected()
        {
            var fakeDataProvider = A.Fake<IDataProvider>();
            A.CallTo(() => fakeDataProvider.GetData()).Returns(InputData());
            var dataProvider = DataProviderFactory.Create();
            var resolver = new Sudoku_Solver.Sudoku9X9Solver( 
                           InputValidatorFactory.Create());
            var (result, outputMatrix) = await resolver.Process(await fakeDataProvider.GetData());
            Assert.IsTrue(result);
            Assert.IsFalse(MatchMatricResult(outputMatrix, InvalidExpectedOutput()));
        }

        #region "Private methods"

        private bool MatchMatricResult(int[,] result,int[,] expected)
        {
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    if (result[i, j] != expected[i, j])
                        return false;
                }
            }

            return true;
        }


        private int[,] InputData()
        {
          return new int[,]
                         {
                              {3,0,6,5,0,8,4,0,0 },
                              {5,2,0,0,0,0,0,0,0 },
                              {0,8,7,0,0,0,0,3,1 },
                              {0,0,3,0,1,0,0,8,0 },
                              {9,0,0,8,6,3,0,0,5 },
                              {0,5,0,0,9,0,6,0,0 },
                              {1,3,0,0,0,0,2,5,0 },
                              {0,0,0,0,0,0,0,7,4 },
                              {0,0,5,2,0,6,3,0,0 },
                         };
        }

        private int[,] ExpectedOutput()
        {
            return new int[,]
                           {
                              {3,1,6,5,7,8,4,9,2 },
                              {5,2,9,1,3,4,7,6,8 },
                              {4,8,7,6,2,9,5,3,1 },
                              {2,6,3,4,1,5,9,8,7 },
                              {9,7,4,8,6,3,1,2,5 },
                              {8,5,1,7,9,2,6,4,3 },
                              {1,3,8,9,4,7,2,5,6 },
                              {6,9,2,3,5,1,8,7,4 },
                              {7,4,5,2,8,6,3,1,9 },
                           };
        }

        private int[,] InvalidExpectedOutput()
        {
            return new int[,]
                           {
                              {3,1,6,5,7,8,4,9,2 },
                              {5,2,9,1,3,4,7,6,8 },
                              {4,8,7,6,2,9,5,3,1 },
                              {2,6,3,4,1,5,9,8,7 },
                              {9,7,4,8,6,3,1,2,5 },
                              {8,5,1,7,9,2,6,4,3 },
                              {1,3,8,9,4,7,2,5,6 },
                              {6,9,2,3,5,1,8,7,4 },
                              {7,4,5,2,8,6,3,1,8 },
                           };
        }

        #endregion "Private methods"

    }
}
