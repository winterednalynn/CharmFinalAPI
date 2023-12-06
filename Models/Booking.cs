

namespace CharmFinalAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }  
        public int ClientId { get; set; }
        public int ArtistId { get; set; }
        public int SpecialtyId {  get; set; }   
        public int RateId {  get; set; }    
        public DateTime DateBooked { get; set; }    


     
        //CREATE TABLE CharmArtistry.Booking(
        //BookingId INT IDENTITY(1,1) PRIMARY KEY,
        //ClientId INT, 
        //ArtistId INT,
        //SpecialtyId INT, 
        //RateId INT,
        //DateBooked DATE

    }
}
