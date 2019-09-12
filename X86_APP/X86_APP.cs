using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC;
namespace X86_APP
{
    class X86_APP
    {
        static void Main(string[] args)
        {
            string[] rtn = IPC_CALL.CallMethod("Return_Add_And_Multiple", 5.3,"6",7);
            Console.WriteLine(rtn[0]);//=18.3
            Console.WriteLine(rtn[1]);//=222.6
            Console.Read();
        }
    }
}
