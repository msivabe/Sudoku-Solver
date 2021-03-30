using NUnit.Framework;
using Sudoku_Solver.Validator;

namespace Sudoku_Solver_test
{
    [TestFixture]
    public class InputValidatorTest
    {
        [Test]
        public void Input_ValidData_Success()
        {
            var validator = InputValidatorFactory.Create();
            validator.Validate(ValidData());
        }

        [Test]
        public void Input_Matrix9X9NotMatching_ThrowInputValidatorE()
        {
            var validator = InputValidatorFactory.Create();
            Assert.Throws<InputValidatorException>(()=>validator.Validate(InvalidMatrixSize()));
        }

        [Test]
        public void Input_MinDataNotPresent_ThrowInputValidatorEx()
        {
            var validator = InputValidatorFactory.Create();
            Assert.Throws<InputValidatorException>(() => validator.Validate(MandatoryDataNotPresent()));
        }


        [Test]
        public void Input_DuplicateNumberInRow_ThrowInputValidatorEx()
        {
            var validator = InputValidatorFactory.Create();
            Assert.Throws<InputValidatorException>(() => validator.Validate(InvalidDataWithDuplicateInRow()));
        }

        [Test]
        public void Input_DuplicateNumberInColumn_ThrowInputValidatorEx()
        {
            var validator = InputValidatorFactory.Create();
            Assert.Throws<InputValidatorException>(() => validator.Validate(InvalidDataWithDuplicateInColumn()));
        }

        [Test]
        public void Input_InvalidNumber_ThrowInputValidatorEx()
        {
            var validator = InputValidatorFactory.Create();
            Assert.Throws<InputValidatorException>(() => validator.Validate(InvalidNumberInInput()));
        }

        #region "Private methods"
        private int[,] ValidData()
        {
            return  new int[,]
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

        private int[,] InvalidMatrixSize()
        {
            return new int[,]
            {
                 {3,0,6,5,0,8,4,0 },
                 {5,2,0,0,0,0,0,0 },
                 {0,8,7,0,0,0,0,3 },
                 {0,0,3,0,1,0,0,8 },
                 {9,0,0,8,6,3,0,0 },
                 {0,5,0,0,9,0,6,0 },
                 {1,3,0,0,0,0,2,5 },

            };

        }

        private int[,] MandatoryDataNotPresent()
        {
            return new int[,]
            {
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,2,0 },
                {0,0,0,0,0,0,0,0,1 }
            };

        }

        private int[,] InvalidDataWithDuplicateInRow()
        {
            return new int[,]
            {
                 {3,0,6,5,0,8,4,0,0 },
                 {5,2,0,0,0,0,0,0,0 },
                 {0,8,7,0,0,0,0,3,1 },
                 {0,0,3,0,1,0,0,8,0 },
                 {9,0,0,8,6,3,0,0,5 },
                 {0,5,0,0,9,0,6,0,0 },
                 {1,3,0,0,0,0,2,5,3 },
                 {0,0,0,0,0,0,0,7,4 },
                 {0,0,5,2,0,6,3,0,0 },
            };

        }

        private int[,] InvalidDataWithDuplicateInColumn()
        {
            return new int[,]
            {
                 {3,1,6,5,0,8,4,0,0 },
                 {5,2,0,0,0,0,0,0,0 },
                 {0,8,7,0,0,0,0,3,1 },
                 {0,0,3,0,1,0,0,8,0 },
                 {9,0,0,8,6,3,0,0,5 },
                 {0,5,0,0,9,0,6,0,0 },
                 {1,3,0,0,0,0,2,5,0 },
                 {0,0,0,0,0,0,0,7,4 },
                 {0,1,5,2,0,6,3,0,0 },
            };

        }

        private int[,] InvalidNumberInInput()
        {
            return new int[,]
            {
                 {3,0,6,5,0,8,4,0,0 },
                 {5,2,0,0,0,0,0,0,0 },
                 {0,8,7,0,0,0,0,3,1 },
                 {0,0,3,0,1,0,0,8,0 },
                 {9,0,0,11,6,3,0,0,5 },
                 {0,5,0,0,9,0,6,0,0 },
                 {1,3,0,0,0,0,2,5,0 },
                 {0,0,0,0,0,0,0,7,4 },
                 {0,0,5,2,0,6,3,0,0 },
            };

        }


        #endregion "Private methods"
    }
}
