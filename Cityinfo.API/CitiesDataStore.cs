using Cityinfo.API.Models;

namespace Cityinfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsofInterest = new List<PointofInterestDto>()
                        {
                        new PointofInterestDto() {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the US."},
                        new PointofInterestDto() {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper located in Midtown." }
                       }
                },
        
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                     PointsofInterest = new List<PointofInterestDto>()
                        {
                        new PointofInterestDto() {
                            Id = 3,
                            Name = "Cathedral of Our Lady",
                            Description = "A Gothic style cathedral."},
                        new PointofInterestDto() {
                            Id = 4,
                            Name = "Antwerp Central Station",
                            Description = "The the finest example of railway architecture in Belgium." }
                       }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                     PointsofInterest = new List<PointofInterestDto>()
                        {
                        new PointofInterestDto() {
                            Id = 5,
                            Name = "Eiffel Tower",
                            Description = "A wrought iron lattice tower on the Champ de Mars."},
                        new PointofInterestDto() {
                            Id = 6,
                            Name = "The Louvre",
                            Description = "The world's largest museum." }
                       }
                }
            };
        }
    }
}
