using NUnit.Framework;
using Sudoku_Solver.DataProvider;
using System.Threading.Tasks;

namespace Sudoku_Solver_test
{
    [TestFixture]
    public class DataProviderIntegrationTest
    {
        [Test]
        public async Task Input_ValidData_Success()
        {
            var dataProvider = DataProviderFactory.Create();
            await dataProvider.GetData();
        }

     }
}
