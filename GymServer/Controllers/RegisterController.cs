using Microsoft.AspNetCore.Mvc;
using GymServer.Models;
using GymServer.Data;
using System.Data.Common;
using Dapper;

namespace GymServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly IDbConnection _dbConnection;

        public RegisterController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<RegisterModel>>> PostUser(RegisterModel user)
        {
  
            using (var conn = _dbConnection.GetConnection)
            {
                string sqlQuery = "INSERT INTO Clients (Name, LastName,BirthDay,Phone,Email,Sex,Password) VALUES(@Name,@LastName,@BirthDay,@Phone,@Email,@Sex,@Password)";

                conn.Execute(sqlQuery, user);
            }

                return Ok();     
        }




    }
}
