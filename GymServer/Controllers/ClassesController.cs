using Dapper;
using GymClient.Models;
using GymServer.Classes;
using GymServer.Data;
using GymServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymServer.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class ClassesController : Controller
	{

		private readonly IDbConnectionn _dbConnection;

		public ClassesController(IDbConnectionn dbConnection)
		{
			_dbConnection = dbConnection;
		}



		[HttpPost("usert")]
		public async Task<ActionResult<IEnumerable<PeopleOnWorkouts>>> PostSchedule(PeopleOnWorkouts schedule)
		{
			
			CheckInfo check = new CheckInfo();
			if (await check.IsOnTrain(schedule, _dbConnection) == false) { return BadRequest(); }
	
			using (var conn = _dbConnection.GetConnection)
			{

				string sqlQuery = "INSERT INTO PeopleOnWorkouts (ClientId,ScheduleId) VALUES (@ClientId,@ScheduleId)";

				await conn.ExecuteAsync(sqlQuery, new { ClientId = schedule.ClientId, ScheduleId = schedule.ScheduledId });


				string sqlQuery2 = "UPDATE Schedule SET  CountPeople +=  1 Where [Id] = @Id;";

				await conn.ExecuteAsync(sqlQuery2, new { Id = schedule.ScheduledId});


				return Ok();
			}
			return Ok();
			
		}








	}
}
