namespace AutoDream.Models
{
    public class Car
    {
        public int Id { get; set; }
        public double EngineVolume { get; set; }
        public string EngineType { get; set; }
        public string Color { get; set; }
        public string ReleaseYear { get; set; }

        public string VINCode { get; set; }

        public int ModelId { get; set; }

        public int CarPriceId { get; set; }

        public byte[] Image { get; set; }
    }
}