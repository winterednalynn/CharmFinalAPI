

namespace CharmFinalAPI.Models
{
    public class Client
    {

        public int ClientId { get; set; }   
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string ContactNumber { get; set; }


        //CREATE TABLE CharmArtistry.Client(
        //ClientId INT IDENTITY(1,1) PRIMARY KEY,
        //FirstName VARCHAR(50) NOT NULL,
        // LastName VARCHAR(50) NOT NULL,
        //    Email VARCHAR(255) NOT NULL,
        //    ContactNumber VARCHAR(10) NOT NULL

    }
}
