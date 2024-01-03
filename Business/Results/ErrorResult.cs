
using Business.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Results
{
    public class ErrorResult : Result
    {
        // Example: Result result = new ErrorResult("Operation failed.");
        public ErrorResult(string message) : base(false, message)
        {
        }

        // Example: Result result = new ErrorResult();
        public ErrorResult() : base(false, "")
        {
        }
    }
}
