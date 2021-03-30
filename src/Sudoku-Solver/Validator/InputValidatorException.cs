using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sudoku_Solver.Validator
{
    [Serializable]
    public class InputValidatorException : Exception
    {
        public InputValidatorException()
            : base() { }

        public InputValidatorException(string message)
            : base(message) { }

        public InputValidatorException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InputValidatorException(string message, Exception innerException)
            : base(message, innerException) { }

        public InputValidatorException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InputValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
