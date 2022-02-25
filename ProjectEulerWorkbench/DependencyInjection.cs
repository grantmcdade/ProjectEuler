using ProjectEuler.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddSolutionUtilities(this ServiceCollection collection)
        {
            collection.AddTransient<IPathProvider, PathProvider>();
            collection.AddTransient<IPrimeSieveInt32, PrimeSieveInt32>();
        }
    }
}
