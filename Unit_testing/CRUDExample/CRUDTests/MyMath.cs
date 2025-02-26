using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public class MyMath
    {
        public int AddNumbers(int a,int b) {
            return a + b;
        }
        public dynamic HardCodedValue()
        {
            var data = new { 
                name = "stark",
                age = 12,
                gender = "M"
            };
            return data;
        }
    }
}
