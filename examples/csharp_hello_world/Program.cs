using System;
using static Bgfx.bgfx;

namespace csharp_hello_world
{
    class Program : Bgfx.bgfx
    {
        static void Main(string[] args)
        {
            var stateFlags = StateFlags.AlphaRefMask;

            Console.WriteLine("Hello World!");
        }
    }
}
