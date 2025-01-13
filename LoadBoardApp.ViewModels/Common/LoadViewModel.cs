using LoadBoardApp.PublishedContentModels.Models;

namespace LoadBoardApp.ViewModels.Common
{
    public class LoadViewModel
    {
        public LoadViewModel(Load model)
        {
            Id = model.Id;
            Lane = model.Lane;
            Rate = model.Rate;
            Miles = model.Miles;
            Broker = model.Broker;
            Name = model.LoadName;
            Contact = model.Contact;
            Commodity = model.Commodity;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
        }

        public int Id { get; }
        public string Lane { get; }
        public string Rate { get; }
        public string Miles { get; }
        public string Broker { get; }
        public string Name { get; }
        public string Contact { get; }
        public string Commodity { get; }
        public string Latitude { get; }
        public string Longitude { get; }
    }
}
