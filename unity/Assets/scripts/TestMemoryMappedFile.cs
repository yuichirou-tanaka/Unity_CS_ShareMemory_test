using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class TestMemoryMappedFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setClient1_2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void setClient1_2()
    {
        MemoryMappedFile shared_mem = MemoryMappedFile.OpenExisting("shared_memory");
        MemoryMappedViewAccessor accessor = shared_mem.CreateViewAccessor();

        int datasize = accessor.ReadInt32(0);
        Debug.Log("datasize = " + datasize);

        char[] data = new char[datasize];
        accessor.ReadArray<char>(sizeof(int), data, 0, data.Length);

        string str = new string(data);

        Debug.Log("Data = " + str);

        accessor.Dispose();
        shared_mem.Dispose();
    }

}
