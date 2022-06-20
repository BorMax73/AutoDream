using Microsoft.AspNetCore.Http;

namespace AutoDream.ViewModels
{
    public class CarViewModel
    {
        public double EngineVolume { get; set; }
        public string EngineType { get; set; }
        public string Color { get; set; }
        public string ReleaseYear { get; set; }

        public string VINCode { get; set; }
        public IFormFile Image { get; set; }
    }
}