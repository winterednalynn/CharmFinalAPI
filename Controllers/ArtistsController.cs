using Dapper;
using CharmFinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CharmFinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ArtistsController : ControllerBase
    {
        //What is a controller ? A controller is just a class that controls a controller base. 

        //We need a constructor to initialize the fields 

        string connectionString;

        public ArtistsController(IConfiguration configuration) //Iconfiguration is a interface connects to connection string. 
        {
            connectionString = configuration.GetConnectionString("CharmConnection");
            Console.WriteLine(connectionString);
        }

        [HttpGet]

        // Return Data & Status 

        public ActionResult<List<Artists>> GetAllArtists()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            //here is where Dapper comes into play 

            // To access the interface ; IEnumerable is called. 

            List<Artists> artistsList = connection.Query<Artists>("SELECT * FROM CharmArtistry.Artists").ToList();
            return artistsList;
        }

        [HttpGet("{id}")]

        public ActionResult<Artists> GetArtists(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            Artists charmArtist = connection.QueryFirstOrDefault<Artists>("SELECT * FROM CharmArtistry.Artists WHERE ArtistId = @Id", new { Id = id }); 
            if (charmArtist == null)
            {
                return BadRequest(); 
            }
            return Ok(charmArtist); 
        }

        [HttpPost]
        // ADD ARTIST 
        public ActionResult<Artists> AddCharm (Artists charmArtist)
        {
            if(charmArtist.ArtistId<1)
            {
                return BadRequest(); 
            }
            using SqlConnection connection = new SqlConnection(connectionString);

            Artists charmArtista = connection.QueryFirstOrDefault<Artists>("SELECT * FROM CharmArtistry.Specialty " + "WHERE ArtistId = @Id", new { Id = charmArtist.ArtistId }); 

            if(charmArtista.FirstName == null)
            {
                return BadRequest(); 
            }
            charmArtist.ArtistId = charmArtista.ArtistId;

            try
            {
                Artists newCharm = connection.QuerySingle<Artists>(
                    "INSERT INTO CharmArtistry.Artists (FirstName, LastName, Email, ContactNumber) " +
                    "VALUES (@FirstName, @LastName, @Email, @ContactNumber); " +
                    "SELECT * FROM CharmArtistry.Artists WHERE ArtistId = SCOPE_IDNETITY();", charmArtist);

                return Ok(newCharm);
            }
            catch
            {
                return BadRequest();
            }

            
        }
        // DELETE = DELETE 
        [HttpDelete("{id}")]
        public ActionResult DeleteCharm(int id)
        {
            if(id < 1 )
            {
                return BadRequest();
            }
            using SqlConnection connection = new SqlConnection( connectionString);
            int xdeletedRows = connection.Execute("DELETE FROM CharmArtistry.Artists WHERE ArtistId = Id", new { Id = id });
            if (xdeletedRows == 0)
            {
                return BadRequest();
            }
            return Ok();    
        }

        //PUT = UPDATE 

        [HttpPut ("{id}")]
        public ActionResult<Artists> UpdateCharm (int id, Artists charm)
        {
            if (id < 1 )
            {
                return BadRequest();
            }
            charm.ArtistId = id; 
            using SqlConnection connection = new SqlConnection ( connectionString);
            Specialty service = connection.QueryFirstOrDefault<Specialty>("SELECT * FROM CharmArtistry.Specialty" + "WHERE SpecialtyId = @Id", new {Id = charm.ArtistId});
            
            if(service == null)
            {
                return BadRequest();    
            }
            charm.ArtistId = service.SpecialtyId; 

            try
            {
                Artists uptodateCharm = connection.QuerySingle<Artists>(
                    "UPDATE CharmArtistry.Artists SET FirstName = @FirstName, LastName = @LastName, Email=@Email, ContactNumber=@ContactNumber", charm);
                return Ok(uptodateCharm); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

           
        }
        





        //CREATE TABLE CharmArtistry.Artists(
        //    ArtistId INT PRIMARY KEY IDENTITY(1,1) ,
        //    FirstName VARCHAR(50) NOT NULL,
        //    LastName VARCHAR(50) NOT NULL,
        //    Email VARCHAR(255) NOT NULL,
        //    ContactNumber VARCHAR(10) NOT NULL

    }
}


    

  


