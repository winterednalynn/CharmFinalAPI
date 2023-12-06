using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CharmFinalAPI.Models;
using System.Data.SqlClient;
using Dapper;

namespace CharmFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        string connectionString;

        public ClientController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("CharmConnection");
        }

        //DISPLAYS ALL CLIENTS IN SYSTEM 
        [HttpGet]
        public ActionResult<List<Client>> AllClients()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            List<Client> clients = connection.Query<Client>("SELECT * FROM CharmArtistry.Client").ToList();
            return Ok(clients);
        }

        //GET BY ID 
        [HttpGet("{id}")]
        //This only get one employee
        public ActionResult<Client> ClientById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            Client theClient = connection.QueryFirstOrDefault<Client>("SELECT * FROM CharmArtistry.Client WHERE ClientId = @Id", new { Id = id });
            //check if the employee was null
            if (theClient == null)
            {
                return NotFound();
            }
            return Ok(theClient);
        }

        [HttpPost]
        //CREATE CLIENT

        public ActionResult<Client> CreateClient(Client client1)
        {
            if (client1.ClientId < 1)
            {
                return BadRequest();
            }

            using SqlConnection connection = new SqlConnection(connectionString);

            Booking booked = connection.QueryFirstOrDefault<Booking>("SELECT * FROM CharmArtistry.Booking" + "WHERE BookingId = @Id", new { Id = client1.ClientId });

            if (booked == null)
            {
                return BadRequest();
            }
            client1.ClientId = booked.BookingId;

            try
            {
                //SCOPE IDENTIY = gets you primary key 

                Client newClient = connection.QuerySingle<Client>(
                    "INSERT INTO CharmArtistry.Client (FirstName, LastName, Email, ContactNumber) " +
                    "VALUES (@FirstName, @LastName, @Email, @ContactNumber);" + "SELECT * FROM CharmArtistry.Client WHERE ClientId = SCOPE_IDENTITY();", client1);

                return Ok(newClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        //DELETE CLIENT 
        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int clientId)
        {
            if (clientId < 1)
            {
                return BadRequest();
            }
            using SqlConnection connection = new SqlConnection(connectionString);
            int deletedRows = connection.Execute("DELETE FROM CharmArtistry.Client WHERE ClientId = @Id", new { Id = clientId });
            if (deletedRows == 0)
            {
                return NotFound();

            }
            return Ok();
        }
        //UPDATE 
        [HttpPut("{id}")]
        public ActionResult<Client> UpdateClient(int id, Client client)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            client.ClientId = id;
            using SqlConnection connection = new SqlConnection(connectionString);

            Booking appointment = connection.QueryFirstOrDefault<Booking>("SELECT * FROM CharmArtistry.Booking " +
                "WHERE BookingId = @Id", new { Id = client.ClientId });
            if (appointment == null)
            {
                return BadRequest();
            }
            client.FirstName = appointment.ToString();
            try
            {
                Client updatedclient = connection.QuerySingle<Client>(
                    "UPDATE CharmArtistry.Client SET FirstName = @FirstName, LastName = @LastName, Email = @Email" +
                    "ContactNumber = @ContactNumber WHERE ClientId = @ClientId", client);
                return Ok(updatedclient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }


        }











        //CREATE TABLE CharmArtistry.Client(
        //ClientId INT IDENTITY(1,1) PRIMARY KEY,
        //FirstName VARCHAR(50) NOT NULL,
        // LastName VARCHAR(50) NOT NULL,
        //    Email VARCHAR(255) NOT NULL,
        //    ContactNumber VARCHAR(10) NOT NULL


    }
}
