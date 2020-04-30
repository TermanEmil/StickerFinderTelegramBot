using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilities
{
    public static class TaskExtensions
    {
        public static async Task<List<T>> ToList<T>(this Task<IEnumerable<T>> task)
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            var results = await task;
            return results.ToList();
        }
    }
}
