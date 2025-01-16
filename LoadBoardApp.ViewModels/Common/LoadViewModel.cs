using LoadBoardApp.PublishedContentModels.Models;

namespace LoadBoardApp.ViewModels.Common
{
    public class LoadViewModel
    {
        public LoadViewModel(Load model)
        {
            Id = model.Id;
            LoadName = model.Name;
            City = model.City;
            DeliveryCity = model.DeliveryCity;
            Rate = model.Rate;
            Miles = model.Miles;
            Broker = model.Broker;
            Contact = model.Contact;
            Commodity = model.Commodity;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
        }

        public int Id { get; }
        public string LoadName { get; }
        public string City { get; }
        public string DeliveryCity { get; }
        public string Rate { get; }
        public string Miles { get; }
        public string Broker { get; }
        public string Contact { get; }
        public string Commodity { get; }
        public string Latitude { get; }
        public string Longitude { get; }
    }
}
