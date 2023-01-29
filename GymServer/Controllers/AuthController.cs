using Microsoft.AspNetCore.Mvc;
using GymServer.Models;
using GymServer.Data;
using System.Data.Common;
using Dapper;
using System.Data;
using GymClient.Models;

namespace GymServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IDbConnectionn _dbConnection;

        public AuthController(IDbConnectionn dbConnection)
        {
            _dbConnection = dbConnection;
        }


        [HttpPost("register")]
        public async Task<ActionResult<IEnumerable<RegisterModel>>> PostUser(RegisterModel user)
        {
  
            using (var conn = _dbConnection.GetConnection)
            {
                string checksql = $"Select * from Clients WHERE [Email] = @Email";
               var checer =  conn.QueryFirstOrDefault<RegisterModel>(checksql, new { Email = user.Email });

                if (checer == null)
                {
                    Console.WriteLine("YEs");
                 

                    string sqlQuery = "INSERT INTO Clients (Name, LastName,BirthDay,Phone,Email,Gender,Password) VALUES(@Name,@LastName,@BirthDay,@Phone,@Email,@Gender,@Password)";

                    conn.Execute(sqlQuery, user);

                    var query = $"SELECT * FROM [Clients] WHERE [Email] = @Email";
                    var param = new DynamicParameters();
                    param.Add("@Email", user.Email);
                    var newUser = await conn.QueryFirstOrDefaultAsync<RegisterModel>(query, param: param, commandType: CommandType.Text);


                    return Ok(newUser);
                }
                return BadRequest();
                }

            }





        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<Client>>> LoginUser(LoginModel user)
        {
      
            using (var conn = _dbConnection.GetConnection)
            {
                string checksql = $"Select * from Clients WHERE [Email] = @Email AND [Password] = @Password";
                var checer = conn.QueryFirstOrDefault<Client>(checksql, new { Email = user.Email,Password = user.Password });

                if (checer != null)
                {
                    Console.WriteLine("YEsClient");
                    return Ok(checer);
                }

                string checksql2 = $"Select * from Personal WHERE [Email] = @Email AND [Password] = @Password";
                var trainer = conn.QueryFirstOrDefault<Personal>(checksql2, new { Email = user.Email, Password = user.Password });

                if (trainer != null)
                {
                    Console.WriteLine("YesTrain");
                    return Ok(trainer);
                }



                return BadRequest();
            }

        }





    }
}
