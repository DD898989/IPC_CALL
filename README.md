# IPC_CALL
simple ipc communication that allow many applications call each others method and get returns, it's a simple solution for X86/X64 compatible issues


usage:

1. make IPC_CALL.cs an isolated class library project that can be referenced by many other projects

2. Application1: 

        static IPC_CALL make_callable = new IPC_CALL(Return_Add_And_Multiple);
        static string[] Return_Add_And_Multiple(string[] args)
        {
            int add = 0;
            int multiple = 1;

            foreach (string s in args)
                add += int.Parse(s);

            foreach (string s in args)
                multiple *= int.Parse(s);

            return new string[] { add.ToString() ,multiple.ToString() };
        }
        
3. Application2

       rtn = IPC_CALL.CallMethod("Return_Add_And_Multiple", "5", "6", "7");
       Console.WriteLine(rtn[0]);//=18
       Console.WriteLine(rtn[1]);//=210
