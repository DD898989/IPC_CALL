using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC;
namespace X64_APP
{
    class X64_APP
    {

        static IPC_CALL make_callable = new IPC_CALL(Return_Add_And_Multiple);
        static object[] Return_Add_And_Multiple(string[] args)
        {
            double add = 0;
            double multiple = 1;

            foreach (string s in args)
                add += double.Parse(s);

            foreach (string s in args)
                multiple *= double.Parse(s);

            return new object[] { add, multiple};
        }

        static void Main(string[] args)
        {
        }
    }
}
