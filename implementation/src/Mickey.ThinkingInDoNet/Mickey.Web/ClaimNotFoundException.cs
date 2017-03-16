using System;

namespace Mickey.Web
{
    public class ClaimNotFoundException : Exception
    {
        public ClaimNotFoundException(string message) : base(message)
        {
        }
    }
}
