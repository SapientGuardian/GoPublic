using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace GoPublic
{    
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "GoPublic generates a copy of a .NET assembly with all of its types and methods public.");
                Console.WriteLine("Usage: GoPublic.exe path-to-assembly [path-to-output]");
                return;
            }

            var module = ModuleDefinition.ReadModule(args[0]);
            foreach (var type in module.Types)
            {
                type.IsPublic = true;

                foreach (var method in type.Methods)
                {
                    method.IsPublic = true;
                }
            }

            var output = args.Length > 1
                             ? args[1]
                             : System.IO.Path.ChangeExtension(
                                 args[0],
                                 ".public" + System.IO.Path.GetExtension(args[0]));

            module.Write(output);
        }
    }
}
