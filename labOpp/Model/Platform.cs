using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
	public class Platform
	{
		[Key]
		public Guid PlatformId { get; set; }

		public string Name { get; set; }
	}
}
