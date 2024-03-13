using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DllSharedDataClass;


public class TestDllAccess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SensorData sd = new SensorData();
        sd.time = 0.0f;
        sd.points = new List<SensorPoint>();
        sd.points.Add(new SensorPoint() { x = 1.0f, y = 5.0f });
        sd.points.Add(new SensorPoint() { x = 3.0f, y = 4.0f });
        sd.points.Add(new SensorPoint() { x = 2.0f, y = 3.0f });
        sd.points.Add(new SensorPoint() { x = 1.0f, y = 2.0f });



        Debug.Log(sd.toString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
