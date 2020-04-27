using System;
using System.Diagnostics;

namespace Utilities
{
    public static class Guard
    {
        public static class Against
        {
            [DebuggerStepThrough]
            public static string Empty(string parameter, string parameterName)
            {
                if (string.IsNullOrWhiteSpace(parameter))
                    throw new ArgumentNullException(parameterName);

                return parameter;
            }
        }
    }
}
