

namespace CharmFinalAPI.Models
{
    public class Rate
    {
        public int RateId { get; set; } 
        public int ArtistId { get; set; }   
        public int SpecialtyId {  get; set; }   
        public decimal RatePrice {  get; set; }
       

        //CREATE TABLE CharmArtistry.Rate(
        //RateId INT IDENTITY(1,1) PRIMARY KEY,
        //ArtistId INT, 
        //SpecialtyId INT,
        //RatePrice DECIMAL(10,2)

    }
}
