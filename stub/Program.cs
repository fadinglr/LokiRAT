using System;

namespace stub
{
    class Program
    {     
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                Core core = new Core(args[0], args[1]);
                core.Start();
            }
        }
    }
}