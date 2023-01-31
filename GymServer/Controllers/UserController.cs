using Dapper;
using GymServer.Data;
using GymServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymServer.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{

		private readonly IDbConnectionn _dbConnection;

		public UserController(IDbConnectionn dbConnection)
		{
			_dbConnection = dbConnection;
		}




		[HttpPost("getusertrain")]
		public async Task<ActionResult<IEnumerable<Schedule>>> PostSchedule(UserId  suserId)
		{

			Console.WriteLine(suserId.Id);
			using (var conn = _dbConnection.GetConnection)
			{

				string sqlQuery = $"Select * from Schedule as s Left Join PeopleOnWorkouts On PeopleOnWorkouts.ScheduleId = s.Id INNER JOIN Personal ON Personal.Id=s.CoachId where DATEDIFF(DAY,GETDATE(),s.DateOfTrain) < 7 and s.DateOfTrain >= GETDATE() AND TypeId = 2 AND s.CountPeople<s.MaxPeople and PeopleOnWorkouts.ClientId = @Id ORDER BY s.DateOfTrain ASC;";

				var coaches = await conn.QueryAsync<Schedule, Personal, Schedule>(sqlQuery, (schedl, person) =>
				{
					schedl.personal = person;
					return schedl;
				},new {Id = suserId.Id});


				return Ok(coaches);
			}

		}






		[HttpPost("getusersolo")]
		public async Task<ActionResult<IEnumerable<Schedule>>> GetSoloTrain(UserId suserId)
		{

			using (var conn = _dbConnection.GetConnection)
			{

				string sqlQuery = $"Select * from Schedule as s Left Join PeopleOnWorkouts On PeopleOnWorkouts.ScheduleId = s.Id INNER JOIN Personal ON Personal.Id=s.CoachId where DATEDIFF(DAY,GETDATE(),s.DateOfTrain) < 7 and s.DateOfTrain >= GETDATE() AND TypeId = 1  and PeopleOnWorkouts.ClientId = @Id ORDER BY s.DateOfTrain ASC;";

				var coaches = await conn.QueryAsync<Schedule, Personal, Schedule>(sqlQuery, (schedl, person) =>
				{
					schedl.personal = person;
					return schedl;
				}, new { Id = suserId.Id });


				return Ok(coaches);
			}

		}






	}


	public class UserId
	{

		public int Id { get; set; }
	}

}
