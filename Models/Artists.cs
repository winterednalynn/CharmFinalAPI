
namespace CharmFinalAPI.Models
{
    public class Artists
    {
        public int ArtistId {  get; set; } // PRIMARY KEY IN DATABASE 
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Email { get; set; }   
        public string ContactNumber { get; set; }   





        //CREATE TABLE CharmArtistry.Artists(
        //    ArtistId INT PRIMARY KEY IDENTITY(1,1) ,
        //    FirstName VARCHAR(50) NOT NULL,
        //    LastName VARCHAR(50) NOT NULL,
        //    Email VARCHAR(255) NOT NULL,
        //    ContactNumber VARCHAR(10) NOT NULL

    }
}
