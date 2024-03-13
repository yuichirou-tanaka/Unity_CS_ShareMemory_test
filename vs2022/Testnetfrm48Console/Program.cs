using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestSharedMemoryFrameworkClassLibrary1;

namespace Testnetfrm48Console
{
    internal class Program
    {

        static void example2()
        {
            long offset = 0x10; // 256 megabytes
            long length = 0x10; // 512 megabytes


            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile(@"D:\tmp\ExtremelyLargeImage.data", FileMode.Open, "ImgA"))
            {
                using (var accessor = mmf.CreateViewAccessor(offset, length))
                {
                    //accessor.Write(offset, length);
                    int colorSize = Marshal.SizeOf(typeof(MyColor));
                    MyColor color;

                    // Make changes to the view.
                    for (long i = 0; i < length; i += colorSize)
                    {
                        accessor.Read(i, out color);
                        color.Brighten(10);
                        accessor.Write(i, ref color);
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            example2();


        }
        static void smain2() { 

            TestClass1 t12 = new TestClass1();

            Console.WriteLine(t12.testCMain());

            MemoryMappedFile shared_mem = MemoryMappedFile.CreateNew("shared_memory", 1024);
            MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

            accessor.Write(0, 256);

            accessor.Dispose();

            while (true) System.Threading.Thread.Sleep(1000);
        }

        public struct MyColor
        {
            public short Red;
            public short Green;
            public short Blue;
            public short Alpha;

            // Make the view brighter.
            public void Brighten(short value)
            {
                Red = (short)Math.Min(short.MaxValue, (int)Red + value);
                Green = (short)Math.Min(short.MaxValue, (int)Green + value);
                Blue = (short)Math.Min(short.MaxValue, (int)Blue + value);
                Alpha = (short)Math.Min(short.MaxValue, (int)Alpha + value);
            }
        }

    }

}
