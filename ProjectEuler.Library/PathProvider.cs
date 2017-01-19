using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Library
{
    public class PathProvider : IPathProvider
    {
        public string GetFullyQualifiedPath(string name)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Resources");
            return Path.Combine(path, name);
        }
    }
}
