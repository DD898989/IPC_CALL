using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.IO.Pipes;


namespace IPC
{
    //[AttributeUsage(AttributeTargets.Method)]
    public class IPC_CALL// : System.Attribute
    {

        public delegate object[] Method_Delegate(string[] args);
        private event Method_Delegate Method_Event;
        static private string[] NULL = new string[] { "NULL" };

        public IPC_CALL(Method_Delegate methodPtr)//no need to keep the object new from this constructor
        {
            Method_Event += methodPtr;
            new Thread(() =>
            {
                while (true)
                {
                    string[] arr = Wait(methodPtr.Method.Name);

                    string[] paras = arr.Skip(1).ToArray();//arr[1 to end]: parameters for method
                    if (paras.SequenceEqual(NULL))
                        paras = null;

                    object[] _rtn = Method_Event(paras);
                    string[] rtn = _rtn.OfType<object>().Select(o => o.ToString()).ToArray();

                    Send(arr[0], rtn);//arr[0]: return channel, from random guid
                }
            }).Start();
        }

        static private void Send(string channel, string[] arr)
        {
            if (arr == null)
                arr = NULL;

            using (NamedPipeClientStream send = new NamedPipeClientStream(channel))
            {
                send.Connect();
                StreamWriter write = new StreamWriter(send);
                write.AutoFlush = true;
                write.Write(string.Join(",", arr));
            }
        }

        static private string[] Wait(string channel)
        {
            using (NamedPipeServerStream wait = new NamedPipeServerStream(channel))
            {
                wait.WaitForConnection();
                string rtn = new StreamReader(wait).ReadToEnd();
                return rtn.Split(',');
            }
        }

        public static string[] CallMethod(string methodName, params object[] _paras)
        {
            if (_paras == null)
                _paras = NULL;

            string[] paras = _paras.OfType<object>().Select(o => o.ToString()).ToArray();


            string rtnChannel = Guid.NewGuid().ToString();
            Send(methodName, new string[] { rtnChannel }.Concat(paras).ToArray());

            string[] rtn = Wait(rtnChannel);

            if (rtn.SequenceEqual(NULL))
                rtn = null;

            return rtn;
        }

    }
}