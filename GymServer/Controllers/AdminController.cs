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
		public async Task<ActionResult<IEnumerable<Schedule>>> PostSchedule(Schedule schedule)
		{
			
			Console.WriteLine(123);
				using (var conn = _dbConnection.GetConnection)
				{

					string sqlQuery = "INSERT INTO Schedule (Name,DateOfTrain,TimeOfTrain,CoachId,TypeId,MaxPeople,CountPeople) VALUES (@Name,@DateOfTrain,@TimeOfTrain,@CoachId,@TypeId,@MaxPeople,@CountPeople)";

					await conn.ExecuteAsync(sqlQuery, schedule);
					return Ok();
				}
			
			return BadRequest();
		}





		[HttpGet("getcoach")]
		public async Task<ActionResult<IEnumerable<Personal>>> GetFormCoaches()
		{
		
				using (var conn = _dbConnection.GetConnection)
				{

				string sqlQuery = "Select * from Personal WHERE RoleId=1"; 
				 var coaches =	conn.Query<Personal>(sqlQuery).ToList();
				
					return Ok(coaches);
				}
			
			return BadRequest();
		}


		[HttpGet("getschedule")]
		public async Task<ActionResult<IEnumerable<Personal>>> GetSchedule()
		{

			using (var conn = _dbConnection.GetConnection)
			{

				string sqlQuery = "Select * from Schedule as s INNER JOIN Personal ON Personal.Id=s.CoachId where DATEDIFF(DAY,GETDATE(),s.DateOfTrain) < 7 and s.DateOfTrain >= GETDATE() AND TypeId = 2 ORDER BY s.DateOfTrain ASC;";
				var coaches = await conn.QueryAsync<Schedule, Personal,Schedule>(sqlQuery, (schedl, person) =>
				{
					schedl.personal = person;
					return schedl;
				});

				return Ok(coaches);
			}

			return BadRequest();
		}



	}
}
