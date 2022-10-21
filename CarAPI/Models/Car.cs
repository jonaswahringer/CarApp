using System;
using System.ComponentModel.DataAnnotations;

namespace CarAPI.Models
{
	public class Car
	{
		[Key]
		public int IdCar { get; set; }

		public string Name { get; set; } = String.Empty;
		public string Type { get; set; } = String.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime ModifiedAt { get; set; }

		public ICollection<Telemetry> Telemetries { get; set; } = new List<Telemetry>(); //Referenz auf n-Seite
	}
}

