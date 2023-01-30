using Dapper;
using GymClient.Models;
using GymServer.Classes;
using GymServer.Data;
using GymServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GymServer.Controllers
{


	[ApiController]
	[Route("[controller]")]
	public class AdminController : Controller
	{



		private readonly IDbConnectionn _dbConnection;

		public AdminController(IDbConnectionn dbConnection)
		{
			_dbConnection = dbConnection;
		}



		[HttpPost("addpersonal")]
		public async Task<ActionResult<IEnumerable<Personal>>> PostPersonal(Personal user)
		{
		      CheckInfo check = new CheckInfo();
		
			if (await check.IsExist(user.Email,_dbConnection) == true)
			{
			
				using (var conn = _dbConnection.GetConnection)
				{

					string sqlQuery = "INSERT INTO Personal (Name, LastName,BirthDay,Phone,Email,Gender,RoleId,IMGPath,Password) VALUES(@Name,@LastName,@BirthDay,@Phone,@Email,@Gender,@RoleId,@IMGPath,@Password)";

					conn.Execute(sqlQuery, user);
					return Ok();
				}
			}
                 return BadRequest();
		}




		[HttpPost("addschedule")]
		public async Task<ActionResult<IEnumerable<Personal>>> PostSchedule(Schedule schedule)
		{
			CheckInfo check = new CheckInfo();
			Console.WriteLine("On serv");
			
				using (var conn = _dbConnection.GetConnection)
				{

					string sqlQuery = "INSERT INTO Schedule (Name, DateOfTrain,CoachId,TypeId,MaxPeople,CountPeople) VALUES (@Name,@DateOfTrain,@CoachId,@TypeId,@MaxPeople,@CountPeople)";

					conn.Execute(sqlQuery, schedule);
					return Ok();
				}
			
			return BadRequest();
		}








		[HttpGet("getformcoach")]
		public async Task<ActionResult<IEnumerable<Personal>>> GetFormCoaches()
		{
			CheckInfo check = new CheckInfo();

		
				using (var conn = _dbConnection.GetConnection)
				{

				string sqlQuery = "Select * from Personal WHERE RoleId=1"; 
				 var coaches =	conn.Query<Personal>(sqlQuery).ToList();
				
					return Ok(coaches);
				}
			
			return BadRequest();
		}



	}
}
