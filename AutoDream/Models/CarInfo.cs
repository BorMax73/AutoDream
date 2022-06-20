using System;
using System.Collections.Generic;

namespace AutoDream.Models
{
    public class CarInfo
    {
        public int Id { get; set; }
       public byte[] Image { get; set; }
        public string Color { get; set; }
       public string EngineType { get; set; }
       public double EngineVolume { get; set; }
        public string ReleaseYear { get; set; }
        public string ModelName { get; set; }
        public string Type { get; set; }

        public static explicit operator CarInfo(List<object> v)
        {
            throw new NotImplementedException();
        }
    }
}