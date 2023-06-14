namespace Nullam.Models
{
	public class Company : ParticipantBase
	{
		public required string Name { get; set; }
		public int ParticipantAmount { get; set; }
		public double RegistrationCode { get; set; }
	}
}
