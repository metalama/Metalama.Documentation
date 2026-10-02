using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    public class Code 
    {
        
        public  string Name { get; set; }

        public string Description { get; set; }

    }

    
    public class Demo 
    {
        public static void Main()
        {
            var currentCode = new Code();
            Console.WriteLine($"Name length = {currentCode.Name.Length}");
        }
    }
}
