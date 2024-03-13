using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

class Program
{
    // Process A:
    static void Main(string[] args)
    {

        server1_2();
    }

    static void server1()
    {
        MemoryMappedFile shared_mem = MemoryMappedFile.CreateNew("shared_memory", 1024);
        MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

        accessor.Write(0, 111);
        accessor.Dispose();

        while (true)
        {
            System.Threading.Thread.Sleep(1000);    
        }

    }

    static void server1_2()
    {
        MemoryMappedFile shared_mem = MemoryMappedFile.CreateNew("shared_memory", 1024);
        MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

        string str = "s asdfasdfasdf  sdfasdfedwerwer";
        char[] data = str.ToCharArray();

        accessor.Write(0, data.Length);
        accessor.WriteArray<char>(sizeof(int), data, 0, data.Length);

        accessor.Dispose();

        Console.WriteLine("write: " + str);

        while (true)
        {
            System.Threading.Thread.Sleep(1000);
        }

    }




    static void smain1() { 

        Console.WriteLine(Environment.SystemPageSize);

        using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
        {
            bool mutexCreated;
            Mutex mutex = new Mutex(true, "testmapmutex", out mutexCreated);
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(0);
            }
            mutex.ReleaseMutex();

            Console.WriteLine("Start Process B and press ENTER to continue.");
            Console.ReadLine();

            Console.WriteLine("Start Process C and press ENTER to continue.");
            Console.ReadLine();

            mutex.WaitOne();
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                Console.WriteLine("Process A says: {0}", reader.ReadBoolean());
                Console.WriteLine("Process B says: {0}", reader.ReadBoolean());
                Console.WriteLine("Process C says: {0}", reader.ReadBoolean());
            }
            mutex.ReleaseMutex();
        }
    }
}