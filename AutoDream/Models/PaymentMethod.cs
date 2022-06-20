namespace AutoDream.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public double Sum { get; set; }
        public int OrderId { get; set; }

        public string WorkerId { get; set; }
    }
}