using System;

namespace ObjWast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading: src/test.wat");
            ObjWastTranspiler transpiler = new ObjWastTranspiler();
            transpiler.Parse(@"src\test.wat");

            Console.ReadKey(true);
        }
    }
}
