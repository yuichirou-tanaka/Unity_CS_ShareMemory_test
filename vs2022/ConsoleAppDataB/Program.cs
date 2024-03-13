using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        setClient1_2();
    }

    static void setClient1()
    {
        MemoryMappedFile shared_mem = MemoryMappedFile.OpenExisting("shared_memory");
        MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

        int data = accessor.ReadInt32(0);
        Console.WriteLine("Data = " + data);

        accessor.Dispose();
        shared_mem.Dispose();
    }

    static void setClient1_2()
    {
        MemoryMappedFile shared_mem = MemoryMappedFile.OpenExisting("shared_memory");
        MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

        int datasize = accessor.ReadInt32(0);
        Console.WriteLine("datasize = " + datasize);

        char[] data = new char[datasize];
        accessor.ReadArray<char>(sizeof(int), data, 0, data.Length);

        string str = new string(data);

        Console.WriteLine("Data = " + str);

        accessor.Dispose();
        shared_mem.Dispose();
    }

    // Process B:
    static void ProcessB(string[] args)
    {
        try
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("testmap"))
            {

                Mutex mutex = Mutex.OpenExisting("testmapmutex");
                mutex.WaitOne();

                using (MemoryMappedViewStream stream = mmf.CreateViewStream(1, 0))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(0);
                }
                mutex.ReleaseMutex();
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Memory-mapped file does not exist. Run Process A first.");
        }
    }
}