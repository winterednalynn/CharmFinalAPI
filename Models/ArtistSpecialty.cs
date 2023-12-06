

namespace CharmFinalAPI.Models
{
    public class ArtistSpecialty
    {
        public int ArtistId { get; set; }
        public int SpecialtyId { get; set; }


        //CREATE TABLE CharmArtistry.ArtistSpecialty(
        //ArtistId INT,
        //SpecialtyId INT,
        //PRIMARY KEY (ArtistId, SpecialtyId), 
        //FOREIGN KEY(ArtistId) REFERENCES CharmArtistry.Artists(ArtistId), 
        //FOREIGN KEY(SpecialtyId) REFERENCES CharmArtistry.Specialty(SpecialtyId)

    }
}
