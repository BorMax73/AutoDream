namespace AutoDream.Models
{
    public class DeliveryOrder
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string EngineVolume { get; set; }
        public string EngineType { get; set; }
        public string MaxPrice { get; set; }

        public string OtherInfo { get; set; }
        public int ClientId { get; set; }
    }
}