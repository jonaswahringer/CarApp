using System;
using System.ComponentModel.DataAnnotations;

namespace CarAPI.Models
{
	public class Telemetry
	{
		[Key]
		public int IdTelemetry{ get; set; }

		public int Capacity { get; set; }
		public int Speed { get; set; }
		public int Latitude { get; set; }
		public int Longitude { get; set; }

		public int CarId { get; set; }
	}
}

