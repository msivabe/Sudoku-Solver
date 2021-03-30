using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sudoku_Solver.DataProvider
{
    [Serializable]
    public class DataProviderException : Exception
    {
        public DataProviderException()
            : base() { }

        public DataProviderException(string message)
            : base(message) { }

        public DataProviderException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DataProviderException(string message, Exception innerException)
            : base(message, innerException) { }

        public DataProviderException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DataProviderException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
