using System;
using System.Collections.Generic;
using System.Linq;

namespace Mickey.Core.ComponentModel
{
    public class InvokedResult
    {
        IReadOnlyCollection<ErrorDescriber> m_Errors;

        public bool Succeeded { get; set; }

        public IReadOnlyCollection<ErrorDescriber> Errors
        {
            get
            {
                return m_Errors;
            }
            set
            {
                Requires.NotNull(value, "value");
                m_Errors = value;
            }
        }

        public ErrorDescriber Error
        {
            get
            {
                return m_Errors == null ? null : m_Errors.FirstOrDefault();
            }
        }

        public static readonly InvokedResult SucceededResult = new InvokedResult { Succeeded = true };

        public static InvokedResult<T> Ok<T>(T data)
        {
            return new InvokedResult<T> { Succeeded = true, Data = data };
        }

        public static InvokedResult Fail(string errorCode, string errorDescription)
        {
            if (errorCode == null)
                throw new ArgumentNullException("errorCode");

            var result = new InvokedResult { Succeeded = false };
            var list = new ErrorDescriber[] { new ErrorDescriber { Code = errorCode, Description = errorDescription } };
            result.Errors = list;
            return result;
        }

        public static InvokedResult<T> Fail<T>(string errorCode, string errorDescription, T data)
        {
            if (errorCode == null)
                throw new ArgumentNullException("errorCode");

            var result = new InvokedResult<T> { Succeeded = false, Data = data };
            var list = new ErrorDescriber[] { new ErrorDescriber { Code = errorCode, Description = errorDescription } };
            result.Errors = list;
            return result;
        }
    }

    public class InvokedResult<T> : InvokedResult
    {
        public T Data { get; set; }
    }
}
