using System;
using System.Collections.Generic;
using System.Text;

namespace DllSharedDataClass
{
    [Serializable]
    public struct SensorPoint
    {
        public float x;
        public float y;
        
        public string toString()
        {
            string s = "";
            s += "x:" + x.ToString() + "\n";
            s += "y:" + y.ToString() + "\n";
            return s;
        }
    }
    [Serializable]
    public class SensorData
    {
        public float time;
        public List<SensorPoint> points;

        public string toString()
        {
            string s = "";
            s += "time:" + time.ToString() + "\n";
            foreach (SensorPoint point in points)
            {
                s += " point:" + point.toString() + "\n";
            }
            return s;
        }

    }
}
