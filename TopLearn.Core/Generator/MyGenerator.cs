using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Generator
{
    public class MyGenerator
    {
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
