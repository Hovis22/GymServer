﻿using System.ComponentModel.DataAnnotations;

namespace GymServer.Models
{
	public class Schedule
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public DateTime DateOfTrain { get; set; }
		[Required]
		public int TimeOfTrain { get; set; }

		[Required]
		public int CoachId { get; set; }
		[Required]
		public int TypeId { get; set; }

		[Required]
		public int MaxPeople { get; set; }

		public int CountPeople { get; set; } = 0;

		public Personal? personal { get; set; }
	}
}
