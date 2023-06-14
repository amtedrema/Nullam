namespace Nullam.Models
{
	public class Person : ParticipantBase
	{
		public required string FirstName { get; set; }
		public string? LastName { get; set; }
		public double IdCode { get; set; }
	}
}
