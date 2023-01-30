﻿using Dapper;
using GymClient.Models;
using GymServer.Data;
using GymServer.Models;

namespace GymServer.Classes
{
	public class CheckInfo
	{

		public async Task<bool> IsExist(string email, IDbConnectionn _dbConnection)
		{
			using (var conn = _dbConnection.GetConnection)
			{
				string checksql = $"Select * from Clients WHERE [Email] = @Email";
				var checer = conn.QueryFirstOrDefault<Client>(checksql, new { Email = email });

				if (checer != null)
				{
					return false;
				}

				string checksql2 = $"Select * from Personal WHERE [Email] = @Email";
				var checer2 = conn.QueryFirstOrDefault<Personal>(checksql2, new { Email = email });


				if (checer2 != null)
				{
					return false;
				}


			}
			return true;
		}


	}
}
